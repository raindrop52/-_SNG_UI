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
                        // 플레이어의 돈을 깎음
                        Common.Player player = NetworkManagerEx.I.GetMyPlayer();
                        if (player != null)
                        {
                            if(player._money >= money)
                            {
                                player._money -= money;

                                if(Common.Inventory.I != null)
                                {
                                    string name = btn.name + " Seed";
                                    // 인벤토리 ( 아이템 명, 갯수[디폴트:1개] )
                                    Common.Inventory.I.AddInventory(name);

                                    // 농작물 유무 체크
                                    Store store = transform.parent.GetComponent<Store>();
                                    if(store != null)
                                    {
                                        // 아이템 이름으로 아이템 테이블에서 타입을 가져옴
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