using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Actor {

    public Text[] texts;
    string[] textVirus = { "VirusA", "VirusS", "B.D." };
    bool hasKey;
    public AudioClip llaveClip;
    public AudioSource audio;
    [SerializeField]
    GameObject win, lose;
    protected override void Update()
    {
        base.Update();



        for (int i = 0; i < textVirus.Length; i++)
        {
            //texts[i].text = textVirus[i] + " = " + Mathf.Clamp(Mathf.Round((tiempoContagiado[i] * 100F) / enfermedades[i].OnSet), 0.1f, 100f);
            //texts[i].text = textVirus[i] + " = " + Mathf.Clamp(Mathf.Round((tiempoContagiado[i] * 100F) / enfermedades[i].OnSet), 0.1f, 100f);
            texts[i].text = textVirus[i] + " = " + Mathf.Clamp( (float)decimal.Round( (decimal)((tiempoContagiado[i])*100 / enfermedades[i].OnSet) ,2 )  ,0,100 ) + "%";
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.GetComponent<Llave>() != null)
        {
            audio.clip = llaveClip;
            audio.Play();
            hasKey = true;
            Destroy(collision.gameObject);
        }

        if (collision.transform.tag == "Finish" && hasKey && !enfermedades[0].Contagiado && !enfermedades[1].Contagiado && !enfermedades[2].Contagiado)
        {
            win.SetActive(true);
            Destroy(this.gameObject);
            //print("Entonces? sacamos 5? o qué?");
        }
    }
    protected override void GameOver()
    {
        lose.GetComponent<Death>().enabled = true;
        base.GameOver();
    }
}
