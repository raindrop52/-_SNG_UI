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
            // �Ҵ� �� ������ �ʵ��� ����
            _itemInfoUI.OnShow(false);

            _txtPage = transform.Find("Text_Page").GetComponent<Text>();

            RefreshUI();
        }

        protected override void OnEnable()
        {
            // Ȱ��ȭ �� ������ ���� ��������
            RefreshUI();
        }

        public void RefreshUI()
        {
            ShowInventoryItem();
        }

        // TODO : ���� ����
        // ����� ������ ���̺� �ִ� ���� ����
        void ShowInventoryItem()
        {
            //TODO : ������ ���̺��� ������ ���� ��������
            GameDataManager gameDataMgr = GameDataManager.I;

            if(gameDataMgr != null)
            {
                // ���� ��ȣ : _boxIndex�� -1 �Ͽ� Box1�϶� 0���� ����
                int startNo = _BAG_PUT_COUNT * (_boxIndex - 1);
                // ���� ��ȣ :
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

            // < ��ư Ŭ�� �� ���� �ּ� �� ���� ���� ��
            if(no < _minBoxIndex)
            {
                // ��ȣ�� �ִ� ������ ����
                no = _maxBoxIndex;
            }
            // > ��ư Ŭ�� �� ���� �ִ� �� ���� Ŭ ��
            else if (no > _maxBoxIndex)
            {
                // ��ȣ�� �ּ� ������ ����
                no = _minBoxIndex;
            }

            // ������ ��ȣ ����
            if(_txtPage != null)
            {
                _txtPage.text = string.Format("{0:D2}", no);
            }

            _boxIndex = no;

            // no �������� ������ ���� �ҷ�����
            ShowInventoryItem();
        }
    }
}