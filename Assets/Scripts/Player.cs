using StarterAssets;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler PlayerOnShot;
    public event EventHandler PlayerOnReload;

    private StarterAssetsInputs input;

    [SerializeField] private GameObject equippedGunObject; //ui'yý ammo bilgisini falan yönetmek için
    private IGun equippedGun;


    private void Awake() {
        if (Instance != null) {
            Debug.Log("Sahnede birden fazla player var!");
        }
        Instance = this;

        equippedGun = equippedGunObject.GetComponent<IGun>();
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

        if (input.fire) {
            PlayerOnShot?.Invoke(this, EventArgs.Empty);
        }

        if (input.reload) {
            PlayerOnReload?.Invoke(this, EventArgs.Empty);
            input.reload = false;
        }
    }

    public IGun GetCurrentGun() { //oyuncu silaha baðlý deðil, mevcut aktif silahý elde etmek için kullanacaðýz
        return equippedGun;
    }

    public void SetCurrentGun(IGun gun) {
        equippedGun = gun;
    }
}
