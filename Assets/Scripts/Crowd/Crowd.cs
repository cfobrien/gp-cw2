using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Crowd : MonoBehaviour
{
    public GameObject player;
    Animator anim;

    public GameObject GetPlayer()
    {
        return player;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // calc distance between player and enemy
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
    }
}
