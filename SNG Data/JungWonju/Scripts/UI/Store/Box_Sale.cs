using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Minwoo
{
    public class Box_Sale : Box_Base
    {
        List<SellBox> _boxs;

        public override void Init()
        {
            base.Init();

            _prefab = Resources.Load("SellBox") as GameObject;

            _boxs = new List<SellBox>();
        }

        // 인벤토리 정보를 가지고 박스 추가
        public void SetBox(Sprite sprite, int money, string name)
        {
            if (IsCheckDuplicate(name))
                return;

            GameObject go = Instantiate(_prefab);
            go.transform.parent = _prefabParent;
            go.transform.localScale = new Vector3(1f, 1f, 1f);

            SellBox box = go.GetComponent<SellBox>();
            box.Init();
                        
            box.SetInfo(sprite, money, name);
            _boxs.Add(box);
        }

        bool IsCheckDuplicate(string name)
        {
            foreach (SellBox box in _boxs)
            {
                if (box.Name.Equals(name))
                {
                    return true;
                }
            }

            return false;
        }

        public void AllSell()
        {
            foreach(SellBox box in _boxs)
            {
                box.OnSell();
            }
        }
    }
}