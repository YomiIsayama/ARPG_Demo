using SingleInstance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SingleInstance 
{
    public class UIManager : SingleMono<UIManager>
    {
        private GameObject bag;
        private IUserInput pi;
        private Button closeBtn;
        private GameObject EscPanel;
        void Awake()
        {
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
        void Update()
        {
            openMyBag();
            openEscPanel();
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
    }
}

