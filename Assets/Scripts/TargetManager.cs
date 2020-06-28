using SingleInstance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

[System.Serializable] public class GameEvent : UnityEvent { }
[System.Serializable] public class LockModeEvent : UnityEvent<bool> { }

public class TargetManager : SingleMono<TargetManager>
{

    [HideInInspector]
    public  GameEvent OnModificationATB;
    [HideInInspector]
    public  LockModeEvent OnTacticalTrigger;
    [HideInInspector]
    public  LockModeEvent OnTargetSelectTrigger;

    public Camera targetCam;
    public IUserInput pi;
    public Animator anim;
    private StateManager stateManager;


    public List<Transform> targets;
    private Transform lookAtObj;

    public bool lockMode = false;
    public bool usingSkill = false;

    internal UnityAction SetAimCamera()
    {
        throw new NotImplementedException();
    }

    private bool isAiming = false;

    public int targetIndex;

    [Header("ATB Data")]
    public  float atbSlider;
    public  float filledAtbValue = 100;
    public  int atbCount;

    void Awake()
    {
        lookAtObj = GameObject.Find("LookAtObj").transform;
        stateManager = gameObject.GetComponent<StateManager>();
        //targetCam = GameObject.Find("targetCamera").GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        targetCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (targets.Count > 0 && !lockMode && !usingSkill)
        {
            targetIndex = NearestTargetToCenter();
            lookAtObj.transform.LookAt(targets[targetIndex]);
        }

        if(pi.lockMode && !usingSkill)
        {
            if (atbCount > 0 && !lockMode)
                SetLockMode(true);
        }
        if (!pi.lockMode)
        {
            CancelAction();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (Transform i in targets)
        {
            if (i == null)
            {
                targets.Remove(i);
            }
        }

        if (other.CompareTag("Enemy")&& !targets.Contains(other.transform))
        {
            targets.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && targets.Contains(other.transform))
        {
            targets.Remove(other.transform);
        }
    }
    private int NearestTargetToCenter()
    {
        float[] distances = new float[targets.Count];

        for (int i = 0; i < targets.Count; i++)
        {
            distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(targets[i].position), new Vector2(Screen.width / 2, Screen.height / 2));
        }

        float minDistance = Mathf.Min(distances);
        int index = 0;

        for (int i = 0; i < distances.Length; i++)
        {
            if (minDistance == distances[i])
                index = i;
        }
        return index;
    }

    private void SetLockMode(bool isLockmode)
    {
        pi.inputEnabled = !isLockmode;
        lockMode = isLockmode;

        if (!isLockmode)
        {
            SetAimCamera(false);
        }

        float time = isLockmode ? 0.01f : 1;
        Time.timeScale = time;

        OnTacticalTrigger.Invoke(isLockmode);
    }
    public void SelectTarget(int index)
    {
        targetIndex = index;
        targetCam.transform.DOLookAt(targets[targetIndex].position, .3f).SetUpdate(true);
    }
    public void SetAimCamera(bool isCmeraLook)
    {

        if (targets.Count < 1)
            return;

        OnTargetSelectTrigger.Invoke(isCmeraLook);
        stateManager.Main_Camera.GetComponent<Camera>().enabled = !isCmeraLook;
        targetCam.transform.LookAt(isCmeraLook ? targets[targetIndex] : null);
        targetCam.gameObject.SetActive(isCmeraLook);

        isAiming = isCmeraLook;



    }

    private void CancelAction()
    {
        if (!targetCam.gameObject.activeSelf && lockMode)
            SetLockMode(false);

        if (targetCam.gameObject.activeSelf)
            SetAimCamera(false);
    }

    public  void ModifyATB(float amount)
    {
        OnModificationATB.Invoke();

        atbSlider += amount;
        atbSlider = Mathf.Clamp(atbSlider, 0, (filledAtbValue * 2));

        if (amount > 0)
        {
            if (atbSlider >= filledAtbValue && atbCount == 0)
                atbCount = 1;
            if (atbSlider >= (filledAtbValue * 2) && atbCount == 1)
                atbCount = 2;
        }
        else
        {
            if (atbSlider <= filledAtbValue)
                atbCount = 0;
            if (atbSlider >= filledAtbValue && atbCount == 0)
                atbCount = 1;
        }

        OnModificationATB.Invoke();
    }
    public void SpinAttack()
    {
        ModifyATB(-100);

        StartCoroutine(AbilityCooldown());

        SetLockMode(false);
        pi.lockMode = false;

        MoveTowardsTarget(targets[targetIndex]);

        //Animation
        anim.SetTrigger("spin");

    }
    public void Heal()
    {
        ModifyATB(-100);

        StartCoroutine(AbilityCooldown());

        SetLockMode(false);
        pi.lockMode = false;

        //Animation
        anim.SetTrigger("hit");

    }
    public void SkillCombo1()
    {
        ModifyATB(-100);

        StartCoroutine(AbilityCooldown());

        SetLockMode(false);
        pi.lockMode = false;

        MoveTowardsTarget(targets[targetIndex]);

        //Animation
        anim.SetTrigger("skillCombo1");

    }
    public void MoveTowardsTarget(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > 1 && Vector3.Distance(transform.position, target.position) < 10)
        {
            StartCoroutine(DashCooldown());
            transform.DOMove(TargetOffset(), .5f);
            transform.LookAt(targets[targetIndex]); 
            transform.DOLookAt(targets[targetIndex].position, .2f);
        }
    }

    IEnumerator AbilityCooldown()
    {
        usingSkill = true;
        yield return new WaitForSeconds(1f);
        usingSkill = false;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(1);
    }

    public Vector3 TargetOffset()
    {
        Vector3 position;
        position = targets[targetIndex].position;
        return Vector3.MoveTowards(position, transform.position, 1.2f);
    }
}
