using StarterAssets;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public event EventHandler PlayerOnShot;
    public event EventHandler PlayerOnReload;

    private StarterAssetsInputs input;

    //[SerializeField] float shootTimeout = 0.05f; //ateþlenme durumunu bu sürede bi kontrol etsin
    private float shootTimeoutDelta;

    private void Awake() {
        if (Instance != null) {
            Debug.Log("Sahnede birden fazla player var!");
        }
        Instance = this;
    }

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
    }

    
    private void Update()
    {
        HandleShoot();
    }

    private void HandleShoot() {
        //if(shootTimeoutDelta >= 0f) {
        //    shootTimeoutDelta -= Time.deltaTime;
        //}

        if (input.fire) {
            PlayerOnShot?.Invoke(this, EventArgs.Empty);
            //shootTimeoutDelta = shootTimeout;
        }

        if (input.reload) {
            PlayerOnReload?.Invoke(this, EventArgs.Empty);
            input.reload = false;
        }
    }
}
