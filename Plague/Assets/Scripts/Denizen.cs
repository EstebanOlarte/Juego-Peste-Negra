using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denizen : IA {

	// Use this for initialization
	void Start () {
		
	}

    protected override void Movimiento()
    {
        // Seguir una ruta 
        // Poner Varias rutas predeinidas en el mapa, que el busque la mas cercana
        // Mover al Denizen usando un MoveTowardsPoint hacia esa ruta
        // Cada ruta está compuesta por un grupo de puntos, al interatur con algúno de los puntos te envia al siguente en la lista
        // Formando un loop
    }
}
