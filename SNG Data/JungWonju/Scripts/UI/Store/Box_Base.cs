using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Minwoo
{
    public class Box_Base : MonoBehaviour
    {
        protected Transform _prefabParent;
        protected GameObject _prefab;

        public virtual void Init()
        {
            _prefabParent = transform.Find("Grid");
        }
    }
}