using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public ObjectPool objectPool; // Referencia a la piscina de objetos, porque no vamos a estar instanciando como locos
    public GameObject prefab; // Prefab de lo que queremos spawnear (los tubos, seguro)
    public float spawnRate = 1f; // Cada cu�nto tiempo se generan los tubos
    public float minHeight = -1f; // Altura m�nima para spawnear un tubo
    public float maxHeight = 1f; // Altura m�xima para spawnear un tubo

    private void OnEnable()
    {
        // Cuando este script se activa, empieza a invocar el m�todo Spawn a intervalos de "spawnRate"
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        // Cuando este script se desactiva, cancela los invokes porque no necesitamos que siga spawneando cosas
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        // Saca un objeto de la piscina, como un tubo
        GameObject pipes = objectPool.GetObject();

        // Cambia la posici�n del tubo al punto del spawner, pero con un desplazamiento aleatorio en Y
        pipes.transform.position = transform.position + Vector3.up * Random.Range(minHeight, maxHeight);

        // Intenta conseguir el script del obst�culo del tubo, pero aqu� no parece que hagamos nada con eso
        Obstacle obstacleScript = pipes.GetComponent<Obstacle>();

    }

    private void ReturnPipeToPool(GameObject pipes)
    {
        // Busca el script de Obstacle en el tubo.
        Obstacle obstacleScript = pipes.GetComponent<Obstacle>();

        // Desuscribimos el evento OnOutOfScreen para evitar memory leaks o errores locos
        obstacleScript.OnOutOfScreen -= () => ReturnPipeToPool(pipes);

        // Regresa el tubo a la piscina para que podamos usarlo despu�s
        objectPool.ReturnObject(pipes);
    }
}

