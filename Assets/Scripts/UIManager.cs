using System.Collections;
using UnityEngine;
using UnityEngine.UI; // <-- IMPORTANTE: Añadido para que reconozca el componente Image
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public int totalColeccionables = 10;

    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public Image barraTimer; // <-- CORREGIDO: Declaramos la barra para que aparezca en el Inspector
    public float tiempoTotal = 120f; 

    [Header("Mensajes")]
    public GameObject panelVictoria;
    public GameObject panelGameOver;
    public TextMeshProUGUI mensajeRecoleccion; 

    private int score = 0;
    private float tiempoRestante;
    private bool juegoTerminado = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        tiempoRestante = tiempoTotal;
        ActualizarTextoScore();

        if (panelVictoria != null) panelVictoria.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (mensajeRecoleccion != null) mensajeRecoleccion.gameObject.SetActive(false);
    }

    void Update()
    {
        if (juegoTerminado) return;

        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            GameOver();
        }

        ActualizarTextoTimer();
        ActualizarBarra(); // <-- CORREGIDO: Ahora sí llamamos a la función en cada frame
    }

    public void Recolectar()
    {
        score++;
        ActualizarTextoScore();

        if (mensajeRecoleccion != null)
            StartCoroutine(MostrarMensaje());

        if (score >= totalColeccionables)
            Victoria();
    }

    void ActualizarTextoScore()
    {
        if (scoreText != null)
            scoreText.text = score + " / " + totalColeccionables;
    }

    void ActualizarTextoTimer()
    {
        if (timerText == null) return;

        int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        timerText.color = tiempoRestante < 20f ? Color.red : Color.white;
    }

   void ActualizarBarra()
{
    if (barraTimer == null) return;

    float porcentaje = tiempoRestante / tiempoTotal;
    barraTimer.fillAmount = porcentaje;

    // Usamos Color32 para asegurar el Alpha (el último número, 255 es totalmente visible)
    if (porcentaje > 0.5f)
    {
        barraTimer.color = new Color32(175, 255, 170, 255); // Verde claro
    }
    else if (porcentaje > 0.25f)
    {
        barraTimer.color = new Color32(57, 255, 20, 255);  // Verde oscuro / Amarillo
    }
    else
    {
        barraTimer.color = new Color32(255, 51, 51, 255);  // Rojo crítico
    }
}

    IEnumerator MostrarMensaje()
    {
        mensajeRecoleccion.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        mensajeRecoleccion.gameObject.SetActive(false);
    }

    void Victoria()
    {
        juegoTerminado = true;
        if (panelVictoria != null) panelVictoria.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void GameOver()
    {
        juegoTerminado = true;
        if (panelGameOver != null) panelGameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}