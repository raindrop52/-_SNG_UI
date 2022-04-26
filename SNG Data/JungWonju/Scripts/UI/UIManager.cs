using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JungWonJu
{
    public enum UI_LIST
    {
        NONE = -1,
        PI_UI = 0,
        STATUS,
        INVENTORY,
        MAP,
        SETTING,
        MENU,
    }

    public class UIManager : MonoBehaviour
    {
        public static UIManager I;
        
        [SerializeField] UI_Base[] _uiList;
        public UI_PIMenu _uiPIMenu;
        public UI_Inventory _uiInventory;
        public UI_Menu _uiMenu;
        public Hud _uiHud;

        [SerializeField] Button _btnMenu;

        int _openedUIIndex = -1;
        bool _usePIMenu = true;

        private void Awake()
        {
            I = this;
        }

        void Start()
        {
            
        }

        public void Init()
        {
            _uiList = GetComponentsInChildren<UI_Base>(true);

            if(_uiList.Length > 0)
            {
                foreach(UI_Base ui in _uiList)
                {
                    if (ui is UI_PIMenu)
                        _uiPIMenu = ui as UI_PIMenu;
                    else if (ui is UI_Inventory)
                        _uiInventory = ui as UI_Inventory;
                    else if (ui is UI_Menu)
                        _uiMenu = ui as UI_Menu;

                    ui.Init();

                    // 전체 UI 비활성화
                    ui.OnShow(false);
                }
            }

            _btnMenu = transform.Find("Btn_Menu").GetComponent<Button>();
            if (_btnMenu != null)
            {
                _btnMenu.onClick.AddListener(delegate ()
                {
                    _uiMenu.EventOpenClose();
                });
            }

            _uiHud = GetComponentInChildren<Hud>();

            StartCoroutine(_CheckerUIList());
        }

        private void Update()
        {
            // PI 메뉴가 존재하며, 다른 UI_Base가 켜지지 않은 상태일 때 사용 가능
            if(_uiPIMenu != null && _usePIMenu)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    _uiPIMenu.gameObject.SetActive(true);
                    _uiPIMenu.OpenCloseEvent();
                }
                else if (Input.GetKeyUp(KeyCode.Tab))
                {
                    _uiPIMenu.OpenCloseEvent(false);
                    _uiPIMenu.gameObject.SetActive(false);
                }
            }


            // ESC 키 눌릴 시 메뉴 설정
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(_uiMenu != null)
                {
                    if(_openedUIIndex >= 0)
                    {
                        if (_uiList[_openedUIIndex].gameObject.activeSelf == true)
                        {
                            ShowMenuEvent(_openedUIIndex, false);
                            return;
                        }
                    }

                    _uiMenu.EventOpenClose();
                }
            }            
        }

        IEnumerator _CheckerUIList()
        {
            while(true)
            {
                bool checkOn = false;

                for (int i = (int)UI_LIST.STATUS; i < _uiList.Length; i++)
                {
                    if (_uiList[i].gameObject.activeSelf == true)
                    {
                        checkOn = true;
                        break;
                    }
                }

                if (checkOn == true)
                {
                    if(_usePIMenu == true)
                        _usePIMenu = false;
                }
                else
                {
                    if (_usePIMenu == false)
                        _usePIMenu = true;
                }

                yield return null;
            }
        }

        public void ShowMenuEvent(int no, bool show = true)
        {
            _openedUIIndex = no;
            _uiList[no].OnShow(show);
        }
    }
}