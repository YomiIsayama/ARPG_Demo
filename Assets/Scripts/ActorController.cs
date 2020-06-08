using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public CameraController camcon;
    public IUserInput pi;
    public float walkSpeed = 1.4f;
    public float runMultiplier = 2.7f;
    public float jumpVelocity = 4.0f;
    public float rollVelocity = 3.0f;
    public float jabVelocity = 3.0f;


    public Animator anim;
    public delegate void OnActionDelegate();
    public event OnActionDelegate OnAction;//am item interaction
    public bool leftIsShield = true;

    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;


    private CapsuleCollider col;
    private Rigidbody rigid;
    private Vector3 planarVec; 
    private Vector3 thrustVec; //冲量
    private bool canAttack;    
    private bool lockPlanar = false;//if lockPlanar == true  既 动作后 无法改变方向，速度的输入  //true = 锁死。 表示  让模型保持之前的速度
    private bool trackDirection = false;
    private Vector3 deltaPos;
    private Vector3 deltaPosroll;

    void Awake()
    {
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach (var input in inputs)
        {
            if (input.enabled == true)
            {
                pi = input;
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        actCtrl();
        lockCtrl();
    }
    void FixedUpdate() //time.fixeddeltatime  1/50
    {
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        //rigid.position += planarVec * Time.fixedDeltaTime + thrustVec;
        //产生影响后立即清零。
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
        deltaPosroll = Vector3.zero;
    }

    private void lockCtrl()
    {
        if (camcon.lockState == false)
        {
            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), ((pi.run) ? 2.0f : 1.0f), 0.5f));
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localDvecz = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("forward", localDvecz.z * ((pi.run) ? 2.0f : 1.0f));
            anim.SetFloat("right", localDvecz.x * ((pi.run) ? 2.0f : 1.0f));
        }

        if (pi.lockon)
        {
            camcon.LockUnlock();
        }

        if (camcon.lockState == false)
        {
            if (pi.inputEnabled == true)
            {
                if (pi.Dmag > 0.1f)
                {
                    // Vector3 transFroward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
                    // // 转身动画速度30%;
                    model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
                }
            }

            if (lockPlanar == false)
            {
                //runMultiplier = 2.0f 跑步速度   ()?():()   判断 walk or run 
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
        }
        else
        {
            if (trackDirection == false)
            {
                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = planarVec.normalized;
            }


            model.transform.forward = transform.forward;
            if (lockPlanar == false)
            {
                planarVec = pi.Dvec * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
        }
    }

    private void actCtrl()
    {
        if (pi.roll)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if (pi.jab)
        {
            anim.SetTrigger("jab");
            canAttack = false;
        }

        if ((pi.attack || pi.defense) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack)
        {
            if (pi.attack)
            {
                anim.SetBool("R0L1", false);
                anim.SetTrigger("attack");
            }
            else if (pi.defense && !leftIsShield)
            {
                anim.SetBool("R0L1", true);
                anim.SetTrigger("attack");
            }
        }

        if ((pi.rt || pi.lt) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack)
        {
            if (pi.rt)
            {
            }
            else
            {
                if (!leftIsShield)
                {
                }
                else
                {
                    anim.SetTrigger("counterBack");
                }
            }
        }

        if (pi.action)
        {
            OnAction.Invoke();
        }

        if (leftIsShield)
        {
            if (CheckState("ground") || CheckState("blocked"))
            {
                anim.SetBool("defense", pi.defense);
                anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);
            }
            else
            {
                anim.SetBool("defense", false);
                anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);

                //anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
        }
    }




    public bool CheckState(string stateName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }

    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(tagName);
        return result;
    }

    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }


    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }

    public void OnGroundExit()
    {
        col.material = frictionZero;
    }

    public void OnJumpEnter()
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        pi.inputEnabled = false;
        lockPlanar = true;
        trackDirection = true;
    }

    public void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");
    }

    public void OnHitEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void OnDieEnter()
    {
        pi.inputEnabled = false;
        model.SendMessage("WeaponDisable");
    }
    public void OnBlockedEnter()
    {
        pi.inputEnabled = false;

    }

    public void OnFallEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        trackDirection = true;
    }

    public void OnRollUpdate()
    {
        thrustVec = model.transform.forward * rollVelocity;
    }

    public void OnJabEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        trackDirection = true;
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * jabVelocity;
    }

    public void OnStunnedEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
    }

    public void OnCounterBackEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
    }

    public void OnCounterBackExit()
    {
        model.SendMessage("CounterBackDisable");
    }
    public void OnLockEnter()
    {
        pi.inputEnabled = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void OnAttack1hAEnter()

    {
        pi.inputEnabled = false;
        //lerpTarget = 1.0f;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity") * 0.5f;
        //float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        // currentWeight = Mathf.Lerp(currentWeight,lerpTarget,0.1f);
        // anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }

    public void OnUpdateRM(object _deltaPos)
    {
        if (CheckState("attack1hC"))
        {
            deltaPos += (0.8f * deltaPos + 0.2f * (Vector3)_deltaPos) / 1.0f;
        }
    }


    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void SetBool(string boolName, bool value)
    {
        anim.SetBool(boolName, value);
    }
}
