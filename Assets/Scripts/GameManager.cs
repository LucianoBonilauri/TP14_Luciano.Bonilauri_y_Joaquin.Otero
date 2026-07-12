using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    public float timer = 60f;
    private UIManager uiManager;
    private bool timerActivo = true;
    private bool juegoTerminado = false;

    void Awake()
    {
        Time.timeScale = 0;
        uiManager = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        if (uiManager != null)
        {
            uiManager.tiempoTotal = timer;
            uiManager.UpdateTimer(timer);
        }
    }

    void Update()
    {
        if (juegoTerminado && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

        if (!timerActivo || juegoTerminado) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            timerActivo = false;
            juegoTerminado = true;

            if (uiManager != null)
                uiManager.MostrarPantallaGameOver();

            Time.timeScale = 0;
        }

        if (uiManager != null)
            uiManager.UpdateTimer(timer);
    }
}