using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JungWonJu
{
    public class Menu_Button : ButtonEx
    {
        protected override void Start()
        {
            base.Start();

            Init();
        }

        public void Init()
        {
            UI_Menu uiMenu = transform.parent.parent.GetComponent<UI_Menu>();
            if (uiMenu != null)
            {
                _btnMy.onClick.AddListener(delegate ()
                {
                    uiMenu.ButtonClickEvent((int)_listIndex);
                });
            }
        }
    }
}