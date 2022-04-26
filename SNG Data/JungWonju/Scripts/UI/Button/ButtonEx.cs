using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JungWonJu
{
    public class ButtonEx : MonoBehaviour
    {
        public UI_LIST _listIndex;
        protected Button _btnMy;

        protected virtual void Start()
        {
            _btnMy = GetComponent<Button>();
        }

        void Update()
        {

        }
    }
}