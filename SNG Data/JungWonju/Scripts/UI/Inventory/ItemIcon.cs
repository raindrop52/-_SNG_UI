using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JungWonJu
{
    // 인벤토리에서도 사용 예정

    public class ItemIcon : MonoBehaviour
    {
        Image _icon;
        Button _btnClick;
        Text _txtCount;

        [SerializeField] Sprite _defaultSprite;
        [SerializeField] Sprite _sprite = null;
        [SerializeField] ItemInfo _info;

        public void Init(GameObject owner)
        {
            UI_Inventory uI_Inventory = owner.GetComponent<UI_Inventory>();

            _icon = transform.Find("Icon").GetComponent<Image>();
            _txtCount = transform.Find("Text").GetComponent<Text>();
            // Default 값 ""
            _txtCount.text = "";

            _btnClick = transform.GetComponent<Button>();
            _btnClick.onClick.AddListener(delegate ()
            {
                uI_Inventory._itemInfoUI.SetUI_ItemInfo(_info);
                uI_Inventory._itemInfoUI.OnShow(true);
            });
        }

        private void Update()
        {
            if(_sprite == null)
            {
                if(_btnClick.IsActive() == true)
                {
                    _btnClick.enabled = false;
                }
            }
            else if (_sprite != null)
            {
                if(_btnClick.IsActive() == false)
                {
                    _btnClick.enabled = true;
                }
            }
        }

        public void Clear()
        {
            _info = null;
            _sprite = null;
            _icon.sprite = _defaultSprite;
            _txtCount.text = "";
        }

        public void SetItemInfo(ItemInfo info)
        {
            _info = info;
            if (_info != null)
                _info.SetJsonData();
        }

        public void SetSprite(Sprite sprite)
        {
            _sprite = sprite;

            _icon.sprite = _sprite;
        }

        public void SetItemCount(int count)
        {
            _txtCount.text = string.Format("{0}", count);
        }
    }
}