using ProjectSteppe.Entities;
using ProjectSteppe.Managers;
using ProjectSteppe.ZedExtensions;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class ContextScreenUI : MonoBehaviour
    {
        private PlayerUIManager uiManager;
        private CanvasGroup canvasGroup;

        [SerializeField] private TextMeshProUGUI contextTMP;
        [SerializeField] private CanvasGroup blackFadeCG;

        public AudioSource victoryImpact;
        public AudioSource defeatImpact;

        private void Awake()
        {
            uiManager = GetComponentInParent<PlayerUIManager>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void TriggerVictory(Entity killedEntity)
        {
            StartCoroutine(PlayVictory(killedEntity));
        }

        public void TriggerDefeat()
        {
            StartCoroutine(PlayDefeat());
        }

        public void TriggerRespite()
        {
            StartCoroutine(PlayRespiteFound());
        }

        public IEnumerator PlayVictory(Entity killedEntity, float waitTime = 2)
        {
            yield return new WaitForSeconds(waitTime);

            GameManager.Instance.playerManager.PlayerUI.playerDetails.HidePlayerDetails();
            killedEntity.EntityDetails.HideDetails();

            contextTMP.text = "FOE SLAIN";

            StartCoroutine(canvasGroup.FadeIn());

            victoryImpact.Play();

            yield return new WaitForSeconds(waitTime * 2);

            StartCoroutine(canvasGroup.FadeOut());
        }

        public IEnumerator PlayDefeat(float waitTime = 2)
        {
            yield return new WaitForSeconds(waitTime);

            contextTMP.text = "<color=red>YOU DIED</color>";

            StartCoroutine(canvasGroup.FadeIn());

            defeatImpact.Play();

            yield return new WaitForSeconds(waitTime * 2);

            StartCoroutine(blackFadeCG.FadeIn());

            yield return new WaitForSeconds(waitTime);

            GameManager.Instance.RespawnCharacter();
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
