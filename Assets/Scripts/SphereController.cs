using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{

		[SerializeField] private float xAngle, yAngle, zAngle;

    // Start is called before the first frame update
    void Start()
    {
				xAngle = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {

				transform.Rotate(-xAngle, yAngle, zAngle, Space.Self);
				//position = new Vector3 (Mathf.Sin(angle),Mathf.Cos(angle),0);
				//transform.position = position;
		}
}
