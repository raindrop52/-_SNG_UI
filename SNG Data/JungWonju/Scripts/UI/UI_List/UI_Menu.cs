using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JungWonJu
{
    public class UI_Menu : UI_Base
    {
        EasyTween _tween;

        public override void Init()
        {
            base.Init();

            _tween = GetComponent<EasyTween>();
        }

        public void ButtonClickEvent(int index)
        {
            EventOpenClose();

            UIManager.I.ShowMenuEvent(index);
        }

        public void EventOpenClose()
        {
            _tween.OpenCloseObjectAnimation();
        }
    }
}