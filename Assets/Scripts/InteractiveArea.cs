using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveArea : MonoBehaviour
{
    private UIManager uiManager;
    public int score = 0;

    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        if (uiManager != null)
        {
            uiManager.UpdateScore(score);
        }
    }

private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("Coleccionable"))
    {
        other.gameObject.tag = "Untagged";
        score++;

        Debug.Log("Score actual: " + score + " / Total: " + uiManager.totalColeccionables);

        if (uiManager != null)
        {
            uiManager.UpdateScore(score);

            if (score >= uiManager.totalColeccionables)
            {
                uiManager.MostrarPantallaWin();
                Time.timeScale = 0;
            }
        }

        Destroy(other.gameObject);
    }
}
    
}