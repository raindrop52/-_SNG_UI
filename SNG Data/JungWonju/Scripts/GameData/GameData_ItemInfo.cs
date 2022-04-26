using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Item_Equip_Type
{
    WEAPON = 0,             // 무기
    SUB_WEAPON,             // 보조 무기
    ARMOR,                  // 몸 방어구
    HELMET,                 // 머리 방어구
    GLOVES,                 // 손 방어구
    SHOES,                  // 발 방어구
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
    public bool isSeed;         // 씨앗인가
    public int money;           // 돈
}

[Serializable]
public class ItemInfo               // 전체 아이템 정보 테이블
{
    public int index;               // 아이템 고유 번호
    public string name;             // 명칭
    public string name_en;          // 영어 명칭
    public int type;                // 타입 ( 장비, 소비, 기타, 작물 )
    public string desc;             // 설명
    public string sprite_path;       // 스프라이트 경로

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
            // 한글 명칭 탐색
            if (item.name.Equals(name))
            {
                info = item;
                break;
            }
            // 영어 명칭 탐색
            else if (item.name_en.Equals(name))
            {
                info = item;
                break;
            }
        }

        return info;
    }


#if UNITY_EDITOR

    //System.Object를 아이템 정보 객체로 변환
    public override void parse(System.Object[] objList)
    {
        _dataList = new List<ItemInfo>();

        foreach (System.Object csvObj in objList)
        {
            ItemInfo info = new ItemInfo();
            ParseObject(info, csvObj);      //얕은복사라서 함수안에서 바뀐 값이 그대로 저장됨

            _dataList.Add(info);
        }
    }
#endif
}