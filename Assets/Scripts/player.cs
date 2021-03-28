using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int lives;  // player lives
    public int state;  // player position (can be -1, 0 or 1)
    public float moveDistance = 0.36f;  // distance moved to side
    float speed = 2.0f;         // speed moved to side
    float height;
    Vector3 target;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        state = 0;
        height = camera.GetComponent<PlotManager>().GetInradius(camera.GetComponent<PlotManager>().road);
        transform.position = new Vector3(0.0f, height, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        var v3 = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, 0.0f);
        transform.Translate(speed * v3.normalized * Time.deltaTime);

        if(transform.position.x >= 0.36f)
        {
            transform.position = new Vector3(0.36f, height, 0.6f);
        }else if (transform.position.x <= -0.36f)
        {
            transform.position = new Vector3(-0.36f, height, 0.6f);
        }
    }

	void OnTriggerEnter(Collider other) {
		Debug.Log(this.gameObject.name + " collided with " + other.gameObject.name);
		if (this.gameObject.name == other.gameObject.name){
			return;
		}
        GameManager.Instance.lives++;
    }
}
