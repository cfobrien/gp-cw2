using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacelSpawner : MonoBehaviour
{

	//[SerializeField] private GameObject[] obstacles;
	//[SerializeField] private float spawnInterval = 5.0f;
	private Vector3 spawnPos;
	private Vector3 playerPos;
	// Start is called before the first frame update
    void Start()
    {
		playerPos = GameObject.Find("Player").GetComponent<Transform>().position;
		// call method spawn every spawn interval time
        //InvokeRepeating("Spawn", spawnInterval, spawnInterval);
    }

    // Spawn obstacles
    void Spawn()
    {

		//spawnPos.x = Random.Range(0, 3);
		//spawnPos.y = 0.5f;
		//spawnPos.z = Random.Range(0, 3);

		//Instantiate(obstacles[UnityEngine.Random.Range(0, obstacles.Length - 1)], spawnPos, Quaternion.identity);
    }
}
