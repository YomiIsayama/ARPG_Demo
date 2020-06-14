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
        void Start()
        {
            pi = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyboardInput>();
            bag = GameObject.FindGameObjectWithTag("Bag");
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
        }
        private void openMyBag()
        {
            _closeBtn();
            bag.SetActive(pi.isOpen);
            InventoryManager.RefreshItem();
        }
        private void _closeBtn()
        {
            closeBtn.onClick.AddListener(delegate ()
            {
                pi.isOpen = false;
            });
        }
    }
}

