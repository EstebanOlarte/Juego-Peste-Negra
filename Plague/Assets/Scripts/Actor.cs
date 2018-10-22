using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(VirusA))]
[RequireComponent(typeof(VirusS))]
[RequireComponent(typeof(BlackDeath))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public abstract class Actor : MonoBehaviour {

    //Movimiento
    [SerializeField]
    public float speed = 1;
    float startSpeed;
    protected Animator anim;

    //Enfermedades            { A , S , BD }
    protected Disease[] enfermedades = new Disease[3];
    protected float[] tiempoContagiado = { 0, 0, 0 };
    protected float[] tiempoParaMorir = { 0, 0, 0 };
    protected float[] probabilidadDeContagio = { 100, 100, 100 };
    bool llave;

    //Transmicion
    bool canSpread = false;

    //Particles
    private ParticleSystem[] particles = new ParticleSystem[3];

    // Use this for initialization
    protected virtual void Start () {
        anim = GetComponent<Animator>();

        particles = GetComponentsInChildren<ParticleSystem>();
        enfermedades[0] = GetComponent<VirusA>();
        enfermedades[1] = GetComponent<VirusS>();
        enfermedades[2] = GetComponent<BlackDeath>();


        startSpeed = speed;
        if (GetComponent<Player>() == null)
        {
            GetComponent<Rigidbody>().useGravity = false;
            for (int i = 0; i < enfermedades.Length; i++)
            {
                enfermedades[i].TimeUntilDeath *= 4;
            }
        }
        GetComponent<Rigidbody>().freezeRotation = true;

        //Para que la IA empieze con defensas , para que no todos se infecten inmediatamente
        if(GetComponent<Player>() == null)
        {
            int r = 0;
            for (int i = 0; i < probabilidadDeContagio.Length; i++)
            {
                r = Random.Range(40,91);

                probabilidadDeContagio[i] = r;
            }
        }
        StartCoroutine(WaitToSpread());
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        Movimiento(); 

        speed = startSpeed * (enfermedades[0].speedModifier) * (enfermedades[1].speedModifier) * (enfermedades[2].speedModifier);


        for (int i = 0; i < enfermedades.Length; i++)
        {
            if(enfermedades[i].Contagiado)
            {
                particles[i].Play(); 
            }
            else
            {
                particles[i].Stop(); 
            }
        }
        for (int i = 0; i < enfermedades.Length; i++)
        {

            if (enfermedades[i].Contagiado)
            {
                if (tiempoContagiado[i] < enfermedades[i].OnSet)
                {
                    tiempoContagiado[i] += Time.deltaTime;
                }
                else
                {
                    tiempoParaMorir[i] += Time.deltaTime;
                    enfermedades[i].Manifestacion = true;
                }

                if(tiempoParaMorir[i] > enfermedades[i].TimeUntilDeath)
                {
                    speed = 0;
                    startSpeed = 0;
                    print("DEAD");
                    anim.SetBool("Dead", true);
                    Destroy(this);
                    Destroy(GetComponent<Collider>());
                    Destroy(GetComponent<Rigidbody>());
                    if (GetComponent<Player>() != null)
                    {
                        GameOver();
                    }
                    for (int j = 0; j < enfermedades.Length; j++)
                    {
                        if (enfermedades[j].Contagiado)
                        {
                            particles[j].Stop();
                        }
                    }
                }
            }
        }

        if(enfermedades[0].Contagiado || enfermedades[1].Contagiado || enfermedades[2].Contagiado)
        {
            anim.SetBool("Sick", true);
        }
        else
            anim.SetBool("Sick", false);

        //print(speed);
    }

    protected virtual void Movimiento()
    {   
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        int r = 0;

        if (collision.gameObject.GetComponent<Actor>() != null)
        {
            if (canSpread)
            {
                if (enfermedades != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (enfermedades[i].Contagiado && collision.gameObject.GetComponent<Actor>().enfermedades[i].Contagiado == false)
                        {
                            r = Random.Range(1, 101);
                            if (r <= collision.gameObject.GetComponent<Actor>().probabilidadDeContagio[i])
                            {
                                collision.gameObject.GetComponent<Actor>().enfermedades[i].Contagiado = true;
                            }
                        }
                    }
                }
            }
            StartCoroutine(WaitToSpread());
        }

        if (collision.gameObject.GetComponent<Vaccine>() != null)
        {
            Vaccine vacuna = collision.gameObject.GetComponent<Vaccine>();
            int i = vacuna.index;

            vacuna.PlaySonido();

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

    IEnumerator WaitToSpread()
    {
        canSpread = false;
        yield return new WaitForSeconds(1);
        canSpread = true;
    }

    protected virtual void GameOver()
    {
        Destroy(GetComponent<PlayerMov>());
    }
}
