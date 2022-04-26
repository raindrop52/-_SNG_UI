using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

namespace JungWonJu
{
    public class Grid_Inventory : MonoBehaviour
    {
        GameObject _owner;
        GameObject _itemFramePrefab;
        [SerializeField] int _defaultPutCount;      // �ʱ� ��ġ �� ����
        [SerializeField] List<ItemIcon> _items;

        public void Init(GameObject owner)
        {
            _owner = owner;

            _itemFramePrefab = Resources.Load("ItemFrame") as GameObject;

            if (_itemFramePrefab != null)
            {
                for (int i = 0; i < _defaultPutCount; i++)
                {
                    // ������ �Ҵ�
                    GameObject itemGo = Instantiate(_itemFramePrefab);
                    // �׸����� �ڽ� ��ü�� ���� �� ���� ������ 1�� ����
                    itemGo.transform.parent = transform;
                    itemGo.transform.localScale = new Vector3(1f, 1f, 1f);

                    // ������ ����
                    ItemIcon icon = itemGo.GetComponent<ItemIcon>();
                    if(icon != null)
                    {
                        // ������ �ʱ�ȭ
                        icon.Init(owner);

                        _items.Add(icon);
                    }
                }
            }
        }

        public void SetClear(int index)
        {
            // ��ȣ�� �´� ��ġ ����
            ItemIcon icon = _items[index];

            icon.Clear();
        }

        public void SetItemInfo(int index, Inventory_Data data)
        {
            GameDataManager gameDataMgr = GameDataManager.I;

            // ��ȣ�� �´� ��ġ ����
            ItemIcon icon = _items[index];

            // ������ �ִ� ������ ������ �̸����� ��������
            ItemInfo info = gameDataMgr._itemInfo.GetItemInfo(data.name);

            icon.SetItemInfo(info);
            Sprite sprite = gameDataMgr._spriteAssetItem.GetSprite(info.sprite_path);
            icon.SetSprite(sprite);

            icon.SetItemCount(data.count);
        }
    }
}