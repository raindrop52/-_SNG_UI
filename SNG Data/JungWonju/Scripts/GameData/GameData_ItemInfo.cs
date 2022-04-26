using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Item_Equip_Type
{
    WEAPON = 0,             // ����
    SUB_WEAPON,             // ���� ����
    ARMOR,                  // �� ��
    HELMET,                 // �Ӹ� ��
    GLOVES,                 // �� ��
    SHOES,                  // �� ��
}

[Serializable]
public class ItemInfo_Equip
{
    public int attack;
    public int defense;
    public int type;
}

[Serializable]
public class ItemInfo_Consumable
{
    public int heal;
    public int damage;
}

[Serializable]
public class ItemInfo_ETC
{
    public string text;
}

[Serializable]
public class ItemInfo_Farm
{
    public bool isSeed;         // �����ΰ�
    public int money;           // ��
}

[Serializable]
public class ItemInfo               // ��ü ������ ���� ���̺�
{
    public int index;               // ������ ���� ��ȣ
    public string name;             // ��Ī
    public string name_en;          // ���� ��Ī
    public int type;                // Ÿ�� ( ���, �Һ�, ��Ÿ, �۹� )
    public string desc;             // ����
    public string sprite_path;       // ��������Ʈ ���

    public string json_sub_info;
    public object sub_info;

    public void SetJsonData()
    {
        switch ((Item_Type)type)
        {
            case Item_Type.EQUIP:
                {
                    sub_info = JsonUtility.FromJson<ItemInfo_Equip>(json_sub_info);
                    break;
                }

            case Item_Type.CONSUMABLE:
                {
                    sub_info = JsonUtility.FromJson<ItemInfo_Consumable>(json_sub_info);
                    break;
                }

            case Item_Type.ETC:
                {
                    sub_info = JsonUtility.FromJson<ItemInfo_ETC>(json_sub_info);
                    break;
                }

            case Item_Type.FARM:
                {
                    sub_info = JsonUtility.FromJson<ItemInfo_Farm>(json_sub_info);
                    break;
                }
        }
    }
}


[CreateAssetMenu(fileName = "GameData_ItemInfo", menuName = "OnlineGame/GameData_ItemInfo", order = 10)]
public class GameData_ItemInfo : GameData
{
    public List<ItemInfo> _dataList;

    public int GetItemListCount()
    {
        return _dataList.Count;
    }

    public ItemInfo GetItemInfo(int index)
    {
        ItemInfo info = null;

        foreach (ItemInfo item in _dataList)
        {
            if (item.index.Equals(index))
            {
                info = item;
                break;
            }
        }

        return info;
    }

    public ItemInfo GetItemInfo(string name)
    {
        ItemInfo info = null;

        foreach (ItemInfo item in _dataList)
        {
            // �ѱ� ��Ī Ž��
            if (item.name.Equals(name))
            {
                info = item;
                break;
            }
            // ���� ��Ī Ž��
            else if (item.name_en.Equals(name))
            {
                info = item;
                break;
            }
        }

        return info;
    }


#if UNITY_EDITOR

    //System.Object�� ������ ���� ��ü�� ��ȯ
    public override void parse(System.Object[] objList)
    {
        _dataList = new List<ItemInfo>();

        foreach (System.Object csvObj in objList)
        {
            ItemInfo info = new ItemInfo();
            ParseObject(info, csvObj);      //��������� �Լ��ȿ��� �ٲ� ���� �״�� �����

            _dataList.Add(info);
        }
    }
#endif
}