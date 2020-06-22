using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_trigger : MonoBehaviour
{
    public static bool is_trigger = false;


    void OnTriggerEnter(Collider col)
    {
        if (col.transform.gameObject.tag == "PlayerModel")
        {
            is_trigger = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        is_trigger = false;
    }
}
