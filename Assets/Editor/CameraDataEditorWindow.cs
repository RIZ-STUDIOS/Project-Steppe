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
                    var element = bodyFoldout.AddEnumField(bodyDataType, "");

                    RegisterLoadChange(element, bodyDataType);
                }

                {
                    var element = bodyFoldout.AddObjectField(baseBodyScriptableObject, "Body Scriptable Object");
                    element.SetEnabled(false);

                    AddElementToDebugView(element);
                }
            }

            {
                var aimFoldout = rootVisualElement.AddFoldout("Aim");

                {
                    var element = aimFoldout.AddEnumField(aimDataType, "");

                    RegisterLoadChange(element, aimDataType);
                }

                {
                    var element = aimFoldout.AddObjectField(baseAimScriptableObject, "Aim Scriptable Object");
                    element.SetEnabled(false);

                    AddElementToDebugView(element);
                }
            }
        }

        protected override void LoadAsset(CameraDataScriptableObject asset, bool isNull)
        {
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

                if (!asset.aimCameraData)
                {
                    aimDataType.Reset();
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
        }

        protected override IEnumerable<CompletionCriteria> GetCompletionCriteria()
        {
            yield return new CompletionCriteria(!string.IsNullOrWhiteSpace(presetName.Value), "Name cannot be null");
        }

        protected override void OnDeleteAsset(CameraDataScriptableObject asset)
        {
            base.OnDeleteAsset(asset);
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
    }
}