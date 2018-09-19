using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Vaccine : MonoBehaviour {

    public int index;
    public int tolerancia;
	// Use this for initialization

	void Start () {
        index = Random.Range(0,3);
        tolerancia = Random.Range(5,20);
	}
}
