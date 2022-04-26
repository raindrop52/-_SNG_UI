using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

namespace JungWonJu
{
    public class UI_Inventory : UI_Base
    {
        static int _BAG_PUT_COUNT = 24;

        [SerializeField] int _boxIndex = 1;
        [SerializeField] int _minBoxIndex = 1;
        [SerializeField] int _maxBoxIndex = 5;

        [SerializeField] Grid_Inventory _itemBox;
        [SerializeField] public ItemInfo_UI _itemInfoUI;

        [SerializeField] Text _txtPage;

        public override void Init()
        {
            base.Init();

            _itemBox = transform.Find("Grid").GetComponent<Grid_Inventory>();
            _itemBox.Init(gameObject);

            _itemInfoUI = transform.Find("Item_Info(UI)").GetComponent<ItemInfo_UI>();
            _itemInfoUI.Init();
            // 할당 후 보이지 않도록 설정
            _itemInfoUI.OnShow(false);

            _txtPage = transform.Find("Text_Page").GetComponent<Text>();

            RefreshUI();
        }

        protected override void OnEnable()
        {
            // 활성화 시 아이템 정보 가져오기
            RefreshUI();
        }

        public void RefreshUI()
        {
            ShowInventoryItem();
        }

        // TODO : 구현 예정
        // 현재는 아이템 테이블에 있는 정보 나열
        void ShowInventoryItem()
        {
            //TODO : 아이템 테이블에서 아이템 정보 가져오기
            GameDataManager gameDataMgr = GameDataManager.I;

            if(gameDataMgr != null)
            {
                // 시작 번호 : _boxIndex에 -1 하여 Box1일때 0으로 설정
                int startNo = _BAG_PUT_COUNT * (_boxIndex - 1);
                // 종료 번호 :
                int endNo = _BAG_PUT_COUNT * _boxIndex;
                for (int i = startNo; i < endNo; i++)
                {
                    int index = i % _BAG_PUT_COUNT;

                    if (i < Inventory.I.GetInventoryCount())
                    {
                        _itemBox.SetItemInfo(index, Inventory.I.GetInventoryData(i));
                    }
                    else
                    {
                        _itemBox.SetClear(index % _BAG_PUT_COUNT);
                    }
                }
            }
        }

        public void ChangeBox(int dir)
        {
            int no = _boxIndex + dir;

            // < 버튼 클릭 시 값이 최소 값 보다 작을 때
            if(no < _minBoxIndex)
            {
                // 번호를 최대 값으로 설정
                no = _maxBoxIndex;
            }
            // > 버튼 클릭 시 값이 최대 값 보다 클 때
            else if (no > _maxBoxIndex)
            {
                // 번호를 최소 값으로 설정
                no = _minBoxIndex;
            }

            // 페이지 번호 변경
            if(_txtPage != null)
            {
                _txtPage.text = string.Format("{0:D2}", no);
            }

            _boxIndex = no;

            // no 페이지의 아이템 정보 불러오기
            ShowInventoryItem();
        }
    }
}