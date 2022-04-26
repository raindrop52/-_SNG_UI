using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

public class GameData : ScriptableObject
{

    //public SpriteAssetManager _spriteAssetMgr;

#if UNITY_EDITOR    //���� ������ ȣ�� x����
    public virtual void parse(System.Object[] objList)
    {

    }

    protected void ParseObject(object obj, object csvObj)
    {
        Dictionary<string, System.Object> dict = csvObj as Dictionary<string, System.Object>;

        FieldInfo[] infos = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        //public�� �ƴѰ� or public�ΰ� or static�� �ƴѰ� ��������

        string fieldname = "";
        
        
        for(int i =0; i < infos.Length; i++)
        {
            FieldInfo fi = infos[i];
            fieldname = fi.Name;
            if(dict.ContainsKey(fieldname))
            {       
                object valueObj = dict[fieldname];
                if(fi.FieldType == typeof(string))
                {
                    fi.SetValue(obj, valueObj.ToString());
                }
                else if (fi.FieldType == typeof(int))
                {
                    int o = Convert.ToInt32(valueObj);
                    fi.SetValue(obj, o);
                }
                else if (fi.FieldType == typeof(float))
                {
                    float o = Convert.ToSingle(valueObj);
                    fi.SetValue(obj, o);
                }
                else if (fi.FieldType == typeof(bool))
                {
                    if (valueObj.ToString() == "TRUE")
                    {
                        fi.SetValue(obj, true);
                    }
                    else
                    {
                        fi.SetValue(obj, false);
                    }
                }            
                else
                {
                    Debug.LogError("Unsupported Type");
                }
            }

        }
    }
#endif
}
