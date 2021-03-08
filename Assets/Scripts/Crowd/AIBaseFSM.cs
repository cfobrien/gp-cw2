using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AIBaseFSM : StateMachineBehaviour
{
    public GameObject NPC;
    public NavMeshAgent agent;
    public GameObject opponent;
    public float speed = 1.0f;
    public float rotSpeed = 1.0f;
    public float accuracy = 3.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        NPC = animator.gameObject;
        opponent = NPC.GetComponent<Crowd>().GetPlayer(); // get player so can chase it
        agent = NPC.GetComponent<NavMeshAgent>();  // init navmesh
    }
}
