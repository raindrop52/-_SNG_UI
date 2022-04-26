using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Common
{
    [Serializable]
    public class Inventory_Data
    {
        public string name;
        public string name_en;
        public int count;
        public bool isEquip;

        public Inventory_Data(string name, string name_en, int count, bool isEquip)
        {
            this.name = name;
            this.name_en = name_en;
            this.count = count;
            this.isEquip = isEquip;
        }
    }

    [Serializable]
    public class InventoryList
    {
        public List<Inventory_Data> inventoryList;
    }

    public class Inventory : NetworkBehaviour
    {
        public static Inventory I;

        public InventoryList _dataList;

        static string KEY_CODE_INVENTORY_JSON = "player_inventory";

        private void Awake()
        {
            I = this;
        }

        void Start()
        {
            
        }

        public void Test()
        {
            PlayerPrefs.DeleteKey(KEY_CODE_INVENTORY_JSON);
        }

        public void Init()
        {
            // �ʱ� �Ҵ� �� �κ��丮 �ε�
            LoadJson();
        }

        public void SaveJson(bool clear = false)
        {
            string json;

            if (clear == true)
            {
                InventoryList nullList = new InventoryList();
                json = JsonUtility.ToJson(nullList);
            }
            else
            {
                json = JsonUtility.ToJson(_dataList);
            }

            Debug.Log(json);
            PlayerPrefs.SetString(KEY_CODE_INVENTORY_JSON, json);
        }

        public void LoadJson()
        {
            if(PlayerPrefs.HasKey(KEY_CODE_INVENTORY_JSON) == false)
            {
                SaveJson(true);
                Debug.Log("�κ��丮 ���� ����");
            }
            else
            {
                string loadJson = PlayerPrefs.GetString(KEY_CODE_INVENTORY_JSON);

                _dataList = JsonUtility.FromJson<InventoryList>(loadJson);

                Debug.Log("�κ��丮 ���� �ε�");
            }
        }

        void OnEnable()
        {
            Debug.Log("PrintOnEnable: script was enabled");
        }

        public int GetInventoryCount()
        {
            return _dataList.inventoryList.Count;
        }

        public Inventory_Data GetInventoryData(int index)
        {
            return _dataList.inventoryList[index];
        }

        public Inventory_Data FindInventoryData(string name)
        {
            foreach(Inventory_Data data in _dataList.inventoryList)
            {
                if( data.name.Equals(name))
                {
                    return data;
                }
                else if (data.name_en.Equals(name))
                {
                    return data;
                }
            }

            return null;
        }

        public void RemoveInventoryData(string name)
        {
            Inventory_Data data = FindInventoryData(name);

            Debug.Log("������ Find");

            if (data != null)
            {
                _dataList.inventoryList.Remove(data);
                Debug.Log("������ Remove");
            }

            SaveJson();

            if (JungWonJu.UIManager.I._uiInventory != null)
            {
                JungWonJu.UIManager.I._uiInventory.RefreshUI();
            }
        }

        public void AddInventory(string name, int count = 1)
        {
            RpcAddInventory(name, count);
        }

        [ClientRpc]
        void RpcAddInventory(string name, int count)
        {
            Inventory_Data data = FindInventoryData(name);

            if (data == null)
            {
                ItemInfo info = GameDataManager.I._itemInfo.GetItemInfo(name);
                if (info != null)
                {
                    data = new Inventory_Data(info.name, info.name_en, count, false);
                    _dataList.inventoryList.Add(data);
                }
            }
            else
            {
                data.count += count;
            }

            Debug.Log("Json Saving");
            SaveJson();
            Debug.Log("Json Saved");

            if (JungWonJu.UIManager.I._uiInventory != null)
            {
                JungWonJu.UIManager.I._uiInventory.RefreshUI();
            }
        }


        public void BtnTest(string name)
        {
            AddInventory(name);

            if(JungWonJu.UIManager.I._uiInventory != null)
            {
                JungWonJu.UIManager.I._uiInventory.RefreshUI();
            }
        }
    }
}