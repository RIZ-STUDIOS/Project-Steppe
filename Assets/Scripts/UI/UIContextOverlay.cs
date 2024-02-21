using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectSteppe.UI
{
    public class UIContextOverlay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI contextText;

        public void DisplayContext(bool deathOrBoss)
        {
            string context = deathOrBoss ? "<color=red>YOU DIED</color>" : "FOE SLAIN";

            contextText.text = context;

            GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
