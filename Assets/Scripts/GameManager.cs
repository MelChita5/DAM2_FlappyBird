using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private int score; // Esta variable guarda el puntaje del jugador. Empieza en 0 siempre.

    public Text scoreText; // Texto que muestra el puntaje en pantalla.
    public GameObject playButtom; // Botón de "Play", aparece al inicio o cuando pierdes.
    public GameObject gameOver; // Mensaje de "Game Over", se muestra cuando pierdes.
    public GameObject Back; // Otro objeto en la escena, tal vez para volver al menú o algo.

    public Player player; // Referencia al jugador, porque sin el jugador no hay juego, ¿no?

    private bool firstTime = true; // Esto sirve para ver si es la primera vez que se carga la escena.

    private void Awake()
    {
        Application.targetFrameRate = 60; // Bloquea el juego a 60 FPS, porque más es innecesario y menos es meh.

        // Estos if verifican si las cosas importantes están configuradas. Si no, tiran un warning en el log.
        if (player == null) Debug.LogWarning("Player no está asignado en esta escena.");
        if (scoreText == null) Debug.LogWarning("ScoreText no está asignado en el Inspector.");
        if (playButtom == null) Debug.LogWarning("PlayButton no está asignado en el Inspector.");
        if (gameOver == null) Debug.LogWarning("GameOver no está asignado en el Inspector.");
        if (Back == null) Debug.LogWarning("Back no está asignado en el Inspector.");

        // Si es la primera vez que abres la escena, arranca el juego directamente.
        if (firstTime)
        {
            StartFirstGame();
        }
        else
        {
            Pause(); // Si no, lo deja pausado.
        }
    }

    private void StartFirstGame()
    {
        // Esta función inicia el juego sin mostrar el botón de "Play".
        firstTime = false; // Así nos aseguramos que no vuelve a entrar acá.
        Play(); // Llama al método para iniciar el juego.
    }

    public void Play()
    {
        score = 0; // Resetea el puntaje porque estamos empezando.
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Muestra el puntaje en pantalla.
        }

        // Oculta el botón de "Play", el mensaje de "Game Over" y el Back.
        if (playButtom != null) playButtom.SetActive(false);
        if (gameOver != null) gameOver.SetActive(false);
        if (Back != null) Back.SetActive(false);

        Time.timeScale = 1f; // Hace que el tiempo avance normal otra vez (no en pausa).

        if (player != null)
        {
            player.enabled = true; // Activa al jugador para que pueda moverse.
        }

        // Encuentra todos los tubos en la escena y los destruye. Limpieza total.
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f; // Pausa el juego.

        if (player != null)
        {
            player.enabled = false; // Desactiva al jugador para que no pueda moverse mientras está pausado.
        }
    }

    public void GameOver()
    {
        // Muestra el botón de "Play" y el mensaje de "Game Over".
        if (playButtom != null) playButtom.SetActive(true);
        if (gameOver != null) gameOver.SetActive(true);
        if (Back != null) Back.SetActive(false); 

        Pause(); 
    }

    public void IncreaseScore()
    {
        score++; // Suma 1 al puntaje.
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Actualiza el texto en pantalla.
        }
    }

    public void ExitGame()
    {
        Application.Quit(); // Cierra el juego, pero solo funciona en la versión final.

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // En el editor, detiene el juego.
        #endif
    }
}

