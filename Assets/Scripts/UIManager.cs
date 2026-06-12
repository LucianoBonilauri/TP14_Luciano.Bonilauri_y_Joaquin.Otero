using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public int totalColeccionables = 10;

    [Header("Timer")]
    public TextMeshProUGUI timerText;
    public Image barraTimer;
    public float tiempoTotal = 60f;

    void Start()
    {
        UpdateScore(0);
        UpdateTimer(tiempoTotal);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = score + " / " + totalColeccionables;
        }
    }

    public void UpdateTimer(float timer)
    {
        timer = Mathf.Clamp(timer, 0, tiempoTotal);

        ActualizarTextoTimer(timer);
        ActualizarBarraTimer(timer);
    }

    private void ActualizarTextoTimer(float timer)
    {
        if (timerText == null) return;

        int minutos = Mathf.FloorToInt(timer / 60);
        int segundos = Mathf.FloorToInt(timer % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        if (timer <= 20f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }
    }

    private void ActualizarBarraTimer(float timer)
    {
        if (barraTimer == null) return;

        float porcentaje = Mathf.Clamp01(timer / tiempoTotal);
        barraTimer.fillAmount = porcentaje;

        if (porcentaje > 0.5f)
        {
            barraTimer.color = new Color32(175, 255, 170, 255);
        }
        else if (porcentaje > 0.25f)
        {
            barraTimer.color = new Color32(255, 220, 80, 255);
        }
        else
        {
            barraTimer.color = new Color32(255, 51, 51, 255);
        }
    }
}