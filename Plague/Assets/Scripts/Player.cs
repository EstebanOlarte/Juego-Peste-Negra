using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Actor {

    public Text[] texts;
    string[] textVirus = { "VirusA" , "VirusS" , "B.D." };

    protected override void Update()
    {
        base.Update();

        for (int i = 0; i < textVirus.Length; i++)
        {
            texts[i].text = textVirus[i] + " = " + Mathf.Clamp(  (float)decimal.Round( (decimal)((tiempoContagiado[i] * 100) / enfermedades[i].OnSet) , 2 )   , 0 ,100) + "%";
        }
    }

    protected override void Movimiento()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.position += transform.right * horizontal * speed * Time.deltaTime;
        transform.position += transform.up * vertical * speed * Time.deltaTime;
    }
}
