using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VirusA))]
[RequireComponent(typeof(VirusS))]
[RequireComponent(typeof(BlackDeath))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

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
        GetComponent<Rigidbody2D>().gravityScale = 0;
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
        //print(speed);
	}

    protected virtual void Movimiento()
    {   
    }


    protected void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.transform.name);


        int r = 0;

        for (int i = 0; i < 3 ; i++)
        {
            if (collision.gameObject.GetComponent<Actor>() != null)
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

        if (collision.gameObject.GetComponent<Vaccine>() != null)
        {
            Vaccine vacuna = collision.gameObject.GetComponent<Vaccine>();
            int i = vacuna.index;

            enfermedades[i].speedModifier = 1;
            enfermedades[i].Contagiado = false;
            enfermedades[i].Manifestacion = false;
            enfermedades[i].Manifestado = false;
            enfermedades[i].OnSet -= enfermedades[i].InicialOnSet * 0.1f;
            enfermedades[i].OnSet = Mathf.Clamp(enfermedades[i].OnSet, enfermedades[i].InicialOnSet * 0.5f, enfermedades[i].InicialOnSet);
            if (enfermedades[i].IsInvoking("Stun"))
                enfermedades[i].CancelInvoke("Stun");
            
            if(i == 2)
            {
                (enfermedades[2] as BlackDeath).counter = 0;
                if (enfermedades[2].IsInvoking("Increase"))
                    enfermedades[2].CancelInvoke("Increase");
            }
            tiempoContagiado[i] = 0;
            tiempoParaMorir[i] = 0;
            probabilidadDeContagio[i] = Mathf.Clamp(probabilidadDeContagio[i] - vacuna.tolerancia, 40, 100);

            Destroy(collision.gameObject);
        }
    }
}
