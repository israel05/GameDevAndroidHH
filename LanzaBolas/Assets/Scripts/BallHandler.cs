using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.Rendering.DebugUI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject pelotaPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private float respawnDelay = 2f;
    [SerializeField] private float lanzamientoDelay = 1.5f;

    private SpringJoint2D currentPelotaMuelle;
    private Rigidbody2D currentPelotaRB;
    private Camera mainCamera;
    private bool estaSiendoArrastrada = false;

    void Start()
    {
        mainCamera = Camera.main;

        generarNuevaPeloata();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();

    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();

    }

    private void Update() {

        if (pelotaPrefab == null) { return; }


        if (Touch.activeTouches.Count == 0)
        {
            if (estaSiendoArrastrada)
            {
                LanzaLaPelota();
            }
            estaSiendoArrastrada = false;
            return;

        }

        estaSiendoArrastrada = true;
        currentPelotaRB.bodyType = RigidbodyType2D.Kinematic;



        Vector2 touchPosition = new Vector2();

        foreach (Touch touch in Touch.activeTouches)
        {
            touchPosition += touch.screenPosition;
        }

        touchPosition /= Touch.activeTouches.Count;

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        currentPelotaRB.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);



    }

    private void LanzaLaPelota()
    {
        currentPelotaRB.bodyType = RigidbodyType2D.Dynamic;

        Invoke(nameof(releaseTheBall), lanzamientoDelay);
    }

    private void releaseTheBall()
    {
        currentPelotaRB.bodyType = RigidbodyType2D.Static;

        Invoke(nameof(generarNuevaPeloata), respawnDelay);
    }

    private void generarNuevaPeloata()
    {
        GameObject nuevaPelota = Instantiate(pelotaPrefab, pivot.position, Quaternion.identity);
        currentPelotaRB = nuevaPelota.GetComponent<Rigidbody2D>();
        currentPelotaMuelle = nuevaPelota.GetComponent<SpringJoint2D>();
        currentPelotaMuelle.connectedBody = pivot;
    }
}


