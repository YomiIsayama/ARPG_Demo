using Fungus;
using SingleInstance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusSetting : SingleMono<FungusSetting>
{
    public static bool mouseOn = false;
    private IUserInput pi;
    public string str_FMessage;
    public Flowchart fc;

    void Update()
    {
        if (fc.GetBooleanVariable("isOver") == true)
        {
            mouseOn = false;
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("PlayerModel")&& Input.GetKeyDown(KeyCode.F))
        {
            mouseOn = true;
            Flowchart.BroadcastFungusMessage(str_FMessage);
        }
    }
}
