using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

public enum ChaSpriteType
{
    Default = 0,
    Accessory,
    Body,
    Eyes,
    Hairstyle,
    Outfit,



    End
}

public enum Direction
{
    Front,
    Right,
    Back,
    Left,

    End
}

[Serializable]
public class SpriteInfo
{    
    public string name;
    public ChaSpriteType type;
    public Sprite sprite;
    public Direction dir;
}

#region 아이템

public enum Item_Type
{
    NONE = -1,
    EQUIP = 0,
    CONSUMABLE,
    ETC,
    FARM,

    END
}

[Serializable]
public class SpriteItemIconInfo
{
    public string path;
    public Item_Type type;
    public Sprite sprite;
}

#endregion

[CreateAssetMenu(fileName = "SpriteAssetManager", menuName = "Common/SpriteAssetManager", order = 0)]
public class SpriteAssetManager : ScriptableObject
{
    public List<SpriteInfo> _spriteList;

    public Sprite GetSprite(string spritePath, Direction dir)
    {
        Sprite s = null;
        foreach (SpriteInfo info in _spriteList)
        {
            if (info.name == spritePath && info.dir == dir)
            {
                s = info.sprite;
                //Debug.Log(info.name);
                break;
            }
        }
        return s;
    }

    #region 아이템
    // 아이템 리스트
    public List<SpriteItemIconInfo> _itemIconList;
    
    public Sprite GetSprite(string spritePath)
    {
        Sprite s = null;

        foreach (SpriteItemIconInfo info in _itemIconList)
        {
            if (info.path == spritePath)
            {
                s = info.sprite;
                break;
            }
        }
        return s;
    }

    #endregion
}