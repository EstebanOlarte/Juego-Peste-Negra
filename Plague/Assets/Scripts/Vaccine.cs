using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(AudioSource))]

public class Vaccine : MonoBehaviour {

    
    public int index;
    public int tolerancia;
    AudioSource audioSource;
    [SerializeField]
    AudioClip sonido;

	// Use this for initialization

	void Start () {
        audioSource = GetComponent<AudioSource>();
        index = Random.Range(0,3);
        tolerancia = Random.Range(10,30);
	}

    public void PlaySonido()
    {
        audioSource.PlayOneShot(sonido);
    }
}
