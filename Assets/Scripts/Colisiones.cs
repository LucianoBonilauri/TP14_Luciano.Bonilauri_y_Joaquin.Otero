using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
if (other.gameObject.CompareTag("Coleccionable")){
    UIManager.Instance.Recolectar(); 
    Debug.Log("Recolectado los " + other.gameObject.name);
    Destroy(other.gameObject);
}
    }
}
