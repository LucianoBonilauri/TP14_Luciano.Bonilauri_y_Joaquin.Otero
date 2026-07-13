using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModoTerror : MonoBehaviour
{
    [Header("Referencias")]
    public Light linterna;
    public Light[] lucesAApagar;   // luces Realtime/Mixed que quieras que se apaguen (las Baked no reaccionan en vivo)
    private UIManager uiManager;

    private bool modoTerrorActivo = false;

    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        if (linterna != null)
            linterna.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            modoTerrorActivo = !modoTerrorActivo;
            ActivarModoTerror(modoTerrorActivo);
        }
    }

    void ActivarModoTerror(bool activar)
    {
        if (linterna != null)
            linterna.enabled = activar;

        foreach (Light luz in lucesAApagar)
        {
            if (luz != null)
                luz.enabled = !activar;
        }

        uiManager?.ActivarOverlayOscuridad(activar);
    }
}