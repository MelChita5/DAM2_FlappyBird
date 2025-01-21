using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Componente que muestra las imágenes del jugador
    public Sprite[] sprites; // Array con los sprites para animar al jugador (tipo "batir las alas")
    private int spriteIndex; // Índice para llevar control del sprite actual.

    private Vector3 direction; // La dirección del jugador (hacia dónde y con qué fuerza se mueve)
    public float gravity = -9.8f; // La gravedad que afecta al jugador, hacia abajo (negativo)
    public float strength = 5f; // Fuerza del salto cuando presionas "Space" o haces clic

    private AudioSource audioSource; // Componente para manejar sonidos
    public AudioClip deathSound; // Sonido cuando te estrellas con un obstáculo
    public AudioClip scoringSound; // Sonido cuando pasas un obstáculo y anotas

    private void Awake()
    {
        // Inicializa componentes importantes cuando el script carga
        spriteRenderer = GetComponent<SpriteRenderer>(); // Conecta al SpriteRenderer del jugador
        audioSource = GetComponent<AudioSource>(); // Conecta al AudioSource para reproducir sonidos
    }

    private void Start()
    {
        // Empieza a cambiar los sprites cada 0.15 segundos para simular una animación
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        // Resetea la posición y dirección del jugador cuando se habilita el objeto
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

        if (Input.touchCount > 0) // Para pantallas táctiles (toques).
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // Cuando empieza el toque
            {
                direction = Vector3.up * strength; // Salta igual que con teclado o clic
            }
        }

        // Aplica gravedad, haciendo que el jugador caiga con el tiempo
        direction.y += gravity * Time.deltaTime;

        // Actualiza la posición del jugador con base en la dirección
        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprite()
    {
        // Cambia el sprite del jugador para simular la animación
        spriteIndex++;

        if (spriteIndex >= sprites.Length) // Si se pasa del último sprite, vuelve al primero
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex]; // Actualiza el sprite del jugador
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta colisiones con triggers (como obstáculos y puntos)
        if (other.gameObject.tag == "Obstacle") // Si choca con un obstáculo:
        {
            if (audioSource != null && deathSound != null) // Reproduce el sonido de muerte si está configurado
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
