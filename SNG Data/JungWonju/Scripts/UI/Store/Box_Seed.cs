using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Minwoo
{
    public class Box_Seed : Box_Base
    {
        List<SeedInven> _boxs;

        public override void Init()
        {
            base.Init();

            _prefab = Resources.Load("Seed") as GameObject;

            _boxs = new List<SeedInven>();
        }

        // �κ��丮 ������ ������ �ڽ� �߰� (�ѱ� ��Ī �ʿ�)
        public void SetBox(Sprite sprite, string name)
        {
            if (IsCheckDuplicate(name) == true)
                return;

            GameObject go = Instantiate(_prefab);
            go.transform.parent = _prefabParent;
            go.transform.localScale = new Vector3(1f, 1f, 1f);

            SeedInven box = go.GetComponent<SeedInven>();
            box.Init();

            box.SetInfo(sprite, name);

            _boxs.Add(box);
        }

        bool IsCheckDuplicate(string name)
        {
            foreach (SeedInven box in _boxs)
            {
                if (box.Name.Equals(name))
                {
                    return true;
                }
            }

            return false;
        }
    }
}