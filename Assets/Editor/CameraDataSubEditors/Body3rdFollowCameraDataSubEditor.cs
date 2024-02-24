using ProjectSteppe.ScriptableObjects.CameraData.BodyCameraData;
using RicTools.Editor.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    public class Body3rdFollowCameraDataSubEditor : BodyCameraDataSubEditor
    {
        public EditorContainer<Vector3> damping = new EditorContainer<Vector3>(new Vector3(0.1f, 0.5f, 0.3f));
        public EditorContainer<Vector3> shoulderOffset = new EditorContainer<Vector3>(new Vector3(0.5f, -0.4f, 0));
        public EditorContainer<float> verticalArmLength = new EditorContainer<float>(0.4f);
        public EditorContainer<float> cameraSide = new EditorContainer<float>(1);
        public EditorContainer<float> cameraDistance = new EditorContainer<float>(2);
        public EditorContainer<LayerMask> cameraCollisionFilter = new EditorContainer<LayerMask>();
        public EditorContainer<string> ignoreTag = new EditorContainer<string>(InternalEditorUtility.tags[0]);
        public EditorContainer<float> cameraRadius = new EditorContainer<float>(0.2f);
        public EditorContainer<float> dampingIntoCollision = new EditorContainer<float>();
        public EditorContainer<float> dampingFromCollision = new EditorContainer<float>(2);

        public Body3rdFollowCameraDataSubEditor(VisualElement rootVisualElement) : base(rootVisualElement, CameraDataEditorWindow.BodyDataType._3rdPersonFollow, typeof(Body3rdFollowCameraDataScriptableObject))
        {
        }

        public override BaseBodyCameraDataScriptableObject CreateScriptableObject()
        {
            var asset = ScriptableObject.CreateInstance<Body3rdFollowCameraDataScriptableObject>();

            asset.damping = damping;
            asset.shoulderOffset = shoulderOffset;
            asset.verticalArmLength=verticalArmLength;
            asset.cameraSide = cameraSide;
            asset.cameraDistance = cameraDistance;
            asset.cameraCollisionFilter = cameraCollisionFilter;
            asset.cameraRadius = cameraRadius;
            asset.dampingIntoCollision = dampingIntoCollision;
            asset.dampingFromCollision = dampingFromCollision;

            asset.ignoreTag = ignoreTag == InternalEditorUtility.tags[0] ? "" : ignoreTag;

            return asset;
        }

        protected override void CreateGUI()
        {
            {
                var element = rootVisualElement.AddVector3Field(damping, "Damping");

                RegisterLoadChange(element, damping);
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

            rootVisualElement.AddLabel("Rig").style.unityFontStyleAndWeight = FontStyle.Bold;

            {
                var element = rootVisualElement.AddVector3Field(shoulderOffset, "Shoulder Offset");

                element.RegisterValueChangedCallback(callback =>
                {
                    var value = shoulderOffset.Value;

                    if (value.x < 0)
                        value.x = 0;

                    if (value.y < 0)
                        value.y = 0;

                    if(value.z < 0)
                        value.z = 0;

                    shoulderOffset.Value = value;
                    element.value = shoulderOffset;
                });

                RegisterLoadChange(element, shoulderOffset);
            }

            {
                var element = rootVisualElement.AddFloatField(verticalArmLength, "Vertical Arm Length");

                RegisterLoadChange(element, verticalArmLength);
            }

            {
                var element = rootVisualElement.AddSlider(cameraSide, 0, 1, "Camera Side");

                RegisterLoadChange(element, cameraSide);
            }

            {
                var element = rootVisualElement.AddFloatField(cameraSide, "Camera Side");

                RegisterLoadChange(element, cameraSide);
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

            rootVisualElement.AddLabel("Obstacles").style.unityFontStyleAndWeight = FontStyle.Bold;

            {
                var element = rootVisualElement.AddLayerMask(cameraCollisionFilter, "Camera Collision Filter");

                RegisterLoadChange(element, cameraCollisionFilter);
            }

            {
                var element = rootVisualElement.AddTagField(ignoreTag, "Ignore Tag");

                RegisterLoadChange(element, ignoreTag);
            }

            {
                var element = rootVisualElement.AddSlider(cameraRadius, 0.001f, 1, "Camera Radius");

                RegisterLoadChange(element, cameraRadius);
            }

            {
                var element = rootVisualElement.AddSlider(dampingIntoCollision, 0, 10, "Damping Into Collision");

                RegisterLoadChange(element, dampingIntoCollision);
            }

            {
                var element = rootVisualElement.AddSlider(dampingFromCollision, 0, 10, "Damping From Collision");

                RegisterLoadChange(element, dampingFromCollision);
            }
        }

        protected override void LoadData(bool isNull, BaseBodyCameraDataScriptableObject asset)
        {
            var body = asset as Body3rdFollowCameraDataScriptableObject;
            if (isNull)
            {
                damping.Reset();
                shoulderOffset.Reset();
                verticalArmLength.Reset();
                cameraSide.Reset();
                cameraDistance.Reset();
                cameraCollisionFilter.Reset();
                ignoreTag.Reset();
                cameraRadius.Reset();
                dampingIntoCollision.Reset();
                dampingFromCollision.Reset();
            }
            else
            {
                if (string.IsNullOrEmpty(body.ignoreTag))
                {
                    ignoreTag.Reset();
                }
                else
                {
                    ignoreTag.Value = body.ignoreTag;
                }

                damping.Value = body.damping;
                shoulderOffset.Value = body.shoulderOffset;
                verticalArmLength.Value = body.verticalArmLength;
                cameraSide.Value = body.cameraSide;
                cameraDistance.Value = body.cameraDistance;

                cameraCollisionFilter.Value = body.cameraCollisionFilter;
                cameraRadius.Value = body.cameraRadius;
                dampingIntoCollision.Value = body.dampingIntoCollision;
                dampingFromCollision.Value = body.dampingFromCollision;
            }
        }
    }
}
