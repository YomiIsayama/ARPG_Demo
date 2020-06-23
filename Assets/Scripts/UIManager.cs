using SingleInstance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

namespace SingleInstance 
{
    public class UIManager : SingleMono<UIManager>
    {
        private TargetManager targetManager;
        public CameraController camcon;
        private GameObject bag;
        private IUserInput pi;
        private Button closeBtn;
        private GameObject EscPanel;

        public CanvasGroup tacticalMenu;
        public CanvasGroup attackMenu;
        public Transform commandsGroup;
        public Transform targetGroup;
        public Image atbSlider;

        public CanvasGroup aimCanvas;
        public bool aimAtTarget;

        void Awake()
        {
            targetManager = GameObject.FindGameObjectWithTag("Player").GetComponent<TargetManager>();
            pi = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyboardInput>();
            bag = GameObject.FindGameObjectWithTag("Bag");
            EscPanel = GameObject.Find("EscPanel");
            foreach (Transform T in bag.GetComponentInChildren<Transform>())
            {
                if (T.name.CompareTo("CloseBtn")==0)
                {
                    closeBtn = T.GetComponent<Button>();
                }

            }
        }

        void Start()
        {
            targetManager.OnModificationATB.AddListener(() => UpdateSlider());
            targetManager.OnTacticalTrigger.AddListener((x) => ShowTacticalMenu(x));
            targetManager.OnTargetSelectTrigger.AddListener((x) => ShowTargetOptions(x));
        }


        void Update()
        {
            openMyBag();
            openEscPanel();
            if (aimAtTarget)
            {
                aimCanvas.transform.position = targetManager.targetCam.WorldToScreenPoint(targetManager.targets[targetManager.targetIndex].position + Vector3.up);
            }
        }
        private void openMyBag()
        {
            _closeBtn();
            bag.SetActive(pi.isOpen);
        }
        private void _closeBtn()
        {
            closeBtn.onClick.AddListener(delegate ()
            {
                pi.isOpen = false;
            });
        }
        private void openEscPanel()
        {
            EscPanel.SetActive(pi.esc);
        }

        public void UpdateSlider()
        {
            atbSlider.fillAmount = targetManager.atbSlider / targetManager.filledAtbValue*2;
        }

        public void ShowTacticalMenu(bool isShow)
        {
            tacticalMenu.DOFade(isShow ? 1 : 0, .15f).SetUpdate(true);
            tacticalMenu.interactable = isShow;
            attackMenu.DOFade(isShow ? 0 : 1, .15f).SetUpdate(true);
            attackMenu.interactable = !isShow;

            EventSystem.current.SetSelectedGameObject(null);

            if (isShow == true)
            {
                EventSystem.current.SetSelectedGameObject(tacticalMenu.transform.GetChild(0).GetChild(0).gameObject);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(attackMenu.transform.GetChild(0).gameObject);
                commandsGroup.gameObject.SetActive(!isShow);
                //targetGroup.gameObject.SetActive(on);
            }
        }


        public void ShowTargetOptions(bool on)
        {
            EventSystem.current.SetSelectedGameObject(null);

            aimAtTarget = on;
            aimCanvas.alpha = on ? 1 : 0;


            commandsGroup.gameObject.SetActive(!on);
            targetGroup.GetComponent<CanvasGroup>().DOFade(on ? 1 : 0, .1f).SetUpdate(true);
            targetGroup.GetComponent<CanvasGroup>().interactable = on;

            if (on)
            {
                for (int i = 0; i < targetGroup.childCount; i++)
                {
                    if (targetManager.targets.Count - 1 >= i)
                    {
                        targetGroup.GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
                        targetGroup.GetChild(i).GetComponent<CanvasGroup>().interactable = true;
                        //targetGroup.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = TargetManager.targets[i].name;
                    }
                    else
                    {
                        targetGroup.GetChild(i).GetComponent<CanvasGroup>().alpha = 0;
                        targetGroup.GetChild(i).GetComponent<CanvasGroup>().interactable = false;
                    }
                }
            }
            EventSystem.current.SetSelectedGameObject(on ? targetGroup.GetChild(0).gameObject : commandsGroup.GetChild(0).gameObject);
        }
    }
}

