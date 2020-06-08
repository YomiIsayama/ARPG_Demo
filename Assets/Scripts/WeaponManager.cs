using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManagerInterface
{
    private Collider weaponColL;
    private Collider weaponColR;

    public GameObject whL;
    public GameObject whR;
    public WeaponController wcL;
    public WeaponController wcR;

    void Start()
    {
        //weaponCol = whR.GetComponentInChildren<Collider>();
        //print(transform.DeepFind("weaponHandleL"));

        try
        {
            whL = transform.DeepFind("weaponHandleL").gameObject;
            wcL = BindWeaponController(whL);
            weaponColL = whL.GetComponentInChildren<Collider>();
        }
        catch (System.Exception)
        {
            // Debug.Log("if there is no weaponhandleL or related obj");
        }
        try
        {
            whR = transform.DeepFind("weaponHandleR").gameObject;
            wcR = BindWeaponController(whR);
            weaponColR = whR.GetComponentInChildren<Collider>();
        }
        catch (System.Exception)
        {
            // Debug.Log("if there is no weaponhandleR or related obj");
        }


    }


    public void UpdateWeaponCollider(string side, Collider col)
    {
        if (side == "L")
        {
            weaponColL = col;
        }
        else if (side == "R")
        {
            weaponColR = col;
        }
    }

    public void UnloadWeapon(string side)
    {
        if (side == "L")
        {
            foreach (Transform tran in whL.transform)
            {
                weaponColL = null;
                wcL.wdata1 = null;
                Destroy(tran.gameObject);
            }
        }
        else if (side == "R")
        {
            foreach (Transform tran in whR.transform)
            {
                weaponColR = null;
                wcR.wdata1 = null;
                Destroy(tran.gameObject);
            }
        }
    }

    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempWc;
        tempWc = targetObj.GetComponent<WeaponController>();
        if (tempWc == null)
        {
            tempWc = targetObj.AddComponent<WeaponController>();
        }

        tempWc.wm = this;
        return tempWc;
    }


    public void WeaponEnable()
    {

        try
        {
            if (am.ac.CheckStateTag("attackL"))
            {
                //weaponColL.enabled = true;
            }
            else
            {
                weaponColR.enabled = true;
            }
        }
        catch (System.Exception)
        {
            // error because : weaponfactory make a weapon and wc get controller
        }

    }

    public void WeaponDisable()
    {
        try
        {
            //weaponColL.enabled = false;
            weaponColR.enabled = false;
        }
        catch (System.Exception)
        {
            // error because : weaponfactory make a weapon and wc get controller
        }

    }

    public void CounterBackEnable()
    {
        am.SetIsCounterBack(true);
    }

    public void CounterBackDisable()
    {
        am.SetIsCounterBack(false);
    }
}