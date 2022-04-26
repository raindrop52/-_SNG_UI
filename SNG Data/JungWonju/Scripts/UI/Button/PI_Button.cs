using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JungWonJu
{
    public class PI_Button : ButtonEx
    {
        GameObject _arrowImg;

        protected override void Start()
        {
            base.Start();

            _arrowImg = transform.Find("IconArrow").gameObject;

            ShowArrow(false);

            UI_PIMenu uiPI = transform.parent.GetComponent<UI_PIMenu>();
            if (uiPI != null)
            {
                _btnMy.onClick.AddListener(delegate()
                {
                    uiPI.PIButtonClickEvent((int)_listIndex);
                });
            }
        }

        public void Highlighted()
        {
            ShowArrow(true);
        }

        public void Normaled()
        {
            ShowArrow(false);
        }

        void ShowArrow(bool show)
        {
            if (_arrowImg != null)
            {
                _arrowImg.SetActive(show);
            }
        }
    }
}