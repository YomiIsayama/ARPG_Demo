using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;
using System;
using SingleInstance;
using UnityEngine.UI;
using TMPro;

public class ButtonEvent : MonoBehaviour, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    private TargetManager targetManager;
    private UIManager uIManager;
    public UnityEvent Confirm;
    public UnityEvent Select;
    private Vector3 pos;

    private UnityAction<bool> setAimCamera;
    private UnityAction spinAttack;
    private static int spinAttackTrigger = 0;
    private UnityAction skillCombo1;
    private static int skillCombo1Trigger = 0;
    private UnityAction heal;
    private static int healTrigger = 0;


    void Awake()
    {
        targetManager = GameObject.FindGameObjectWithTag("Player").GetComponent<TargetManager>();
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void Start()
    {
        pos = transform.position;
        StartCoroutine(WaitForFrames());


        setAimCamera = new UnityAction<bool>(targetManager.SetAimCamera);
        heal = new UnityAction(targetManager.Heal);
        spinAttack = new UnityAction(targetManager.SpinAttack);
        skillCombo1 = new UnityAction(targetManager.SkillCombo1);
    }

    IEnumerator WaitForFrames()
    {
        yield return new WaitForSecondsRealtime(.5f);
        pos = transform.position;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.DOMove(pos, .2f).SetEase(Ease.InOutSine).SetUpdate(true);
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.DOMove(pos + (Vector3.right * 10), .2f).SetEase(Ease.InOutSine).SetUpdate(true);
        Select.Invoke();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        transform.DOPunchPosition(Vector3.right, .2f, 10, 1).SetUpdate(true);
        switch(eventData.selectedObject.name)
        {
            case "SkillCombo1":
                skillCombo1Trigger = 1;
                break;
            case "SpinAttack":
                spinAttackTrigger = 1;
                break;
        }

        if (eventData.selectedObject.GetComponentInChildren<TextMeshProUGUI>().text == targetManager.targets[targetManager.targetIndex].name && skillCombo1Trigger == 1)
        {
            Confirm.AddListener(skillCombo1);
            skillCombo1Trigger = 0;
        }
        if (eventData.selectedObject.GetComponentInChildren<TextMeshProUGUI>().text == targetManager.targets[targetManager.targetIndex].name && spinAttackTrigger == 1)
        {
            Confirm.AddListener(spinAttack);
            spinAttackTrigger = 0;
        }
            
            
        Confirm.Invoke();
    }

}
