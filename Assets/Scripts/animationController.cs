using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    Animator animator;
    public player currPlayer;

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
        
        if (Input.GetKey("w"))
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

        if (Input.GetKeyDown("space"))
        {
            animator.SetBool("jump", true);
            animator.SetBool("leftTurn", false);
            animator.SetBool("rightTurn", false);
        }
        else if (Input.GetKeyDown("a") && !Input.GetKey("d"))
        {
            animator.SetBool("jump", false); 
            animator.SetBool("leftTurn", true);
            animator.SetBool("rightTurn", false);
        }
        else if (!Input.GetKey("a") && Input.GetKeyDown("d"))
        {
            animator.SetBool("jump", false);
            animator.SetBool("leftTurn", false);
            animator.SetBool("rightTurn", true);
        }
        else
        {
            animator.SetBool("jump", false); 
            animator.SetBool("leftTurn", false);
            animator.SetBool("rightTurn", false);
        }

        // check if lost all lives
        if (GameManager.Instance.lives == GameManager.Instance.MAXLIVES)
        {
            animator.SetBool("dead", true);
        }
    }
}
