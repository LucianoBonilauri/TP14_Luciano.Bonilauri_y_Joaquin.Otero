using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public int totalColeccionables = 10;

    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public Image barraTimer; 
    public float tiempoTotal = 120f;


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
    }

    void Update()
    {
        if (juegoTerminado) return;

        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
        }

        ActualizarTextoTimer();
        ActualizarBarra(); 
    }

public void Recolectar()
{
    score++;
    ActualizarTextoScore();
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
    if (porcentaje > 0.5f)
    {
        barraTimer.color = new Color32(175, 255, 170, 255); 
    }
    else if (porcentaje > 0.25f)
    {
        barraTimer.color = new Color32(57, 255, 20, 255);  
    }
    else
    {
        barraTimer.color = new Color32(255, 51, 51, 255); 
    }
}

}