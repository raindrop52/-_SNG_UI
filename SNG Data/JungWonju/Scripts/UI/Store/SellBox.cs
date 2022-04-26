using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

namespace Minwoo
{
    public class SellBox : MonoBehaviour
    {
        // �۹� ������
        Image _icon;
        // ��ȭ ������
        Image _moneyIcon;
        [SerializeField] Sprite _spriteMoney;
        string _name;       // �۹� ��
        public string Name
        { get { return _name; } }

        // �Ǹ� �ݾ�
        Text _txtSellMoney;

        public void Init()
        {
            _icon = transform.Find("Icon").GetComponent<Image>();
            _moneyIcon = transform.Find("MoneyIcon").GetComponent<Image>();
            _moneyIcon.sprite = _spriteMoney;
            _txtSellMoney = transform.Find("SellMoney").GetComponent<Text>();

            // Default �� ""
            _txtSellMoney.text = "5";
        }

        public void SetInfo(Sprite sprite, int money, string name)
        {
            _icon.sprite = sprite;

            _txtSellMoney.text = string.Format("{0}", money);

            _name = name;
        }

        public void OnSell()
        {
            Inventory_Data data = Inventory.I.FindInventoryData(_name);
            if(data != null)
            {
                Common.Player player = NetworkManagerEx.I.GetMyPlayer();

                if(player != null)
                {
                    int count = data.count;
                    int sellMoney = int.Parse(_txtSellMoney.text);

                    int getMoney = sellMoney * count;

                    player._money += getMoney;

                    Inventory.I.RemoveInventoryData(_name);

                    Destroy(gameObject);
                }
            }
        }
    }
}