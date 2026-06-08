using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText;
    public int totalColeccionables = 10; 

    private int score = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ActualizarTexto();
    }

    public void Recolectar()
    {
        score++;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        scoreText.text = score + " / " + totalColeccionables;
    }
}