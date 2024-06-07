using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSteppe
{
    public class DebugOnlyButton : MonoBehaviour
    {
        [SerializeField]
        private RectTransform buttonsLayout;
#if !UNITY_EDITOR
        private void Awake()
        {
            var pos = buttonsLayout.anchoredPosition;
            pos.y = 375;
            buttonsLayout.anchoredPosition = pos;

            var button = GetComponent<Button>();
            var up = button.navigation.selectOnUp;
            var down = button.navigation.selectOnDown;
            var left = button.navigation.selectOnLeft;
            var right = button.navigation.selectOnRight;

            if(up){
                var nav = up.navigation;

                nav.selectOnDown = down;

                up.navigation = nav;
            }

            if(down){
                var nav = down.navigation;

                nav.selectOnUp = up;

                down.navigation = nav;
            }

            if(left){
                var nav = left.navigation;

                nav.selectOnRight = right;

                left.navigation = nav;
            }

            if(right){
                var nav = right.navigation;

                nav.selectOnLeft = left;

                right.navigation = nav;
            }

            Destroy(gameObject);
        }
#endif
    }
}
