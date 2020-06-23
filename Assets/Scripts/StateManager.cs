using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : IActorManagerInterface
{
    public float HP = 15.0f;
    public float HPMax = 15.0f;
    public float ATK = 10.0f;
    public Image uiHPBar;
    private GameObject PlayerHandle;
    public GameObject Main_Camera;


    [Header("1st order state flags")] 
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isJab;
    public bool isAttack;
    public bool isRoll;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;
    public bool isCounterBack;
    public bool isCounterBackEnable;

    [Header("2nd order state flags")] public bool isAllowDefense;
    public bool isImmortal;
    public bool isCounterBackSuccess;
    public bool isCounterBackFailure;

    void Awake()
    {
        Main_Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
        HP = HPMax;
    }

    void Update()
    {
        isGround = am.ac.CheckState("ground");
        isJump = am.ac.CheckState("jump");
        isFall = am.ac.CheckState("fall");
        isJab = am.ac.CheckState("jab");
        isHit = am.ac.CheckState("hit");
        isRoll = am.ac.CheckState("roll");
        isDie = am.ac.CheckState("die");
        isBlocked = am.ac.CheckState("blocked");
        //isDefense = am.ac.CheckState("defense1h", "defense");
        isAttack = am.ac.CheckStateTag("attackR") || am.ac.CheckStateTag("attackL");

        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.ac.CheckState("defense1h", "defense");
        isImmortal = isRoll || isJab;
        isCounterBack = am.ac.CheckState("counterBack");
        isCounterBackSuccess = isCounterBackEnable;
        isCounterBackFailure = isCounterBack && isCounterBackEnable;

        HPBarUpdate();
    }

    public void AddHP(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMax);
    }

    private void HPBarUpdate()
    {
        uiHPBar.fillAmount = HP / HPMax;
        if(am.ac.camcon.isAI)
        {
            uiHPBar.transform.LookAt(Main_Camera.transform);
        }
    }


    public void Test()
    {
        print("sm test HP : " + HP);
    }
}