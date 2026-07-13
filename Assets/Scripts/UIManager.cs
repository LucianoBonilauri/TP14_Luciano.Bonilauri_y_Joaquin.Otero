using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public int totalColeccionables = 10;

    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public Image barraTimer;
    public float tiempoTotal = 60f;

    [Header("Cartel de interacción")]
    public GameObject cartelInteraccion;

    [Header("Pantallas de fin")]
    public GameObject panelWin;
    public GameObject panelGameOver;

    [Header("Pantalla de inicio")]
    public GameObject panelInicio;

    private bool juegoTerminado = false;
    private bool juegoIniciado = false;

    void Start()
    {
        UpdateScore(0);
        UpdateTimer(tiempoTotal);

        if (panelInicio != null)
            panelInicio.SetActive(true);

        if (cartelInteraccion != null)
            cartelInteraccion.SetActive(false);
    }

    void Update()
    {
        if (!juegoIniciado && panelInicio != null && panelInicio.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                panelInicio.SetActive(false);
                juegoIniciado = true;
                Time.timeScale = 1;
            }
            return;
        }

        if (juegoTerminado && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = score + " / " + totalColeccionables;
    }

    public void UpdateTimer(float timer)
    {
        timer = Mathf.Clamp(timer, 0, tiempoTotal);

        if (timerText != null)
        {
            int minutos = Mathf.FloorToInt(timer / 60);
            int segundos = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }

        if (barraTimer != null)
        {
            float porcentaje = Mathf.Clamp01(timer / tiempoTotal);
            barraTimer.fillAmount = porcentaje;

            if (porcentaje > 0.5f)
                barraTimer.color = new Color32(175, 255, 170, 255);
            else if (porcentaje > 0.25f)
                barraTimer.color = new Color32(255, 220, 80, 255);
            else
                barraTimer.color = new Color32(255, 51, 51, 255);
        }
    }

    public void MostrarCartelInteraccion(bool mostrar)
    {
        if (cartelInteraccion != null)
            cartelInteraccion.SetActive(mostrar);
    }

    public void MostrarPantallaWin()
    {
        if (panelWin != null)
            panelWin.SetActive(true);
        juegoTerminado = true;
    }

    public void MostrarPantallaGameOver()
    {
        if (panelGameOver != null)
            panelGameOver.SetActive(true);
        juegoTerminado = true;
    }
}