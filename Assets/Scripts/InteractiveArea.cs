using UnityEngine;

public class InteractiveArea : MonoBehaviour
{
    [Header("Referencias")]
    public Camera camaraJugador;
    private UIManager uiManager;

    [Header("Configuración")]
    public float distanciaInteraccion = 3f;
    public LayerMask capasIgnoradas; // asignar la layer "Player" acá

    public int score = 0;
    private GameObject objetoActual;

    void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (camaraJugador == null)
            camaraJugador = Camera.main;
    }

    void Start()
    {
        if (uiManager != null)
            uiManager.UpdateScore(score);
    }

    void Update()
    {
        DetectarColeccionable();

        if (objetoActual != null && Input.GetKeyDown(KeyCode.E))
        {
            Recolectar(objetoActual);
        }
    }

    void DetectarColeccionable()
    {
        Ray ray = new Ray(camaraJugador.transform.position, camaraJugador.transform.forward);
        int mascara = ~capasIgnoradas; // el "~" invierte: golpea todo MENOS lo que pusiste ahí

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaInteraccion, mascara))
        {
            Debug.Log("Rayo pegó en: " + hit.collider.name + " | Tag: " + hit.collider.tag);

            if (hit.collider.CompareTag("Coleccionable"))
            {
                objetoActual = hit.collider.gameObject;
                uiManager?.MostrarCartelInteraccion(true);
                return;
            }
        }

        objetoActual = null;
        uiManager?.MostrarCartelInteraccion(false);
    }

    void Recolectar(GameObject objeto)
    {
        objeto.tag = "Untagged";
        score++;

        if (uiManager != null)
        {
            uiManager.UpdateScore(score);
            uiManager.MostrarCartelInteraccion(false);

            if (score >= uiManager.totalColeccionables)
            {
                uiManager.MostrarPantallaWin();
                Time.timeScale = 0;
            }
        }

        Debug.Log("Recolectado: " + objeto.name);
        Destroy(objeto);
    }
}