using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minwoo
{
    public class Box_SeedStore : MonoBehaviour
    {
        Button[] _buttons;

        public void Init()
        {
            _buttons = GetComponentsInChildren<Button>();

            if (_buttons.Length > 0)
            {
                foreach (Button btn in _buttons)
                {
                    Text txtMoney = btn.GetComponentInChildren<Text>();
                    int money = int.Parse(txtMoney.text);

                    btn.onClick.AddListener(delegate ()
                    {
                        // �÷��̾��� ���� ����
                        Common.Player player = NetworkManagerEx.I.GetMyPlayer();
                        if (player != null)
                        {
                            if(player._money >= money)
                            {
                                player._money -= money;

                                if(Common.Inventory.I != null)
                                {
                                    string name = btn.name + " Seed";
                                    // �κ��丮 ( ������ ��, ����[����Ʈ:1��] )
                                    Common.Inventory.I.AddInventory(name);

                                    // ���۹� ���� üũ
                                    Store store = transform.parent.GetComponent<Store>();
                                    if(store != null)
                                    {
                                        // ������ �̸����� ������ ���̺��� Ÿ���� ������
                                        ItemInfo info = GameDataManager.I._itemInfo.GetItemInfo(name);
                                        Sprite sprite = GameDataManager.I._spriteAssetItem.GetSprite(info.sprite_path);
                                        store._boxSeed.SetBox(sprite, info.name);
                                    }
                                }
                            }
                        }
                    });
                }
            }
        }
    }
}