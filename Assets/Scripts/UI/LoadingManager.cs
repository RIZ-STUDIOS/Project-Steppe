using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ProjectSteppe
{
    public class LoadingManager : MonoBehaviour
    {
        private static int targetBuildIndex;

        [SerializeField]
        private CanvasGroup canvasGroup;

        private AsyncOperation loadOperation;

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

            loadOperation = SceneManager.LoadSceneAsync(targetBuildIndex);
            loadOperation.allowSceneActivation = false;

            while (loadOperation.progress < 0.9f)
                yield return null;

            canvasGroup.alpha = 1;
        }

        private void OnSubmit(InputValue inputAction)
        {
            if (loadOperation == null) return;
            if (loadOperation.progress < 0.9f) return;
            var pressed = inputAction.Get<float>() != 0;
            loadOperation.allowSceneActivation = pressed;
        }
    }
}
