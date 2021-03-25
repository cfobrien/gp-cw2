using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    Animator animator;
    public player currPlayer;
    int lives;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject playerObj = GameObject.Find("player");
        currPlayer = playerObj.GetComponent<player>();
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("lives: " + lives);
        if (Input.GetKey("w") || Input.GetKeyDown("w"))
        {
            animator.SetBool("running", true);

        }
        else
        {
            animator.SetBool("running", false);
        }

        if (Input.GetKeyDown("m"))
        {
            lives -= 1;
            if (lives == 0 || currPlayer.lives == 0)
            {
                animator.SetBool("dead", true);
            }
            Debug.Log("lives: " + lives);
        }
    }
}
