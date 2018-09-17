using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Disease : MonoBehaviour {

    protected float onSet;
    protected float timeUntilDeath;
    [SerializeField]
    protected bool contagiado = false;
    [SerializeField]
    protected bool manifestacion = false;
    protected bool manifestado;

    public float speedModifier = 1;


    //Getters
    public float OnSet
    {
        get
        {
            return onSet;
        }

        set
        {
            onSet = value;
        }
    }
    public float TimeUntilDeath
    {
        get
        {
            return timeUntilDeath;
        }

        set
        {
            timeUntilDeath = value;
        }
    }
    public bool Contagiado
    {
        get
        {
            return contagiado;
        }

        set
        {
            contagiado = value;
        }
    }
    public bool Manifestacion
    {
        get
        {
            return manifestacion;
        }

        set
        {
            manifestacion = value;
        }
    }


	// Update is called once per frame
	protected void Update () {
        if (manifestacion)
            if (manifestado == false)
            {
                ManifestarSintomas();
                manifestado = true;
            }
	}

    protected virtual void ManifestarSintomas()
    {
    }

    protected virtual void Stun()
    {
    }
}
