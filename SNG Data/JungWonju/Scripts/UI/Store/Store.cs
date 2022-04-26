using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

namespace Minwoo
{
    public class Store : MonoBehaviour
    {
        public Box_SeedStore _boxStore;
        public Box_Sale _boxSale;
        public Box_Seed _boxSeed;

        private void Start()
        {
            _boxStore = GetComponentInChildren<Box_SeedStore>();
            if(_boxStore != null)
            {
                _boxStore.Init();
            }

            Box_Base[] boxs = GetComponentsInChildren<Box_Base>();
            if(boxs.Length > 0)
            {
                foreach(Box_Base box in boxs)
                {
                    if (box is Box_Sale)
                        _boxSale = box as Box_Sale;
                    else if (box is Box_Seed)
                        _boxSeed = box as Box_Seed;

                    box.Init();
                }
            }

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if(gameObject.activeSelf == true)
            {
                if(Inventory.I != null)
                {
                    int max = Inventory.I.GetInventoryCount();

                    for (int i = 0; i < max; i++)
                    {
                        // �κ��丮 �� ������ ��Ī ������
                        Inventory_Data data = Inventory.I.GetInventoryData(i);
                        string name = data.name;

                        // ������ �̸����� ������ ���̺��� Ÿ���� ������
                        ItemInfo info = GameDataManager.I._itemInfo.GetItemInfo(name);
                        if(info != null)
                        {
                            ItemInfo_Farm info_Farm = info.sub_info as ItemInfo_Farm;
                            if (info_Farm != null)
                            {
                                bool isSeed = info_Farm.isSeed;
                                Sprite sprite = GameDataManager.I._spriteAssetItem.GetSprite(info.sprite_path);
                                
                                if (isSeed == true)
                                {
                                    // ���� ������ ����
                                    _boxSeed.SetBox(sprite, name);
                                }
                                else
                                {
                                    // �۹� ������ ����
                                    _boxSale.SetBox(sprite, info_Farm.money, name);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}