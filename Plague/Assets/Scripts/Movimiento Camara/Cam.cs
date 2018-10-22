using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

    public float camMoveSpeed = 120f;
    public GameObject camFollow;
    public float clampAngle = 80;
    public float sensitivity = 150;
    public float mouseX;
    public float mouseY;
    private float rotY = 0;
    private float rotX = 0;


    // Use this for initialization
    void Start () {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

		
	}
	
	// Update is called once per frame
	void Update () {

        mouseY = Input.GetAxis("Mouse X");
        mouseX = -Input.GetAxis("Mouse Y");

        rotY += mouseY * sensitivity * Time.deltaTime;
        rotX += mouseX * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -12, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = localRotation;


    }

    private void LateUpdate()
    {
        CameraUpdater();
    }

    private void CameraUpdater()
    {
        Transform target = camFollow.transform;

        float step = camMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
