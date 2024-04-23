using ProjectSteppe.ZedExtensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectSteppe.UI
{
    public class ContextScreenUI : MonoBehaviour
    {
        private PlayerUIManager uiManager;
        private CanvasGroup canvasGroup;

        [SerializeField] private TextMeshProUGUI contextTMP;
        [SerializeField] private CanvasGroup blackFadeCG;

        private void Awake()
        {
            uiManager = GetComponentInParent<PlayerUIManager>();
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

            uiManager.playerDetails.HidePlayerDetails();
            uiManager.bossDetails.HideBossDetails();

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

            SceneManager.LoadScene(1);
        }

        public IEnumerator PlayRespiteFound(float waitTime = 2)
        {
            contextTMP.text = "<color=#FF8100>RESPITE KINDLED</color>";

            StartCoroutine(canvasGroup.FadeIn());

            yield return new WaitForSeconds(waitTime * 2);

            IEnumerator fadeOut = canvasGroup.FadeOut();
            yield return fadeOut;
        }
    }
}
