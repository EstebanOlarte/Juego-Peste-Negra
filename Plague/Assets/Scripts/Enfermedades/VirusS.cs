using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusS : Disease {

	// Use this for initialization
	protected override void Start () {
        onSet = 40;
        timeUntilDeath = 40;
        base.Start();
    }

    protected override void ManifestarSintomas()
    { 
        speedModifier = 0.8f;
        InvokeRepeating("Stun" , 10 , 10);
    }

    protected override void Stun()
    {
        int r = 0;
        r = Random.Range( 1 , 101 );
        if (r <= 50)
            StartCoroutine(WaitToMove());
    }

    IEnumerator WaitToMove()
    {
        anim.SetBool("Stun", true);
        speedModifier = 0;
        yield return new WaitForSeconds(2);
        speedModifier = 0.8f;
        anim.SetBool("Stun", false);
    }
}
