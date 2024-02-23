using ProjectSteppe.ScriptableObjects.CameraData.AimCameraData;
using RicTools.Editor.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectSteppe.Editor.CameraDataSubEditors
{
    public class AimComposerCameraDataSubEditor : AimCameraDataSubEditor
    {
        public EditorContainer<Vector3> trackedObjectOffset = new EditorContainer<Vector3>();
        public EditorContainer<float> lookaheadTime = new EditorContainer<float>();
        public EditorContainer<float> lookaheadSmoothing = new EditorContainer<float>();
        public EditorContainer<bool> lookaheadIgnoreY = new EditorContainer<bool>();

        public EditorContainer<float> horizontalDamping = new EditorContainer<float>(0.5f);
        public EditorContainer<float> verticalDamping = new EditorContainer<float>(0.5f);

        public EditorContainer<float> screenX = new EditorContainer<float>(0.5f);
        public EditorContainer<float> screenY = new EditorContainer<float>(0.5f);

        public EditorContainer<float> deadZoneWidth = new EditorContainer<float>();
        public EditorContainer<float> deadZoneHeight = new EditorContainer<float>();
        public EditorContainer<float> softZoneWidth = new EditorContainer<float>(0.8f);
        public EditorContainer<float> softZoneHeight = new EditorContainer<float>(0.8f);
        public EditorContainer<float> biasX = new EditorContainer<float>();
        public EditorContainer<float> biasY = new EditorContainer<float>();

        public EditorContainer<bool> centerOnActivate = new EditorContainer<bool>(true);

        public AimComposerCameraDataSubEditor(VisualElement rootVisualElement) : base(rootVisualElement, CameraDataEditorWindow.AimDataType.Composer, typeof(AimComposerCameraDataScriptableObject))
        {
        }

        protected override void CreateGUI()
        {
            {
                var element = rootVisualElement.AddVector3Field(trackedObjectOffset, "Tracked Object Offset");

                RegisterLoadChange(element, trackedObjectOffset);
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

            {
                var element = rootVisualElement.AddSlider(lookaheadTime, 0, 1, "Lookahead Time");
                RegisterLoadChange(element, lookaheadTime);
            }

            {
                var element = rootVisualElement.AddSlider(lookaheadSmoothing, 0, 30, "Lookahead Smoothing");
                RegisterLoadChange(element, lookaheadSmoothing);
            }

            {
                var element = rootVisualElement.AddToggle(lookaheadIgnoreY, "Lookahead Ignore Y");
                RegisterLoadChange(element, lookaheadIgnoreY);
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

            {
                var element = rootVisualElement.AddSlider(horizontalDamping, 0, 20, "Horizontal Damping");
                RegisterLoadChange(element, horizontalDamping);
            }

            {
                var element = rootVisualElement.AddSlider(verticalDamping, 0, 20, "Vertical Damping");
                RegisterLoadChange(element, verticalDamping);
            }

            rootVisualElement.AddSeparator().style.backgroundImage = null;

            {
                var element = rootVisualElement.AddSlider(screenX, -0.5f, 1.5f, "Screen X");
                RegisterLoadChange(element, screenX);
            }

            {
                var element = rootVisualElement.AddSlider(screenY, -0.5f, 1.5f, "Screen Y");
                RegisterLoadChange(element, screenY);
            }

            {
                var element = rootVisualElement.AddSlider(deadZoneWidth, 0, 2, "Dead Zone Width");
                RegisterLoadChange(element, deadZoneWidth);
            }

            {
                var element = rootVisualElement.AddSlider(deadZoneHeight, 0, 2, "Dead Zone Height");
                RegisterLoadChange(element, deadZoneHeight);
            }

            {
                var element = rootVisualElement.AddSlider(softZoneWidth, 0, 2, "Soft Zone Width");
                RegisterLoadChange(element, softZoneWidth);
            }

            {
                var element = rootVisualElement.AddSlider(softZoneHeight, 0, 2, "Soft Zone Height");
                RegisterLoadChange(element, softZoneHeight);
            }

            {
                var element = rootVisualElement.AddSlider(biasX, -0.5f, 0.5f, "Bias X");
                RegisterLoadChange(element, biasX);
            }

            {
                var element = rootVisualElement.AddSlider(biasY, -0.5f, 0.5f, "Bias Y");
                RegisterLoadChange(element, biasY);
            }

            {
                var element = rootVisualElement.AddToggle(centerOnActivate, "Center On Activate");
                RegisterLoadChange(element, centerOnActivate);
            }
        }

        public override BaseAimCameraDataScriptableObject CreateScriptableObject()
        {
            var asset = ScriptableObject.CreateInstance<AimComposerCameraDataScriptableObject>();

            asset.trackedObjectOffset = trackedObjectOffset;
            asset.lookaheadTime = lookaheadTime;
            asset.lookaheadSmoothing = lookaheadSmoothing;
            asset.lookaheadIgnoreY = lookaheadIgnoreY;
            asset.horizontalDamping = horizontalDamping;
            asset.verticalDamping = verticalDamping;
            asset.screenX = screenX;
            asset.screenY = screenY;
            asset.deadZoneWidth = deadZoneWidth;
            asset.deadZoneHeight = deadZoneHeight;
            asset.softZoneWidth = softZoneWidth;
            asset.softZoneHeight = softZoneHeight;
            asset.biasX = biasX;
            asset.biasY = biasY;
            asset.centerOnActivate = centerOnActivate;

            return asset;
        }

        protected override void LoadData(bool isNull, BaseAimCameraDataScriptableObject asset)
        {
            var aim = asset as AimComposerCameraDataScriptableObject;
            if (isNull)
            {
                trackedObjectOffset.Reset();
                lookaheadTime.Reset();
                lookaheadSmoothing.Reset();
                lookaheadIgnoreY.Reset();
                horizontalDamping.Reset();
                verticalDamping.Reset();
                screenX.Reset();
                screenY.Reset();
                deadZoneWidth.Reset();
                deadZoneHeight.Reset();
                softZoneWidth.Reset();
                softZoneHeight.Reset();
                biasX.Reset();
                biasY.Reset();
                centerOnActivate.Reset();
            }
            else
            {
                trackedObjectOffset.Value = aim.trackedObjectOffset;
                lookaheadTime.Value = aim.lookaheadTime;
                lookaheadSmoothing.Value = aim.lookaheadSmoothing;
                lookaheadIgnoreY.Value = aim.lookaheadIgnoreY;
                horizontalDamping.Value = aim.horizontalDamping;
                verticalDamping.Value = aim.verticalDamping;
                screenX.Value = aim.screenX;
                screenY.Value = aim.screenY;
                deadZoneWidth.Value = aim.deadZoneWidth;
                deadZoneHeight.Value = aim.deadZoneHeight;
                softZoneWidth.Value = aim.softZoneWidth;
                softZoneHeight.Value = aim.softZoneHeight;
                biasX.Value = aim.biasX;
                biasY.Value = aim.biasY;
                centerOnActivate.Value = aim.centerOnActivate;
            }
        }
    }
}
