using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;              // Prefab que vamos a "reciclar" en el pool
    public int poolSize = 10;              // Tamaño del pool, cuántos objetos se crean al inicio
    private Queue<GameObject> pool;        // Cola donde almacenamos los objetos para reutilizarlos

    private void Awake()
    {
        // Inicializamos la cola del pool.
        pool = new Queue<GameObject>();

        // Creamos objetos y los metemos en el pool.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);  // Creamos una instancia del prefab
            obj.SetActive(false);  // Lo desactivamos para que no aparezca al principio
            pool.Enqueue(obj);     // Lo agregamos a la cola
        }
    }

    // Método para sacar un objeto del pool.
    public GameObject GetObject()
    {
        // Si hay objetos disponibles en el pool, los usamos.
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();  // Sacamos un objeto de la cola.
            obj.SetActive(true);  // Activamos el objeto para que se vea en pantalla.
            return obj;
        }
        else
        {
            // Si no hay objetos en el pool, creamos uno nuevo.
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }

    // Método para devolver un objeto al pool (lo desactivamos para no verlo)
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);  // Desactivamos el objeto antes de devolverlo al pool

        pool.Enqueue(obj);     // Lo metemos de nuevo en la cola para reutilizarlo después.
    }
}

