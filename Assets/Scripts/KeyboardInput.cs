using SingleInstance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{
    [Header("===== Key settings =====")]
    public string KeyUp = "w";
    public string KeyDown = "s";
    public string KeyLeft = "a";
    public string KeyRight = "d";
    public string keyRun;
    public string keyAttack;
    public string keyDefense;
    public string keyLockon;
    public string keyAction;
    public string keyJump;
    public string keyRoll;
    public string keyJab;
    public string keyLT;
    public string keyRT;
    public string keyEsc;
    public string keyOpenBag;

    [Header("===== Mouse settings =====")]
    public bool mouseEnable = false;
    public float mouseSensitivityX = 0.6f;
    public float mouseSensitivityY = 0.6f;
    public string keyJRight;
    public string keyJUp;
    public string keyJLeft;
    public string keyJDown;



    // Start is called before the first frame update
    void Start()
    {
        esc = false;
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseInputUpdate();
        inputEnabledUpdate();
        inputRun();
        inputAct();
        inputEsc();
        inputOpenBag();
    }

    private void mouseInputUpdate()
    {
        if (mouseEnable == true)
        {
            jup = Input.GetAxis("Mouse Y") * mouseSensitivityY;
            jright = Input.GetAxis("Mouse X") * mouseSensitivityX;
            cmrfov = Input.GetAxis("Mouse ScrollWheel");
            cmrfov = Mathf.Lerp(cmrfov, cmrfov * 5, 2.5f);
        }
        else
        {
            jup = (Input.GetKey(keyJUp) ? 0.6f : 0) - (Input.GetKey(keyJDown) ? 0.6f : 0);
            jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);
        }

        
    }

    private void inputEnabledUpdate()
    {
        targetDup = (Input.GetKey(KeyUp) ? 1.0f : 0) - (Input.GetKey(KeyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(KeyRight) ? 1.0f : 0) - (Input.GetKey(KeyLeft) ? 1.0f : 0);
        if (inputEnabled == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt(Dup2 * Dup2) + (Dright2 * Dright2);
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;
    }

    private void inputRun()
    {
        bool newrun = Input.GetKeyDown(keyRun);
        if (newrun != onRun && newrun == true)
        {
            onRun = true;
            run = true;
        }
        else if (newrun == onRun)
        {
            run = false;
            onRun = false;
        }
    }

    void inputEsc()
    {
        if (Input.GetKeyDown(keyEsc))
        {
            esc = !esc;
        }
    }
    void inputOpenBag()
    {
        if(Input.GetKeyDown(keyOpenBag))
        {
            isOpen = !isOpen;
        }
    }
    private void inputAct()
    {
        defense = Input.GetMouseButton(1);

        bool newattack = Input.GetMouseButton(0);
        if (newattack != lastattack && newattack == true)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }

        lastattack = newattack;

        bool newaction = Input.GetKey(keyAction);
        if (newaction != lastaction && newaction == true)
        {
            action = true;
        }
        else
        {
            action = false;
        }

        lastaction = newaction;

        bool newlockon = Input.GetKey(keyLockon);
        if (newlockon != lastlockon && newlockon == true)
        {
            lockon = true;
        }
        else
        {
            lockon = false;
        }

        lastlockon = newlockon;



        bool newjump = Input.GetKey(keyJump);
        if (newjump != lastjump && newjump == true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        lastjump = newjump;


        bool newrt = Input.GetKey(keyRT);
        if (newrt != lastrt && newrt == true)
        {
            rt = true;
        }
        else
        {
            rt = false;
        }

        lastrt = newrt;


        bool newlt = Input.GetKey(keyLT);
        if (newlt != lastlt && newlt == true)
        {
            lt = true;
        }
        else
        {
            lt = false;
        }

        lastlt = newlt;


        bool newroll = Input.GetKey(keyRoll);
        if (newroll != lastroll && newroll == true)
        {
            roll = true;
        }
        else
        {
            roll = false;
        }

        lastroll = newroll;

        bool newjab = Input.GetKey(keyJab);
        if (newjab != lastjab && newjab == true)
        {
            jab = true;
        }
        else
        {
            jab = false;
        }

        lastjab = newjab;
    }
}
