using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Denizen : Actor {


    protected NavMeshAgent aiAgent;
    string[] puntos = { "points", "points1", "points2", "points3", "points4", "points5", "points6", "points7", "points8", "points9" };
    [SerializeField]
    protected Transform[] points; //Los Waypoints a donde ira, points
    protected int currentWP = 0; //El Waypoint Actual
    //int pastWP = -1; //El Anterior
    [SerializeField]
    protected int repeatRate = 20; //ratio en el que el invoke se repite

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        aiAgent = GetComponent<NavMeshAgent>();

        InvokeRepeating("RouteChange", 0, repeatRate); 
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        aiAgent.speed = 2.5f * speed;
        if (points != null)
        {
            if (Vector3.Distance(transform.position, points[currentWP].position) < 2)
            {
                currentWP++;
                Movimiento();

            }
        }

    }


    protected override void Movimiento()
    {
        // Seguir una ruta 
        // Poner Varias rutas predeinidas en el mapa, que el busque la mas cercana
        // Mover al Denizen usando un MoveTowardsPoint hacia esa ruta
        // Cada ruta está compuesta por un grupo de puntos, al interatur con algúno de los puntos te envia al siguente en la lista
        // Formando un loop




        //print(currentWP);
        if (points != null)
        {
            aiAgent.SetDestination(points[currentWP].position);

            if (currentWP >= points.Length - 1)
            {

                currentWP = 0;
            }
        }
    }

    protected virtual void RouteChange()
    {
        int index = Random.Range(0, puntos.Length);
        points = GameObject.Find(puntos[index]).GetComponentsInChildren<Transform>();
        currentWP = Random.Range(0,points.Length);
        Movimiento();
        //print("He Cambiado de Ruta :D, Soy una IA Inteligente :3");
    }

    
}
