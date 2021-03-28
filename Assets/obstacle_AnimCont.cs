using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_AnimCont : MonoBehaviour
{
    Animator animator;
    player currPlayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject playerObj = GameObject.Find("newPlayer");
        currPlayer = playerObj.GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && currPlayer.lives > 0)
        {
            animator.SetBool("gameStart", true);
        }
        if (currPlayer.lives == 0)
        {
            animator.SetBool("gameStart", false);
            animator.SetBool("gameEnd", true);
        }
    }
}
