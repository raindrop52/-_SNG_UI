using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

namespace JungWonJu
{
    public class ItemInfo_UI : MonoBehaviour
    {
        // ������ ����
        ItemInfo _info;

        bool _isEquip = false;
        [SerializeField] GameObject _equipMarkGo;

        // ������ ������ ������� �� ������
        [SerializeField] Image _iconImg;
        [SerializeField] Text _txtName;
        [SerializeField] Text _txtType;
        [SerializeField] Text _txtSubName1;
        [SerializeField] Text _txtSubName2;
        [SerializeField] Text _txtSubData1;
        [SerializeField] Text _txtSubData2;
        [SerializeField] Text _txtDesc;

        [SerializeField] Button _btnUse;
        [SerializeField] Text _txtButtonUse;

        static string BUTTON_TEXT_EQUIP_KR = "����";
        static string BUTTON_TEXT_UNEQUIP_KR = "����";
        static string BUTTON_TEXT_UESABLE_KR = "���";

        public void Init()
        {
            // �ʱ� ��� ���� false ����
            ShowEquip();

            _btnUse = transform.Find("Btn_Use").GetComponent<Button>();
            _btnUse.onClick.AddListener(delegate ()
            {

            });
        }

        public void OnShow(bool show)
        {
            gameObject.SetActive(show);
        }

        public void SetUI_ItemInfo(ItemInfo info)
        {
            _info = info;

            SetIcon();
            SetText();
            ShowEquip();
        }

        void SetIcon()
        {
            if (_info != null)
            {
                _iconImg.sprite = GameDataManager.I._spriteAssetItem.GetSprite(_info.sprite_path);
            }
        }

        void SetText()
        {
            if (_info != null)
            {
                // ���� ������ ���� �������� �ʵ��� ����
                ShowTextSubItem(false);

                _txtName.text = _info.name;
                _txtType.text = GetItemTypeString(_info.type);

                if (_info.type == (int)Item_Type.EQUIP)
                {
                    ItemInfo_Equip itemInfo = _info.sub_info as ItemInfo_Equip;
                    SetSubItemData(itemInfo.attack, itemInfo.defense);
                    // ���� ������ ���� ������
                    ShowTextSubItem(true);
                }

                _txtDesc.text = _info.desc;

                ChangeButtonUseText();
            }
        }

        void ShowTextSubItem(bool show)
        {
            _txtSubData1.gameObject.SetActive(show);
            _txtSubData2.gameObject.SetActive(show);
            _txtSubName1.gameObject.SetActive(show);
            _txtSubName2.gameObject.SetActive(show);
        }

        void SetSubItemData(object o1, object o2)
        {
            _txtSubData1.text = string.Format("{0}", o1);
            _txtSubData2.text = string.Format("{0}", o2);
        }

        string GetItemTypeString(int index)
        {
            string result;
            Item_Type type = (Item_Type)index;

            if (type == Item_Type.EQUIP)
            {
                result = "���";
            }
            else if (type == Item_Type.CONSUMABLE)
            {
                result = "�Һ�";
            }
            else
            {
                result = "��Ÿ";
            }

            return result;
        }

        void ShowEquip()
        {
            if(_info != null)
            {
                Inventory_Data data = Inventory.I.FindInventoryData(_info.name);
                if (data != null)
                    _isEquip = data.isEquip;
            }

            if (_equipMarkGo != null)
                _equipMarkGo.SetActive(_isEquip);
        }

        void ChangeButtonUseText()
        {
            if (_info != null)
            {
                // �⺻ Ȱ��ȭ
                _btnUse.gameObject.SetActive(true);

                if (_info.type == (int)Item_Type.EQUIP)
                {
                    if (_isEquip == true)
                    {
                        _txtButtonUse.text = BUTTON_TEXT_UNEQUIP_KR;
                    }
                    else
                    {
                        _txtButtonUse.text = BUTTON_TEXT_EQUIP_KR;
                    }
                }
                else if (_info.type == (int)Item_Type.CONSUMABLE)
                {
                    _txtButtonUse.text = BUTTON_TEXT_UESABLE_KR;
                }
                else
                {
                    // ���, �Һ� Ÿ���� �ƴ� ��� ��Ȱ��ȭ
                    _btnUse.gameObject.SetActive(false);
                }
            }
        }
    }
}