using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    Animator animator;
    int lives;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n"))
        {
            animator.SetBool("startGame", true);

        }
        else if(Input.GetKeyDown("m"))
        {
            lives -= 1;
			GameManager.Instance.lives ++;
            if (lives == 0)
            {
                animator.SetBool("dead", true);
            }
            Debug.Log("lives: " + lives);
        }
    }
}
