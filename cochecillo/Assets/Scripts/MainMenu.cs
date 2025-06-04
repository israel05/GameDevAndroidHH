using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text puntuacionMaxima;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button playButton; // Bot�n para iniciar el juego
    // Texto para mostrar la puntuaci�n m�xima del jugador

    [SerializeField] private AndroidNotificationHanlder androidNotificationHandler;

    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargerDuration;

    private int energy;

    // Clave para guardar la energ�a del jugador mediante PlayerPrefs
    private const string energyKey = "Energy";    
    private const string energyReadyKey = "EnergyReady";

    public void Play()
    {
        if(energy < 1) { return; }
        energy--;

        PlayerPrefs.SetInt(energyKey, energy);

        if (energy == 0)
        {
            DateTime oneMinuteFromNow = DateTime.Now.AddMinutes(1);
            PlayerPrefs.SetString(energyReadyKey, oneMinuteFromNow.ToString());
            //vamos a meter unas directivas del compilador para ver si es UNITY_ANDROID
            //solo funcionara si estamos en Android
#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(oneMinuteFromNow);
#endif
        }


        SceneManager.LoadScene("Escena_Carrera");
    }


    private void Start()
    {
        OnApplicationFocus(true);
    }


    private void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus)
        {
          return; // Si la aplicaci�n pierde el foco, no hacemos nada
        }
        CancelInvoke(nameof(EnergyRecharged));  //que deje de contar

        // Carga la puntuaci�n m�xima del jugador al iniciar el men�
        //de la clase puntos, sabiendo que usa PlayerPrefs lee el valor de maximoActualKey
        int maximoActual = PlayerPrefs.GetInt(Puntos.maximoActualKey, 0);
        puntuacionMaxima.text = "Puntuaci�n M�xima: " + maximoActual.ToString();
        // Actualiza el texto con la puntuaci�n m�xima guardada


        //como tengo que leer la cantaidad de energia del juegador
        //la obtengo del PlayerPrefs con la clave energyKey
        energy = PlayerPrefs.GetInt(energyKey, maxEnergy);
        if(energy == 0)
        {
            string energyReadySyting = PlayerPrefs.GetString(energyReadyKey, string.Empty);
            //no tenias energia, por lo que la pongo a 0
            if (energyReadyKey == string.Empty) { return; }
            DateTime energyReady =DateTime.Parse(energyReadySyting);

            if(DateTime.Now >= energyReady)
            {
                //si la fecha y hora actual es mayor o igual a la fecha y hora de cuando se recarg� la energ�a
                energy = maxEnergy; //recargo la energ�a al m�ximo
                PlayerPrefs.SetInt(energyKey, energy); //guardo la energ�a en PlayerPrefs
               
            }
            else
            {
                playButton.interactable = false; // Habilita el bot�n de jugar
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
                //usar nameof te ayuda a que no sea un string y cometas fallos
            }
        } 
        energyText.text = "Jugar (" + energy.ToString() + ")";
    }
    private void EnergyRecharged()
    {
        playButton.interactable = true; // Habilita el bot�n de jugar
        energy = maxEnergy; //recargo la energ�a al m�ximo
        PlayerPrefs.SetInt(energyKey, energy); //guardo la energ�a en PlayerPrefs
        energyText.text = "Jugar (" + energy.ToString() + ")";

    }
}


