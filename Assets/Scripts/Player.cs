using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Componente que muestra las im�genes del jugador
    public Sprite[] sprites; // Array con los sprites para animar al jugador (tipo "batir las alas")
    private int spriteIndex; // �ndice para llevar control del sprite actual.

    private Vector3 direction; // La direcci�n del jugador (hacia d�nde y con qu� fuerza se mueve)
    public float gravity = -9.8f; // La gravedad que afecta al jugador, hacia abajo (negativo)
    public float strength = 5f; // Fuerza del salto cuando presionas "Space" o haces clic

    private AudioSource audioSource; // Componente para manejar sonidos
    public AudioClip deathSound; // Sonido cuando te estrellas con un obst�culo
    public AudioClip scoringSound; // Sonido cuando pasas un obst�culo y anotas

    private void Awake()
    {
        // Inicializa componentes importantes cuando el script carga
        spriteRenderer = GetComponent<SpriteRenderer>(); // Conecta al SpriteRenderer del jugador
        audioSource = GetComponent<AudioSource>(); // Conecta al AudioSource para reproducir sonidos
    }

    private void Start()
    {
        // Empieza a cambiar los sprites cada 0.15 segundos para simular una animaci�n
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        // Resetea la posici�n y direcci�n del jugador cuando se habilita el objeto
        Vector3 position = transform.position;
        position.y = 0f; // Pone al jugador en el centro en el eje Y
        transform.position = position;
        direction = Vector3.zero; // Sin movimiento al principio
    }

    private void Update()
    {
        // Detecta inputs del jugador para hacer que "salte".
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // Teclado o clic
        {
            direction = Vector3.up * strength; // Salta hacia arriba con la fuerza configurada
        }

        if (Input.touchCount > 0) // Para pantallas t�ctiles (toques).
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // Cuando empieza el toque
            {
                direction = Vector3.up * strength; // Salta igual que con teclado o clic
            }
        }

        // Aplica gravedad, haciendo que el jugador caiga con el tiempo
        direction.y += gravity * Time.deltaTime;

        // Actualiza la posici�n del jugador con base en la direcci�n
        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprite()
    {
        // Cambia el sprite del jugador para simular la animaci�n
        spriteIndex++;

        if (spriteIndex >= sprites.Length) // Si se pasa del �ltimo sprite, vuelve al primero
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex]; // Actualiza el sprite del jugador
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta colisiones con triggers (como obst�culos y puntos)
        if (other.gameObject.tag == "Obstacle") // Si choca con un obst�culo:
        {
            if (audioSource != null && deathSound != null) // Reproduce el sonido de muerte si est� configurado
            {
                audioSource.Play();
            }
            FindObjectOfType<GameManager>().GameOver(); // Llama al GameManager para terminar el juego
        }
        else if (other.gameObject.tag == "Scoring") // Si pasa por un punto de scoring:
        {
            if (audioSource != null && scoringSound != null) // Reproduce el sonido de puntaje
            {
                audioSource.PlayOneShot(scoringSound);
            }
            FindObjectOfType<GameManager>().IncreaseScore(); // Llama al GameManager para sumar puntos
        }
    }
}
