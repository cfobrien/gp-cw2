using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Patrol : AIBaseFSM  // inherits from base FSM
{
    GameObject[] patrolPts;
    int currentPt;

    void Awake()
    {
        patrolPts =  GameObject.FindGameObjectsWithTag("patrolPt");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentPt = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {      
        if(patrolPts.Length == 0) return;  //empty array

        if(Vector3.Distance(patrolPts[currentPt].transform.position, NPC.transform.position) < accuracy)
        {
            Debug.Log("curr point: " + currentPt);
            currentPt ++;
            if(currentPt >= patrolPts.Length){
                currentPt = 0;  //reset when get to end
            }
        }

        agent.SetDestination(patrolPts[currentPt].transform.position);  // using NavMesh to set destination

    }

}
