using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
using System.Collections;
using System;

public class ButtonEvent : MonoBehaviour, ISubmitHandler, ISelectHandler, IDeselectHandler
{
    private TargetManager targetManager;
    public UnityEvent Confirm;
    public UnityEvent Select;
    public UnityAction sk;
    private Vector3 pos;

    private void Start()
    {
        targetManager = GameObject.FindGameObjectWithTag("Player").GetComponent<TargetManager>();
        pos = transform.position;
        StartCoroutine(WaitForFrames());
        sk = new UnityAction(targetManager.SkillCombo1);
    }

    void Awake()
    {

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
        Confirm.AddListener(sk);
        Confirm.Invoke();
    }
}
