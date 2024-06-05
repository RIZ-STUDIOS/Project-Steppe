using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProjectSteppe
{
    [ExecuteAlways]
    public class ScreenshotTaker : MonoBehaviour
    {
#if UNITY_EDITOR
        private bool takeScreenshot;

        private List<Canvas> canvases = new List<Canvas>();

        [ContextMenu("Take Screenshot")]
        private void TakeScreenshot()
        {
            takeScreenshot = true;
        }

        private void OnEnable()
        {
            RenderPipelineManager.beginCameraRendering += RenderPipelineManager_beginCameraRendering;
            RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
        }

        private void RenderPipelineManager_beginCameraRendering(ScriptableRenderContext arg1, Camera arg2)
        {
            if (arg2.name != "SceneCamera") return;
            if(takeScreenshot)
            {
                var c = GameObject.FindObjectsOfType<Canvas>();
                canvases.Clear();
                foreach(var canvas in c)
                {
                    if(canvas.renderMode != RenderMode.WorldSpace)
                    {
                        canvases.Add(canvas);
                        canvas.enabled = false;
                    }
                }
            }
        }

        private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext arg1, Camera arg2)
        {
            if (arg2.name != "SceneCamera") return;
            if (takeScreenshot)
            {
                takeScreenshot = false;
                int width = arg2.scaledPixelWidth;
                int height = arg2.scaledPixelHeight;
                var screenshotTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);
                var rect = new Rect(0, 0, width, height);

                screenshotTexture.ReadPixels(rect, 0, 0);
                screenshotTexture.Apply();

                byte[] byteArray = screenshotTexture.EncodeToPNG();
                System.IO.File.WriteAllBytes(Application.dataPath + "/screenshot.png", byteArray);

                foreach(var canvas in canvases)
                {
                    canvas.enabled = true;
                }

                Debug.Log("Screenshot taken!");
            }
        }

        private void OnDisable()
        {
            RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
            RenderPipelineManager.beginCameraRendering -= RenderPipelineManager_beginCameraRendering;
        }
#else
        private void OnEnable(){
            Destroy(this);
        }
#endif
    }
}
