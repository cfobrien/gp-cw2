using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    GameObject enemy;



    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.GetComponent<Transform>().position;
        // number of enemies
        // select random position, left (0), middle (1) or right (2)
        int pos = Random.Range(0, 3);  // number between 0-2

        if (pos == 0)
        {
            // left
            enemy.transform.position = new Vector3(playerPos.x - 1, playerPos.y, playerPos.z + 1);
        }
        else if (pos == 1)
        {
            enemy.transform.position = new Vector3(playerPos.x - 1, playerPos.y, playerPos.z + 1);
        }
    }
}
