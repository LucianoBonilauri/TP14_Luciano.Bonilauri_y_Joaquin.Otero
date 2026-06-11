using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public int totalColeccionables = 10;

    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public float tiempoTotal = 120f; // 2 minutos, cambialo a gusto

    [Header("Mensajes")]
    public GameObject panelVictoria;
    public GameObject panelGameOver;
    public TextMeshProUGUI mensajeRecoleccion; // texto "¡Recolectado!" que aparece y desaparece

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
    void ActualizarBarra()
{
    if (barraTimer == null) return;

    float porcentaje = tiempoRestante / tiempoTotal;
    barraTimer.fillAmount = porcentaje;


    if (porcentaje > 0.5f)

        barraTimer.color = new Color(0.686f, 1f, 0.666f);
    else if (porcentaje > 0.25f)

        barraTimer.color = new Color(0.223f, 1f, 0.078f);

        barraTimer.color = new Color(1f, 0.2f, 0.2f);
}
}