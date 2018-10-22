using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : MonoBehaviour {

    [SerializeField]
    float speed = 2;
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles += new Vector3(0,speed,0);
	}
}
