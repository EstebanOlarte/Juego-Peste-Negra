using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Spawner : MonoBehaviour {

    [SerializeField]
    Transform[] puntosEnElMapa;
    [SerializeField]
    Transform[] puntosParaLaLlave;
    [SerializeField]
    Transform[] puntosVacuna;
    [SerializeField]
    int cantidadDeIas;
    [SerializeField]
    GameObject[] Aldeanos;
    [SerializeField]
    GameObject llave;
    [SerializeField]
    GameObject particles;
    [SerializeField]
    GameObject[] vacunas;
    [SerializeField]
    float cantidadVacunas;

	// Use this for initialization
	void Start () {

        Instantiate(llave,puntosParaLaLlave[Random.Range(0, puntosParaLaLlave.Length)].position,Quaternion.identity);

        for (int i = 0; i < cantidadVacunas; i++)
        {
            int r = Random.Range(0,3);
            Instantiate( vacunas[r], puntosVacuna[Random.Range(0, puntosEnElMapa.Length)].position, Quaternion.identity);
        }

        for (int i = 0; i < cantidadDeIas; i++)
        {
            GameObject aldeano = Instantiate(Aldeanos[Random.Range(0,Aldeanos.Length)],puntosEnElMapa[Random.Range(0,puntosEnElMapa.Length)].position ,Quaternion.identity);
            int r = Random.Range(0, 6);

            Instantiate(particles, aldeano.transform);

            if(r <=2)      //Grudger
            {
                aldeano.AddComponent(typeof(Grudger));
            }
            else if(r == 4)     //Survivor
            {
                aldeano.AddComponent(typeof(Survivor));
            }
            else            //Denizen
            {
                aldeano.AddComponent(typeof(Denizen));
            }
        }
	}
}
