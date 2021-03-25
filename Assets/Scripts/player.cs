using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int lives;  // player lives
    public int state;  // player position (can be -1, 0 or 1)
    public float moveDistance = 0.36f;  // distance moved to side
    float speed = 4.0f;         // speed moved to side
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        state = 0;
        transform.position = new Vector3(0.0f, 31.82f, 0.6f);
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && state > -1)
        {
            state -= 1;
            target = new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown("d") && state < 1)
        {
            state += 1;
            target = new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}

