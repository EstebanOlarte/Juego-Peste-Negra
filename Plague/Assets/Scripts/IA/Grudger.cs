using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Grudger : Actor {

    string[] puntos = { "points" , "points1" , "points2" , "points3" , "points4" , "points5" , "points6" , "points7"  , "points8" , "points9" };
    NavMeshAgent agent;
    bool sigue = false;
    bool infectado = false; 
    Transform[] points;
    int rand;
    

    protected override void Start()
    {
        base.Start();

        points = GameObject.Find(puntos[Random.Range(0,2)]).GetComponentsInChildren<Transform>();                       ////// BORRAR ESTE CUANDO SE TENGAN MAS RUTAS
        //points = GameObject.Find(puntos[Random.Range(0, puntos.Length)]).GetComponentsInChildren<Transform>();        ////// USAR ESTE CUANDO SE TENGAN MAS RUTAS
        agent = GetComponent<NavMeshAgent>();

        NextPoint();
    }

    protected override void Update()
    {
        base.Update();

        agent.speed = 2.5f*speed;

        if (infectado && sigue)
        {
            agent.SetDestination(GameObject.Find("kid").GetComponent<Transform>().position);
            anim.SetBool("Chase", true);
        }
        if (infectado==false || (infectado && !sigue))
        {
            anim.SetBool("Chase", false);
            if (Vector3.Distance(transform.position, points[rand].position) < 2)
            {
                NextPoint();
            }
        }

        //print(sigue);

    }

    private void NextPoint()
    {
        rand = Random.Range(0, points.Length);

        agent.SetDestination(points[rand].position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            int c = 0;
            for (int i = 0; i < enfermedades.Length; i++)
            {
                if (enfermedades[i].Manifestado)
                {
                    c++;
                }
            }
            if (c > 0)
                infectado = true;
            else
                infectado = false;

            sigue = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            sigue = false;

            if (infectado)
                NextPoint();
        }
    }
}
