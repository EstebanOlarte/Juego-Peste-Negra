using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDeath : Disease {

    public int counter = 1;

    // Use this for initialization
    protected override void Start () {
        onSet = 8;
        timeUntilDeath = 30;
        base.Start();
    }

    protected override void ManifestarSintomas()
    { 
        speedModifier = 1 - (0.05f * counter);
        InvokeRepeating("Stun", 10, 10);
        InvokeRepeating("Increase", 4, 4);
    }

    protected override void Stun()
    {
        int r = 0;
        r = Random.Range(1, 101);
        if (r <= 15)
        {
            StartCoroutine(WaitToMove());
        }
    }

    void Increase()
    {
        counter++;
        speedModifier = 1 - (0.05f * counter);
        if (counter == 8)
            CancelInvoke("Increase");
    }

    IEnumerator WaitToMove()
    {
        anim.SetBool("Stun", true);
        speedModifier = 0;
        yield return new WaitForSeconds(3);
        speedModifier = 1 - (0.05f * counter);
        anim.SetBool("Stun", false);
    }
}
