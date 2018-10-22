using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class Disease : MonoBehaviour {

    protected float onSet;
    protected float inicialOnSet;
    protected float timeUntilDeath;
    [SerializeField]
    protected bool contagiado = false;
    [SerializeField]
    protected bool manifestacion = false;
    protected bool manifestado;
    public float speedModifier = 1;
    protected Animator anim;

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
    public float InicialOnSet
    {
        get
        {
            return inicialOnSet;
        }

        set
        {
            inicialOnSet = value;
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
    public bool Manifestado
    {
        get
        {
            return manifestado;
        }

        set
        {
            manifestado = value;
        }
    }

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        inicialOnSet = OnSet;
    }

    // Update is called once per frame
    protected void Update () {
        if (manifestacion)
            if (Manifestado == false)
            {
                ManifestarSintomas();
                Manifestado = true;
            }
	}

    protected virtual void ManifestarSintomas()
    {
    }

    protected virtual void Stun()
    {
    }
}
