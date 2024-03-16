using ProjectSteppe.ZedExtensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSteppe.UI
{
    public class ContextScreenUI : MonoBehaviour
    {
        private CanvasGroup canvasGroup;

        [SerializeField] private TextMeshProUGUI contextTMP;
        [SerializeField] private CanvasGroup blackFadeCG;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void TriggerVictory()
        {
            StartCoroutine(PlayVictory());
        }

        public void TriggerDefeat()
        {
            StartCoroutine(PlayDefeat());
        }

        public void TriggerRespite()
        {
            StartCoroutine(PlayRespiteFound());
        }

        public IEnumerator PlayVictory(float waitTime = 2)
        {
            yield return new WaitForSeconds(waitTime);

            contextTMP.text = "FOE SLAIN";

            StartCoroutine(canvasGroup.FadeIn());

            yield return new WaitForSeconds(waitTime * 2);

            StartCoroutine(canvasGroup.FadeOut());
        }

        public IEnumerator PlayDefeat(float waitTime = 2)
        {
            yield return new WaitForSeconds(waitTime);

            contextTMP.text = "<color=red>YOU DIED</color>";

            StartCoroutine(canvasGroup.FadeIn());

            yield return new WaitForSeconds(waitTime * 2);

            StartCoroutine(blackFadeCG.FadeIn());

            yield return new WaitForSeconds(waitTime);

            SceneManager.LoadScene(0);
        }

        public IEnumerator PlayRespiteFound(float waitTime = 2)
        {
            contextTMP.text = "<color=#FF8100>RESPITE REKINDLED</color>";

            StartCoroutine(canvasGroup.FadeIn());

            yield return new WaitForSeconds(waitTime * 2);

            StartCoroutine(canvasGroup.FadeOut());

            yield return new WaitForSeconds(waitTime);
        }
    }
}
