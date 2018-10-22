using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour {

    Animator anim;

    public GameObject cam;
    public float speed;
    public float velLerp;
    Actor player;
    bool move;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        player = GetComponent<Actor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        speed = player.speed;
        Vector3 dirMoveForward = new Vector3(cam.transform.forward.x,0,cam.transform.forward.z).normalized*0.1f;
        Vector3 dirMoveLateral = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized*0.08f; 


        transform.position += (dirMoveForward*speed* Input.GetAxis("Vertical"))+(dirMoveLateral * speed * Input.GetAxis("Horizontal"));


        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            move = true;
            transform.forward = Vector3.Lerp(transform.forward, (dirMoveForward * speed * Input.GetAxis("Vertical")) + (dirMoveLateral * speed * Input.GetAxis("Horizontal")), velLerp).normalized;
        }

        else
            move = false;

        anim.SetBool("Run", move); 
	}
}
