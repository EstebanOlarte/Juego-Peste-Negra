using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class Survivor : Denizen {

    float lookRate = 10;
    bool look;
    bool vaccineDetected;
    SphereCollider mCollider;
    GameObject nearestVaccine;
    protected override void Start()
    {
        mCollider = GetComponent<SphereCollider>();
        mCollider.isTrigger = true;
        mCollider.radius = 20;
        //lookRate = Random.Range(5,15);
        base.Start();
        InvokeRepeating("LookForVaccine", lookRate, lookRate);
    }

    protected override void Movimiento()
    {

        if (look)
        {
            if (nearestVaccine != null)
                aiAgent.SetDestination(nearestVaccine.transform.position);
            else
                base.Movimiento();
        }
        else
        {
            base.Movimiento();
        }
        // + Escaneo de la zona para encontrar vacunas
    }

    void LookForVaccine()
    {
        if(nearestVaccine != null)
        {
            look = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Vaccine>() != null)
        {
            if(enfermedades[other.GetComponent<Vaccine>().index].Contagiado)
                nearestVaccine = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Vaccine>() != null)
        {
            if (enfermedades[other.GetComponent<Vaccine>().index].Contagiado)
                nearestVaccine = null;
        }
    }

    //private new void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Vaccine>() != null)
    //    {
    //        look = false;
    //        print("Soy un survivor inteligente y me acabo de curar de mi infermedad <3");
    //    }
    //}

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.GetComponent<Vaccine>() != null)
        {
            look = false;
            //print("Soy un survivor inteligente y me acabo de curar de mi infermedad <3");
        }
    }
}

