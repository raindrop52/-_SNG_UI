using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using ns;

public class GameDataEditor : EditorWindow
{

    GameData _gameData;
    SpriteAssetManager _spriteAssetMgr;

    [MenuItem("Window/Game Data Editor")]
    static public void OpenGameDataEditor()
    {
        GetWindow<GameDataEditor>(false, "Game Data Editor", true);
    }



    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("==GAME DATA EDITOR==");
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Separator();

        _gameData = EditorGUILayout.ObjectField(_gameData, typeof(GameData), false) as GameData;
        _spriteAssetMgr = EditorGUILayout.ObjectField(_spriteAssetMgr, typeof(SpriteAssetManager), false) as SpriteAssetManager;

        //CSV파일 게임 데이터 가져오기
        if (GUILayout.Button("Import from csv"))
        {
            string gameDataType = _gameData.GetType().ToString();
            string filePath = "GameData/CSV/" + gameDataType + ".csv";

            bool hasFieldName = true;
            char seperator = ',';

            System.Object[] objList = CsvLoader.LoadCsvToObjectList(filePath, hasFieldName, seperator);

            _gameData.parse(objList);

            EditorUtility.SetDirty(_gameData);
        }


        //스프라이트 가져오기
        if (GUILayout.Button("Load Sprite"))
        {
            _spriteAssetMgr._spriteList = new List<SpriteInfo>();

            //Sprite Sheet 가져오기

            for(int i = (int)ChaSpriteType.Default +1; i<(int)ChaSpriteType.End; i++)
            {
                GetChaSpriteSheet((ChaSpriteType)i);
            }
                     
            Debug.Log(Directory.GetCurrentDirectory());


            EditorUtility.SetDirty(_spriteAssetMgr);

        }

        #region 아이템
        //스프라이트 가져오기 (Item Icon)
        if (GUILayout.Button("Load Item Icon Sprite"))
        {
            _spriteAssetMgr._spriteList = new List<SpriteInfo>();
            _spriteAssetMgr._itemIconList.Clear();

            //Sprite Sheet 가져오기
            for (int i = (int)Item_Type.NONE + 1; i < (int)Item_Type.END; i++)
            {
                GetItemIconSprite((Item_Type)i);
            }

            Debug.Log(Directory.GetCurrentDirectory());

            EditorUtility.SetDirty(_spriteAssetMgr);
        }
        #endregion

    }

    #region 아이템
    void GetItemIconSprite(Item_Type type)
    {
        string folderName = type.ToString();

        string[] array = Directory.GetFiles(Directory.GetCurrentDirectory() +
                                               "/Assets/Images/Item/" + folderName + "/", "*.png", SearchOption.AllDirectories);

        foreach (string fullPath in array)
        {
            if (type == Item_Type.FARM)
            {
                string filepath = "Assets/Images/Item/" + folderName + "/Farming_Plants_items.png";

                var sprites2 = AssetDatabase.LoadAllAssetRepresentationsAtPath(filepath);
                if (sprites2 != null)
                {
                    foreach (var s in sprites2)
                    {
                        if (s is Sprite && s.name.Contains("items") == false)
                        {
                            SpriteItemIconInfo info = new SpriteItemIconInfo();

                            info.path = s.name;
                            info.sprite = s as Sprite;
                            info.type = type;

                            _spriteAssetMgr._itemIconList.Add(info);
                        }
                    }
                }
            }
            else
            {
                string path = fullPath.Replace(Directory.GetCurrentDirectory() + "/", "");

                var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);
                if (sprites != null)
                {
                    foreach (var s in sprites)
                    {
                        if (s is Sprite)
                        {
                            SpriteItemIconInfo info = new SpriteItemIconInfo();

                            info.path = s.name;
                            info.sprite = s as Sprite;
                            info.type = type;

                            _spriteAssetMgr._itemIconList.Add(info);
                        }
                    }
                }
            }            
        }
    }
    #endregion

    void GetChaSpriteSheet(ChaSpriteType type)
    {
        string folderName = type.ToString();

        string[] array = Directory.GetFiles(Directory.GetCurrentDirectory() +
                                               "/Assets/Developers/hyoju/Character/" + folderName + "/",
                                               "*.png",
                                               SearchOption.AllDirectories);

        foreach (string fullPath in array)
        {
            string path = fullPath.Replace(Directory.GetCurrentDirectory() + "/", "");

            var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);
            if (sprites != null)
            {
                foreach (var s in sprites)
                {
                    if (s is Sprite)
                    {
                        Direction dir;
                        if (s.name.EndsWith("_0"))
                        {
                            dir = Direction.Right;
                        }
                        else if (s.name.EndsWith("_1"))
                        {
                            dir = Direction.Back;
                        }
                        else if (s.name.EndsWith("_2"))
                        {
                            dir = Direction.Left;
                        }
                        else if (s.name.EndsWith("_3"))
                        {
                            dir = Direction.Front;
                        }
                        else
                        {
                            continue;
                        }
                        SpriteInfo info = new SpriteInfo();
                
                        info.name = s.name.Remove(s.name.Length -2, 2);
                        info.sprite = s as Sprite;
                        info.dir = dir;
                        info.type = type;
                        _spriteAssetMgr._spriteList.Add(info);
                        
                    }
                }
            }
        }

    }
}

