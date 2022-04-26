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
        [SerializeField] int _defaultPutCount;      // 초기 배치 될 갯수
        [SerializeField] List<ItemIcon> _items;

        public void Init(GameObject owner)
        {
            _owner = owner;

            _itemFramePrefab = Resources.Load("ItemFrame") as GameObject;

            if (_itemFramePrefab != null)
            {
                for (int i = 0; i < _defaultPutCount; i++)
                {
                    // 프리팹 할당
                    GameObject itemGo = Instantiate(_itemFramePrefab);
                    // 그리드의 자식 객체로 설정 및 로컬 스케일 1로 지정
                    itemGo.transform.parent = transform;
                    itemGo.transform.localScale = new Vector3(1f, 1f, 1f);

                    // 아이콘 변경
                    ItemIcon icon = itemGo.GetComponent<ItemIcon>();
                    if(icon != null)
                    {
                        // 아이콘 초기화
                        icon.Init(owner);

                        _items.Add(icon);
                    }
                }
            }
        }

        public void SetClear(int index)
        {
            // 번호에 맞는 위치 설정
            ItemIcon icon = _items[index];

            icon.Clear();
        }

        public void SetItemInfo(int index, Inventory_Data data)
        {
            GameDataManager gameDataMgr = GameDataManager.I;

            // 번호에 맞는 위치 설정
            ItemIcon icon = _items[index];

            // 가지고 있는 아이템 정보를 이름으로 가져오기
            ItemInfo info = gameDataMgr._itemInfo.GetItemInfo(data.name);

            icon.SetItemInfo(info);
            Sprite sprite = gameDataMgr._spriteAssetItem.GetSprite(info.sprite_path);
            icon.SetSprite(sprite);

            icon.SetItemCount(data.count);
        }
    }
}