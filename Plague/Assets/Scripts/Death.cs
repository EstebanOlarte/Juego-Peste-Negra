using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Death : MonoBehaviour {

    Image i;
    float timer;

	// Use this for initialization
	void Start () {
        i = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        i.color = new Color(1,1,1,timer / 2.5f);

        if (timer > 2.5f)
            Destroy(this);
	}
}
