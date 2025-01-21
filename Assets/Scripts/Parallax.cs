
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer; // Componente que maneja la visualización del material (el fondo o lo que sea)
    public float animationSpeed = 0.4f; // Velocidad de desplazamiento de la textura, como si se moviera un fondo

    private void Awake()
    {
        // Obtiene el MeshRenderer del objeto. Esto es lo que nos permite modificar la textura del objeto
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Mueve la textura del material en el eje X para crear el efecto de parallax
        // Básicamente, va desplazando la textura poco a poco, creando un movimiento constante
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
