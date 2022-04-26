using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

namespace Minwoo
{
    public class SeedInven : MonoBehaviour
    {
        Image _icon;
        Text _seedQuantity;
        int _quantity;
        public int Quantity
        { get { return _quantity; } set { _quantity = value; } }

        string _name;       // ¿€π∞ ∏Ì
        public string Name
        { get { return _name; } }

        public void Init()
        {
            _icon = transform.Find("Icon").GetComponent<Image>();

            _seedQuantity = transform.Find("SeedQuantity").GetComponent<Text>();
            _seedQuantity.text = "";            
        }

        public void SetInfo(Sprite sprite, string name)
        {
            _icon.sprite = sprite;

            _name = name;
        }
                
        private void Update()
        {
            Inventory_Data data = Inventory.I.FindInventoryData(_name);
            if(data != null)
            {
                if(_quantity != data.count)
                {
                    _quantity = data.count;

                    _seedQuantity.text = string.Format("X{0}", _quantity);
                }
            }            
        }
    }
}
