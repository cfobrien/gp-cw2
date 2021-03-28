using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_AnimCont : MonoBehaviour
{
    Animator animator;
    player currPlayer;
    int caught_counter;

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
        if (GameManager.Instance.lives == GameManager.Instance.MAXLIVES)
        {
            animator.SetBool("gameEnd", true);
        }
        if (caught_counter != 0)
        {
            caught_counter--;
        }
        else
        {
            animator.SetBool("catchPlayer", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "newPlayer")
        {
            animator.SetBool("catchPlayer", true);
            caught_counter = 3;
            Debug.Log("here catchplayer");
        }
        
    }
}
