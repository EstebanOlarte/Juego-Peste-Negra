using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VirusA))]
[RequireComponent(typeof(VirusS))]
[RequireComponent(typeof(BlackDeath))]
[RequireComponent(typeof(CircleCollider2D))]

public abstract class Actor : MonoBehaviour {

    //Movimiento
    [SerializeField]
    public float speed;
    float startSpeed;

    //Enfermedades            { A , S , BD }
    protected Disease[] enfermedades = new Disease[3];
    protected float[] tiempoContagiado = { 0 , 0 , 0 };
    protected float[] tiempoParaMorir = { 0 , 0 , 0 };
    protected float[] probabilidadDeContagio = { 100 , 100 , 100};



    // Use this for initialization
    protected void Start () {
        enfermedades[0] = GetComponent<VirusA>();
        enfermedades[1] = GetComponent<VirusS>();
        enfermedades[2] = GetComponent<BlackDeath>();

        startSpeed = speed;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        Movimiento();

        speed = startSpeed * (enfermedades[0].speedModifier) * (enfermedades[1].speedModifier) * (enfermedades[2].speedModifier);

        for (int i = 0; i < enfermedades.Length; i++)
        {
            if (enfermedades[i].Contagiado)
            {
                if (tiempoContagiado[i] < enfermedades[i].OnSet)
                    tiempoContagiado[i] += Time.deltaTime;
                else
                {
                    tiempoParaMorir[i] += Time.deltaTime;
                    enfermedades[i].Manifestacion = true;
                }

                if(tiempoParaMorir[i] > enfermedades[i].TimeUntilDeath)
                {
                    //Destroy(this);
                    //Destroy(GetComponent<CircleCollider2D>());
                }
            }
        }
        print(speed);
	}

    protected virtual void Movimiento()
    {   
    }

    protected virtual void Contagiar()
    {
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        int r = 0;

        for (int i = 0; i < 3 ; i++)
        {
            if (enfermedades[i].Contagiado && collision.gameObject.GetComponent<Actor>().enfermedades[i].Contagiado == false)
            {
                r = Random.Range(1, 101);

                if (r <= probabilidadDeContagio[i])
                {

                    collision.gameObject.GetComponent<Actor>().enfermedades[i].Contagiado = true;
                }
            }
        }
    }
}
