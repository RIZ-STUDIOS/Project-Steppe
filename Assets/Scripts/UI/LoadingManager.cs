using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ProjectSteppe
{
    public class LoadingManager : MonoBehaviour
    {
        private static int targetBuildIndex = 0;

        private AsyncOperation loadOperation;

        [SerializeField]
        private CanvasGroup overlayImage;

        [SerializeField]
        private CanvasGroup loadingIcon;

        [SerializeField]
        private CanvasGroup loadingIcon2;

        [SerializeField]
        private float rotationTime;

        [SerializeField]
        private float rotationTime2;

        private int loadingIconIndex;

        private bool transitioning;

        public static void LoadScene(int buildIndex)
        {
            targetBuildIndex = buildIndex;
            SceneManager.LoadScene(2);
        }

        private void Start()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return null;

            float alpha = 1;
            overlayImage.alpha = alpha;

            while(alpha > 0)
            {
                alpha -= Time.deltaTime * 2;
                overlayImage.alpha = alpha;
                yield return null;
            }

            alpha = 0;
            overlayImage.alpha = alpha;

            yield return null;

            loadOperation = SceneManager.LoadSceneAsync(targetBuildIndex);
            loadOperation.allowSceneActivation = false;

            while (loadOperation.progress < 0.9f)
                yield return null;

            yield return new WaitForSeconds(3f);

            alpha = 0;
            overlayImage.alpha = alpha;

            while (alpha < 1)
            {
                alpha += Time.deltaTime * 2;
                overlayImage.alpha = alpha;
                yield return null;
            }

            alpha = 1;
            overlayImage.alpha = alpha;

            loadOperation.allowSceneActivation = true;
        }

        private void Update()
        {
            if (transitioning) return;
            if (loadingIconIndex == 0)
            {
                float rotation = loadingIcon.transform.eulerAngles.z;

                rotation += Time.deltaTime * rotationTime;

                loadingIcon.transform.eulerAngles = new Vector3(0, 0, rotation);

                if (rotation >= 180f)
                {
                    StartCoroutine(ChangeCanvasGroup(loadingIcon, loadingIcon2, 3));
                    loadingIconIndex = 1;
                    rotation = 0;

                    loadingIcon.transform.eulerAngles = new Vector3(0, 0, rotation);

                }
            }
            else
            {
                float rotation = loadingIcon2.transform.eulerAngles.z;

                rotation -= Time.deltaTime * rotationTime2;

                loadingIcon2.transform.eulerAngles = new Vector3(0, 0, rotation);

                if (rotation <= 0)
                {
                    StartCoroutine(ChangeCanvasGroup(loadingIcon2, loadingIcon, 3));
                    loadingIconIndex = 0;
                    rotation = 180;

                    loadingIcon2.transform.eulerAngles = new Vector3(0, 0, rotation);
                }
            }
        }

        private IEnumerator ChangeCanvasGroup(CanvasGroup start, CanvasGroup end, float speed)
        {
            transitioning = true;

            float alpha = 1;
            start.alpha = alpha;
            end.alpha = 1 - alpha;

            while(alpha > 0)
            {
                alpha -= Time.deltaTime * speed;
                start.alpha = alpha;
                end.alpha = 1 - alpha;

                yield return null;
            }

            alpha = 0;
            start.alpha = alpha;
            end.alpha = 1 - alpha;

            transitioning = false;
        }
    }
}
