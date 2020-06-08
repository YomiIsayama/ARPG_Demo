using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//挂载 battle manager 时 , 自动 添加 capsule collider;
[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManagerInterface
{

    private CapsuleCollider defCol;

    void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = Vector3.up * 1.0f;
        defCol.height = 2.0f;
        defCol.radius = 0.5f;
        defCol.isTrigger = true;

    }

    void OnTriggerEnter(Collider col)
    {
        WeaponController targetWc = col.GetComponentInParent<WeaponController>();
        ///

        if (targetWc == null)
        {
            return;
        }

        //left right 45° attack limit
        GameObject attacker = targetWc.wm.am.gameObject;
        //GameObject receiver = am.gameObject;
        GameObject receiver = am.ac.model;

        // Vector3 attackingDir = receiver.transform.position - attacker.transform.position;
        // Vector3 counterDir = attacker.transform.position - receiver.transform.position;

        // float attackingAngle1 = Vector3.Angle(attacker.transform.forward,attackingDir);
        // float counterAngle1 = Vector3.Angle(receiver.transform.forward, counterDir);
        // float counterAngle2 = Vector3.Angle(attacker.transform.forward, receiver.transform.forward);

        // bool attackValid = (attackingAngle1 < 45);
        // bool counterValid = (counterAngle1 <45 && Mathf.Abs(counterAngle2-180)<45 );
        ///

        if (col.tag == "Weapon")
        {
            am.TryDoDamage(targetWc, CheckAngleTarget(receiver, attacker, 70), CheckAnglePlayer(receiver, attacker, 30));
        }

    }

    public static bool CheckAnglePlayer(GameObject player, GameObject target, float playerAngleLimit)
    {
        Vector3 counterDir = target.transform.position - player.transform.position;
        float counterAngle1 = Vector3.Angle(player.transform.forward, counterDir);
        float counterAngle2 = Vector3.Angle(target.transform.forward, player.transform.forward);
        bool counterValid = (counterAngle1 < playerAngleLimit && Mathf.Abs(counterAngle2 - 180) < playerAngleLimit);
        return counterValid;
    }

    public static bool CheckAngleTarget(GameObject player, GameObject target, float targetAngleLimit)
    {
        Vector3 attackingDir = player.transform.position - target.transform.position;
        float attackingAngle1 = Vector3.Angle(target.transform.forward, attackingDir);
        bool attackValid = (attackingAngle1 < targetAngleLimit);
        return attackValid;
    }
}