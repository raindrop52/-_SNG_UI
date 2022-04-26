using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JungWonJu
{
    public class UI_PIMenu : UI_Base
    {
        EasyTween _tween;

        public override void Init()
        {
            base.Init();

            _tween = GetComponent<EasyTween>();

            if(_tween != null)
                _tween.ChangeSetState(false);
        }

        public void OpenCloseEvent(bool open = true)
        {
            if(_tween.IsObjectOpened() == open)
            {
                _tween.ChangeSetState(!open);
            }

            _tween.OpenCloseObjectAnimation();
        }

        public void PIButtonClickEvent(int index)
        {
            OpenCloseEvent(false);

            UIManager.I.ShowMenuEvent(index);
        }
    }
}