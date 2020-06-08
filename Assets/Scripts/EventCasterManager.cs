using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManagerInterface
{
    public string eventName;
    public bool active;
    public Vector3 offset = new Vector3(0, 0, 1f);


    // Start is called before the first frame update
    void Start()
    {
        if (am == null)
        {
            am = GetComponent<ActorManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
