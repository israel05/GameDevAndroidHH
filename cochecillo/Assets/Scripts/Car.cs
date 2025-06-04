using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField]    float speed = 10f; // Speed of the car  
    [SerializeField] float incrementoDeVelocidadEnTIempo = 0.1f; // Incremento de velocidad en el tiempo
     

    [SerializeField] float velocidadCantidadGiro = 100f; // Giro de la carroza

    private int cantidadGiro;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed += incrementoDeVelocidadEnTIempo * Time.deltaTime;

        transform.Rotate(0f, cantidadGiro* Time.deltaTime *  velocidadCantidadGiro, 0f);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }

    public void SetCantidadGiro(int cantidadGiro)
    {
        this.cantidadGiro = cantidadGiro;
        Debug.Log("Cantidad de giro: " + cantidadGiro);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            Debug.Log("Has chocado");
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
