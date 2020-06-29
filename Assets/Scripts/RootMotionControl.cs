using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionControl : MonoBehaviour
{

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    //使用OnAnimatorMove 则 handle by script;   
    void OnAnimatorMove()
    {
        //SendMessageUpwards 的 msg  给 父类 
        SendMessageUpwards("OnUpdateRM", (object)anim.deltaPosition);
    }
}
