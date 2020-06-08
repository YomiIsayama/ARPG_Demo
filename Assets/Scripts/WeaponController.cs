using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponManager wm;
    public WeaponData wdata1;

    void Awake()
    {
        wdata1 = GetComponentInChildren<WeaponData>();
    }

    public float GetATK()
    {
        return wdata1.ATK + wm.am.sm.ATK;
    }

}
