using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JungWonJu
{
    public class Hud : MonoBehaviour
    {
        Image _imgHp;
        Image _imgMp;
        Image _imgExp;

        int _money;
        Text _textMoney;
        Text _textLevel;

        void Start()
        {
            // hp, mp 바
            _imgHp = transform.Find("HpBar").GetComponent<Image>();
            _imgMp = transform.Find("MpBar").GetComponent<Image>();
            // 레벨
            Transform transLevel = transform.Find("Img_Level");
            _imgExp = transLevel.Find("Img_Exp").GetComponent<Image>();
            _textLevel = transLevel.GetComponentInChildren<Text>();

            //Gradient gradient = new Gradient();
            //_imgExp.color = gradient.Evaluate()

            // 코인 량
            _textMoney = transform.Find("Text_Coin").GetComponent<Text>();
        }

        void Update()
        {
            RefreshHudUI();
        }

        void RefreshHudUI()
        {
            if(NetworkManagerEx.Inst != null)
            {
                Common.Player p = NetworkManagerEx.Inst.GetMyPlayer();
                if(p != null)
                {
                    float perHp = p._hp / p._hpMax;
                    _imgHp.fillAmount = perHp;

                    // TODO : MP 바 연동

                    // 코인 갯수 연동
                    if(_money != p._money)
                    {
                        _money = p._money;

                        ChangeMoney();
                    }
                }
            }
        }

        public void ChangeMoney()
        {
            _textMoney.text = string.Format("{0}", _money);
        }
    }
}