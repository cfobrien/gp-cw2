using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class PlayerMovementController : MonoBehaviour
{
    public float maxSpeed = 10.0f; // Movement speed in units/s
    public float accel = 20.0f;
    public float decel = 90.0f;
    public Vector3 gravity = Physics.gravity;
    public bool isJumping = false;
    public float tiltAngle = 2.0f; // Angle in deg of camera tilt when strafing sideways
    private float actualTilt;
    static Vector3 kbDir, mvDir, vel; // Direction indicated by movement keys, actual direction, current velocity
    public KeyCode _left = KeyCode.A;
    public KeyCode _right = KeyCode.D;
    public KeyCode _fwd = KeyCode.W;
    public KeyCode _back = KeyCode.S;

    Camera playerCamera;
    CharacterController cc;

    public float lookSpeed = 2.0f; // Sensitivity
    static float lookXLimit = 90.0f; // Limit in degrees for looking up and down from straight forward
    private float rotationX = 0.0f;


    void Start()
    {
        cc = GetComponent<CharacterController>();
        cc.enabled = true;
        playerCamera = GetComponent<Camera>();

        // Configure fps style camera
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Poll wasd keys
        kbDir = Vector3.zero;
        if (Input.GetKey(_fwd)) {
            kbDir += forward;
        }
        if (Input.GetKey(_back)) {
            kbDir -= forward;
        }
        if (Input.GetKey(_left)) {
            kbDir -= right;
        }
        if (Input.GetKey(_right)) {
            kbDir += right;
        }
        kbDir = kbDir.normalized;

        // Player movement limited when in the air
        mvDir = kbDir;
        if (!cc.isGrounded) {
            mvDir = 0.1f * kbDir;
        }

        // Jump handling
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            cc.Move(-0.2f * gravity);
            isJumping = true;
        }

        // Calculate velocity and gravity
        if (cc.isGrounded) {
            vel += mvDir * accel * mvDir.magnitude * maxSpeed * Time.deltaTime;
            vel *= decel * Time.deltaTime;
        } else {
            vel += mvDir * 0.1f * accel * mvDir.magnitude * maxSpeed * Time.deltaTime;
        }

        cc.Move(vel);
        cc.SimpleMove(gravity);

        // Handle camera tilt
        if (Input.GetKeyDown(_left)) {
            actualTilt += tiltAngle;
        } else if (Input.GetKeyUp(_left)) {
            actualTilt -= tiltAngle;
        }
        if (Input.GetKeyDown(_right)) {
            actualTilt -= tiltAngle;
        } else if (Input.GetKeyUp(_right)) {
            actualTilt += tiltAngle;
        }

        // Rotate camera and player with mouse input
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, actualTilt);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }
}
