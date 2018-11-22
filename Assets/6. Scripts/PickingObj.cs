using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickingObj : MonoBehaviour {

    protected NavMeshAgent agent;
    protected Transform hitObj;

    
    public Picking picking;

    private void Start()
    {

        picking = FindObjectOfType<Picking>();
    }

    //protected Dictionary<string,ITEM> 


    public virtual void PickingMove(NavMeshAgent _agent)
    {
       
        this.agent = _agent;
        this.agent.stoppingDistance = 2.0f;
        
        this.agent.destination = this.transform.position;
    }


    

}
