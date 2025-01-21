using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 5f; // Velocidad a la que se mueveen los tubos hacia la izquierda
    private float leftEdge; // Límite izquierdo de la pantalla donde los tubos se destruyen

    private void Start()
    {
        // Calcula la posición más a la izquierda visible en la pantalla
        // Esto lo hace transformando el punto de la esquinaa inferior izquierda de la pantalla (Vector3.zero) al mundo
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f; // Deja un margen extra de 1 unidad
    }

    private void Update()
    {
        // Mueve el tubo hacia la izquierda en cada frame
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Si el tubo se pasa del límite izquierdo, lo destruye para limpiar la escena
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject); 
        }
    }
}

