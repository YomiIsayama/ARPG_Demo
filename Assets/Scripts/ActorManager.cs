using SingleInstance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;

    [Header("===== Auto Generate if null =====")]
    public BattleManager bm;

    public WeaponManager wm;
    public StateManager sm;
    public DirectorManage dm;
    public InteractionManager im;

    public TargetManager targetManager;

    // Start is called before the first frame update
    void Awake()
    {
        targetManager = GameObject.FindGameObjectWithTag("Player").GetComponent<TargetManager>();
        ac = GetComponent<ActorController>();
        GameObject model = ac.model;
        GameObject sensor = null;
        try
        {
            sensor = transform.Find("sensor").gameObject;
        }
        catch (System.Exception)
        {
            // Debug.Log("if there is no sensor obj");
        }


        //
        //        bm = sensor.GetComponent<BattleManager>();
        //        if (bm == null)
        //        {
        //            bm = sensor.AddComponent<BattleManager>();
        //        }
        //
        //        bm.am = this;
        //
        //
        //        wm = model.GetComponent<WeaponManager>();
        //        if (wm == null)
        //        {
        //            wm = model.AddComponent<WeaponManager>();
        //        }
        //
        //        wm.am = this;
        //
        //
        //        sm = gameObject.GetComponent<StateManager>();
        //        if (sm == null)
        //        {
        //            sm = gameObject.AddComponent<StateManager>();
        //        }
        //
        //        sm.am = this;


        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectorManage>(gameObject);
        im = Bind<InteractionManager>(sensor);

        ac.OnAction += DoAction;




    }

    public void DoAction()
    {
        if (im.overlapEcastms.Count != 0)
        {
            if (im.overlapEcastms[0].active == true && !dm.IsPlaying())
            {
                if (im.overlapEcastms[0].eventName == "frontStab")
                {
                    dm.PlayFrontStab("frontStab", this, im.overlapEcastms[0].am);
                }
                else if (im.overlapEcastms[0].eventName == "openBox")
                {
                    if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 60))
                    {
                        im.overlapEcastms[0].active = false;
                        //EventCasterManager 的 offset .  人物自身 transform.position = box 的 am.transform.position + offset值通过TransformVector 转换成 世界坐标 为 box am.transform.position（if不转换，会因为box的Y轴影响，model的Y发生改变）
                        transform.position = im.overlapEcastms[0].am.transform.position + im.overlapEcastms[0].am.transform.TransformVector(im.overlapEcastms[0].offset);
                        ac.model.transform.LookAt(im.overlapEcastms[0].am.transform, Vector3.up);
                        dm.PlayFrontStab("openBox", this, im.overlapEcastms[0].am);
                    }

                }
                else if (im.overlapEcastms[0].eventName == "leverUp")
                {
                    if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 60))
                    {
                        //im.overlapEcastms[0].active = false;
                        transform.position = im.overlapEcastms[0].am.transform.position + im.overlapEcastms[0].am.transform.TransformVector(im.overlapEcastms[0].offset);
                        ac.model.transform.LookAt(im.overlapEcastms[0].am.transform, Vector3.up);
                        dm.PlayFrontStab("leverUp", this, im.overlapEcastms[0].am);
                    }

                }
            }

        }
    }



    // public static T DeepClone<T>(T obj)
    // {
    //  using (var ms = new MemoryStream())
    //  {
    //    var formatter = new BinaryFormatter();
    //    formatter.Serialize(ms, obj);
    //    ms.Position = 0;

    //    return (T) formatter.Deserialize(ms);
    //  }
    // }
    private T Bind<T>(GameObject go) where T : IActorManagerInterface
    {
        T tempInstance;
        if (go == null)
        {
            return null;
        }
        tempInstance = go.GetComponent<T>();
        if (tempInstance == null)
        {
            tempInstance = go.AddComponent<T>();
        }

        tempInstance.am = this;
        return tempInstance;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;
    }

    public void TryDoDamage(WeaponController targetWc, bool attackValid, bool counterValid)
    {

        if (sm.isCounterBackSuccess)
        {
            if (counterValid)
            {
                targetWc.wm.am.Stunned();
            }

        }
        else if (sm.isCounterBackFailure)
        {
            if (attackValid)
            {
                HitOrDie(targetWc, false);
            }

        }

        else if (sm.isImmortal)
        {
            //DoNothing
        }

        else if (sm.isDefense)
        {
            Blocked();
        }
        else
        {
            if (attackValid)
            {
                HitOrDie(targetWc, true);

            }

        }
    }

    public void Stunned()
    {
        ac.IssueTrigger("stunned");
    }

    public void Blocked()
    {
        ac.IssueTrigger("blocked");
    }

    public void HitOrDie(WeaponController targetWc, bool doHitAnimation)
    {
        if (sm.HP <= 0)
        {

        }
        else
        {
            sm.AddHP(-1 * targetWc.GetATK());
            if (sm.HP > 0)
            {
                if (doHitAnimation)
                {
                    Hit();
                    targetManager.ModifyATB(25);
                }
                // do some vfx 
            }
            else
            {
                Die();
            }
        }
    }
    public void Hit()
    {
        ac.IssueTrigger("hit");
    }

    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnabled = false;

        //死亡后 锁定 camcon;
        if (ac.camcon.lockState == true)
        {
            ac.camcon.LockUnlock();

        }
        ac.camcon.enabled = false;
    }



    public void LockUnLockActorController(bool value)
    {
        ac.SetBool("lock", value);
    }



}