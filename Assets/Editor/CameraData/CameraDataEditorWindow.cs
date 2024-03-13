using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RicTools;
using RicTools.Editor.Windows;
using UnityEditor;
using ProjectSteppe.ScriptableObjects.CameraData;
using RicTools.Editor.Utilities;
using Cinemachine;
using UnityEngine.UIElements;
using ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData;
using ProjectSteppe.ScriptableObjects.CameraData.AimCameraData;
using ProjectSteppe.Editor.CameraDataSubEditors;
using System;
using Cinemachine.Utility;

namespace ProjectSteppe.Editor
{
    public class CameraDataEditorWindow : GenericEditorWindow<CameraDataScriptableObject>
    {
        public EditorContainer<string> presetName = new EditorContainer<string>();

        public EditorContainer<CinemachineVirtualCameraBase.StandbyUpdateMode> standbyUpdateMode = new EditorContainer<CinemachineVirtualCameraBase.StandbyUpdateMode>(CinemachineVirtualCameraBase.StandbyUpdateMode.RoundRobin);

        public EditorContainer<float> verticalFov = new EditorContainer<float>(40);
        public EditorContainer<float> nearClipPlane = new EditorContainer<float>(0.2f);
        public EditorContainer<float> farClipPlane = new EditorContainer<float>(2000);
        public EditorContainer<float> dutch = new EditorContainer<float>();

        public EditorContainer<BodyDataType> bodyDataType = new EditorContainer<BodyDataType>();

        public EditorContainer<AimDataType> aimDataType = new EditorContainer<AimDataType>();

        public EditorContainer<BaseBodyCameraDataScriptableObject> baseBodyScriptableObject = new EditorContainer<BaseBodyCameraDataScriptableObject>();
        public EditorContainer<BaseAimCameraDataScriptableObject> baseAimScriptableObject = new EditorContainer<BaseAimCameraDataScriptableObject>();

        private List<BodyCameraDataSubEditor> bodySubEditors = new List<BodyCameraDataSubEditor>();
        private List<AimCameraDataSubEditor> aimSubEditors = new List<AimCameraDataSubEditor>();
        private List<ExtensionCameraDataSubEditor> extensionSubEditors = new List<ExtensionCameraDataSubEditor>();

        [MenuItem("Window/RicTools Windows/Camera Data Editor")]
    	public static CameraDataEditorWindow ShowWindow()
        {
            return GetWindow<CameraDataEditorWindow>("Camera Data Editor");
        }

        protected override void CreateEditorGUI()
        {
            {
                var element = rootVisualElement.AddTextField(presetName, "Name");

                RegisterLoadChange(element, presetName);
            }

            {
                var element = rootVisualElement.AddEnumField(standbyUpdateMode, "Standby Update");

                RegisterLoadChange(element, standbyUpdateMode);
            }

            {
                var lensFoldout = rootVisualElement.AddFoldout("Lens");

                {
                    var element = lensFoldout.AddFloatField(verticalFov, "Vertical FOV");

                    element.RegisterValueChangedCallback(callback =>
                    {
                        verticalFov.Value = Mathf.Clamp(verticalFov.Value, 1, 179);
                        element.value = verticalFov.Value;
                    });

                    RegisterLoadChange(element, verticalFov);
                }

                {
                    FloatField farClipPlaneFloatField = null;

                    {
                        var element = lensFoldout.AddFloatField(nearClipPlane, "Near Clip Plane");

                        element.RegisterValueChangedCallback(callback =>
                        {
                            nearClipPlane.Value = Mathf.Max(nearClipPlane.Value, 0.01f);
                            if (nearClipPlane.Value > farClipPlane.Value)
                            {
                                farClipPlane.Value = nearClipPlane.Value;
                                farClipPlaneFloatField.value = farClipPlane.Value;
                            }
                            element.value = nearClipPlane.Value;
                        });

                        RegisterLoadChange(element, nearClipPlane);
                    }

                    {
                        farClipPlaneFloatField = lensFoldout.AddFloatField(farClipPlane, "Far Clip Plane");

                        farClipPlaneFloatField.RegisterValueChangedCallback(callback =>
                        {
                            if (farClipPlane.Value <= nearClipPlane.Value)
                                farClipPlane.Value = nearClipPlane.Value + 0.001f;
                            farClipPlaneFloatField.value = farClipPlane.Value;
                        });

                        RegisterLoadChange(farClipPlaneFloatField, farClipPlane);
                    }
                }

                {
                    var element = lensFoldout.AddSlider(dutch, -180, 180, "Dutch");

                    RegisterLoadChange(element, dutch);
                }
            }

            {
                var bodyFoldout = rootVisualElement.AddFoldout("Body");

                {
                    var element = bodyFoldout.AddEnumField(bodyDataType, "", () =>
                    {
                        CheckBodyVisibility();
                    });

                    RegisterLoadChange(element, bodyDataType);
                }

                {
                    var element = bodyFoldout.AddObjectField(baseBodyScriptableObject, "Body Scriptable Object");
                    element.SetEnabled(false);


                    RegisterLoadChange(element, baseBodyScriptableObject);
                    AddElementToDebugView(element);
                }

                bodySubEditors.Add(new Body3rdFollowCameraDataSubEditor(bodyFoldout));
                bodySubEditors.Add(new BodyFramingTransposerCameraDataSubEditor(bodyFoldout));
                bodySubEditors.Add(new BodyHardLockCameraDataSubEditor(bodyFoldout));
                bodySubEditors.Add(new BodyTrackedDollyCameraDataSubEditor(bodyFoldout));
                CheckBodyVisibility();
            }

            {
                var aimFoldout = rootVisualElement.AddFoldout("Aim");

                {
                    var element = aimFoldout.AddEnumField(aimDataType, "", () =>
                    {
                        CheckAimVisibility();
                    });

                    RegisterLoadChange(element, aimDataType);
                }

                {
                    var element = aimFoldout.AddObjectField(baseAimScriptableObject, "Aim Scriptable Object");
                    element.SetEnabled(false);

                    RegisterLoadChange(element, baseAimScriptableObject);
                    AddElementToDebugView(element);
                }

                aimSubEditors.Add(new AimComposerCameraDataSubEditor(aimFoldout));
                aimSubEditors.Add(new AimGroupComposerCameraDataSubEditor(aimFoldout));
                aimSubEditors.Add(new AimPOVCameraDataSubEditor(aimFoldout));
                aimSubEditors.Add(new AimHardLookAtCameraDataSubEditor(aimFoldout));
                aimSubEditors.Add(new AimSameAsTargetCameraDataSubEditor(aimFoldout));
                CheckAimVisibility();
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

            {
                {
                    var element = rootVisualElement.AddLabel("Extensions");

                    element.style.unityFontStyleAndWeight = FontStyle.Bold;
                    element.style.marginLeft = new StyleLength(new Length(5, LengthUnit.Pixel));
                }

                /*{
                    var element = new DropdownField();

                    element.RegisterValueChangedCallback(callback =>
                    {
                        var index = element.index;
                        if (index > 0)
                            AddNewExtension(index);

                        element.index = 0;
                    });

                    element.choices = new List<string>(sExtensionNames);
                    element.index = 0;

                    rootVisualElement.Add(element);
                }*/

                {
                    var element = new EnumField();

                    element.Init(ExtensionDataType.none);

                    element.RegisterValueChangedCallback(callback =>
                    {
                        if ((ExtensionDataType)callback.newValue != ExtensionDataType.none)
                            AddNewExtension((ExtensionDataType)callback.newValue);

                        element.value = ExtensionDataType.none;
                    });

                    rootVisualElement.Add(element);
                }
            }
        }

        private void AddNewExtension(ExtensionDataType extensionDataType)
        {
            foreach(var subEditor in extensionSubEditors)
            {
                if (subEditor.IsSelectedEditor((int)extensionDataType))
                {
                    subEditor.Show();
                }
            }
        }

        private void CheckAimVisibility()
        {
            foreach (var subEditor in aimSubEditors)
            {
                subEditor.CheckVisibility((int)aimDataType.Value);
            }
        }

        private void CheckBodyVisibility()
        {
            foreach (var subEditor in bodySubEditors)
            {
                subEditor.CheckVisibility((int)bodyDataType.Value);
            }
        }

        protected override void LoadAsset(CameraDataScriptableObject asset, bool isNull)
        {
            foreach(var subEditor in extensionSubEditors)
            {
                subEditor.Hide();
            }

            if (isNull)
            {
                presetName.Reset();

                standbyUpdateMode.Reset();

                verticalFov.Reset();
                nearClipPlane.Reset();
                farClipPlane.Reset();
                dutch.Reset();

                bodyDataType.Reset();
                aimDataType.Reset();

                baseBodyScriptableObject.Reset();
                baseAimScriptableObject.Reset();

                foreach (var subEditor in bodySubEditors)
                {
                    subEditor.Load(true, null);
                }

                foreach (var subEditor in aimSubEditors)
                {
                    subEditor.Load(true, null);
                }

                foreach(var subEditor in extensionSubEditors)
                {
                    subEditor.Load(true, null);
                }
            }
            else
            {
                presetName.Value = asset.presetName;

                standbyUpdateMode.Value = asset.standbyUpdateMode;

                verticalFov.Value = asset.verticalFov;
                nearClipPlane.Value = asset.nearClipPlane;
                farClipPlane.Value = asset.farClipPlane;
                dutch.Value = asset.dutch;

                baseBodyScriptableObject.Value = asset.bodyCameraData;
                baseAimScriptableObject.Value = asset.aimCameraData;

                if (!asset.bodyCameraData)
                {
                    bodyDataType.Reset();
                }
                else
                {
                    foreach (var bodySubEditor in bodySubEditors)
                    {
                        if (bodySubEditor.IsSameType(asset.bodyCameraData.GetType()))
                        {
                            bodyDataType.Value = bodySubEditor.bodyDataType;
                            break;
                        }
                    }
                }

                if (!asset.aimCameraData)
                {
                    aimDataType.Reset();
                }
                else
                {
                    foreach (var aimSubEditor in aimSubEditors)
                    {
                        if (aimSubEditor.IsSameType(asset.aimCameraData.GetType()))
                        {
                            aimDataType.Value = aimSubEditor.aimDataType;
                            break;
                        }
                    }
                }

                foreach (var bodySubEditor in bodySubEditors)
                {
                    if (asset.bodyCameraData && bodySubEditor.IsSameType(asset.bodyCameraData.GetType()))
                        bodySubEditor.Load(false, asset.bodyCameraData);
                    else
                        bodySubEditor.Load(true, null);
                }

                foreach (var aimSubEditor in aimSubEditors)
                {
                    if (asset.aimCameraData && aimSubEditor.IsSameType(asset.aimCameraData.GetType()))
                        aimSubEditor.Load(false, asset.aimCameraData);
                    else
                        aimSubEditor.Load(true, null);
                }

                foreach(var subEditor in extensionSubEditors)
                {
                    foreach(var d in asset.extensions)
                    {
                        if (subEditor.IsSameType(d.GetType()))
                        {
                            subEditor.Load(false, d);
                            AddNewExtension(subEditor.extensionDataType);
                        }
                        else
                        {
                            subEditor.Load(true, null);
                        }
                    }
                }
            }
        }

        protected override void SaveAsset(ref CameraDataScriptableObject asset)
        {
            asset.presetName = presetName;

            asset.standbyUpdateMode = standbyUpdateMode;

            asset.verticalFov = verticalFov;
            asset.nearClipPlane = nearClipPlane;
            asset.farClipPlane = farClipPlane;
            asset.dutch = dutch;

            {
                bool found = false;
                foreach (var bodySubEditor in bodySubEditors)
                {
                    if (bodySubEditor.bodyDataType == bodyDataType)
                    {
                        var bodyCameraData = bodySubEditor.CreateScriptableObject();
                        AssetDatabase.CreateAsset(bodyCameraData, $"{SavePath}/{asset.guid} - Body Data.asset");
                        asset.bodyCameraData = bodyCameraData;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    if (asset.bodyCameraData)
                    {
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset.bodyCameraData));
                        asset.bodyCameraData = null;
                    }
                }
            }

            {
                bool found = false;
                foreach (var aimSubEditor in aimSubEditors)
                {
                    if (aimSubEditor.aimDataType == aimDataType)
                    {
                        var aimCameraData = aimSubEditor.CreateScriptableObject();
                        AssetDatabase.CreateAsset(aimCameraData, $"{SavePath}/{asset.guid} - Aim Data.asset");
                        asset.aimCameraData = aimCameraData;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    if (asset.aimCameraData)
                    {
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset.aimCameraData));
                        asset.aimCameraData = null;
                    }
                }
            }
        }

        protected override IEnumerable<CompletionCriteria> GetCompletionCriteria()
        {
            yield return new CompletionCriteria(!string.IsNullOrWhiteSpace(presetName.Value), "Name cannot be null");
        }

        protected override void OnDeleteAsset(CameraDataScriptableObject asset)
        {
            if (asset.bodyCameraData)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset.bodyCameraData));
            }

            if (asset.aimCameraData)
            {
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(asset.aimCameraData));
            }
        }

        public enum BodyDataType
        {
            [InspectorName("Do Nothing")]
            Nothing,
            [InspectorName("3rd Person Follow")]
            _3rdPersonFollow,
            [InspectorName("Framing Transposer")]
            FramingTransposer,
            [InspectorName("Hard Lock To Target")]
            HardLock,
            [InspectorName("Orbital Transposer")]
            OrbitalTransposer,
            [InspectorName("Tracked Dolly")]
            TrackedDolly,
            [InspectorName("Transposer")]
            Transposer
        }

        public enum AimDataType
        {
            [InspectorName("Do Nothing")]
            Nothing,
            [InspectorName("Composer")]
            Composer,
            [InspectorName("Group Composer")]
            GroupComposer,
            [InspectorName("Hard Look At")]
            HardLook,
            [InspectorName("POV")]
            POV,
            [InspectorName("Same As Follow Target")]
            FollowTarget
        }

        public enum ExtensionDataType
        {
            [InspectorName("(select)")]
            none,
            Collider
        }
    }
}