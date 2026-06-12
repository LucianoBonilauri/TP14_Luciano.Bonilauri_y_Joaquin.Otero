using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    public float timer = 60f;

    private UIManager uiManager;
    private bool timerActivo = true;

    void Awake()
    {
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
        if (!timerActivo) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            timerActivo = false;
        }

        if (uiManager != null)
        {
            uiManager.UpdateTimer(timer);
        }
    }
}