using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JungWonJu
{
    public class UI_Base : MonoBehaviour
    {
        public virtual void Init()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void OnEnable()
        {

        }

        public void OnShow(bool show)
        {
            gameObject.SetActive(show);
        }
    }
}