using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

namespace JungWonJu
{
    public class UI_Status : UI_Base
    {
        Text _playerName;

        [SerializeField] Transform _transStatus;
        Text _textHp;
        Text _textAtk;
        Text _textDef;

        public override void Init()
        {
            base.Init();

            _playerName = transform.Find("Frame(Name)").GetComponentInChildren<Text>();

            if (_transStatus != null)
            {
                _textHp = _transStatus.Find("Text_HP_Value").GetComponent<Text>();
                _textAtk = _transStatus.Find("Text_ATK_Value").GetComponent<Text>();
                _textDef = _transStatus.Find("Text_DEF_Value").GetComponent<Text>();
            }
        }

        protected override void Update()
        {
            if(NetworkManagerEx.I != null)
            {
                Player p = NetworkManagerEx.I.GetMyPlayer();
                if (_transStatus != null && p != null)
                {
                    _textHp.text = string.Format("{0}", (int)p._hp);
                    _textAtk.text = string.Format("{0}", p._attack);
                    _textDef.text = string.Format("{0}", p._defense);
                }
            }
        }

        protected override void OnEnable()
        {
            Player p = null;
            if (NetworkManagerEx.I != null)
                p = NetworkManagerEx.I.GetMyPlayer();

            if(p != null)
            {
                // 활성화 시 플레이어 이름 체크
                if (_playerName != null)
                {
                    _playerName.text = p.playerName;
                }
            }
        }
    }
}