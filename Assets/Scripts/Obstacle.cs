using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Define un evento para cuando el obst�culo se salga de la pantalla
    public System.Action OnOutOfScreen;

    private void Update()
    {
        // Si el obst�culo ha pasado el l�mite de la pantalla (x < -10f), dispara el evento
        if (transform.position.x < -10f)
        {
            // Invoca el evento (si est� suscrito a algo), esto se usa para, por ejemplo, devolverlo al pool
            OnOutOfScreen?.Invoke();
        }
    }
}
