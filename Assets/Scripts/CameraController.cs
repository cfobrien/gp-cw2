using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject topRoad = GameObject.Find("Road 1");
        Vector3 topRoadPos = topRoad.GetComponent<Transform>().position;
        gameObject.GetComponent<Transform>().position = topRoadPos + new Vector3(0.0f, 1.0f, -1.2f);
        GameObject sky = GameObject.Find("Sky");
        sky.GetComponent<Transform>().position = topRoadPos + new Vector3(0.0f, 5.0f, 20.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
