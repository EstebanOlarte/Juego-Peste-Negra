using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusA : Disease {

    // Use this for initialization
    protected override void Start () {
        onSet = 20;
        timeUntilDeath = 50;
        base.Start();
    }


    protected override void ManifestarSintomas()
    {
        speedModifier = 0.9f;
        GetComponent<SpriteRenderer>().color -= new Color(0.5f,0,0,0);
    }
}
