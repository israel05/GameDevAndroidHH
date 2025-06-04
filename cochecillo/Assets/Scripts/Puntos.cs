using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puntos : MonoBehaviour
{

    [SerializeField] private TMP_Text marcador; // Puntos del jugador
    public const string maximoActualKey = "MaximaPuntuacion";
    // Clave para guardar la puntuación máxima del jugador mediante PlayerPrefs

    private float puntosJugador = 0f; // Puntos del jugador 
   


    // Update is called once per frame
    void Update()
    {
        puntosJugador += Time.deltaTime; // Incrementa los puntos del jugador con el tiempo
        marcador.text = "Puntos: " + Mathf.RoundToInt(puntosJugador).ToString(); // Actualiza el marcador con los puntos del jugador
    }

    private void OnDestroy()
    {
        // Guarda los puntos del jugador al destruir el objeto
        int maximoActual =  PlayerPrefs.GetInt(maximoActualKey, 0);
        //la palabra "MaximaPuntuacion" se usa para guardar la puntuación máxima del jugador
        // hace referencia a una clave constante de esta clase "maximoActualKey"
        if (puntosJugador > maximoActual)
        {
            PlayerPrefs.SetInt(maximoActualKey, Mathf.RoundToInt(puntosJugador)); 
            // Guarda los puntos del jugador si son mayores que los guardados
            PlayerPrefs.Save(); // Asegura que los datos se guarden
        }
    }
}

