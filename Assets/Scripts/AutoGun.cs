using System;
using UnityEngine;

public class AutoGun : MonoBehaviour, IGun
{
    //WeaponSO alanlarý
    private float rateOfFire;
    private int magazine;
    private float reloadTime;

    public event EventHandler<IGun.GunOnShotEventArgs> GunOnShot;
    public event EventHandler GunOnReload;
    public event EventHandler<IGun.GunOnFinishedReloadingEventArgs> GunOnFinishedReloading;
    
    private float rateOfFireDelta;
    private bool isReloading;
    private Vector2 screenCenterPoint;

    [SerializeField] private FireWeaponSO fireWeaponSO;

    private void Start()
    {
        Player.Instance.PlayerOnShot += Player_OnShot;
        Player.Instance.PlayerOnReload += Player_OnReload;
        screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        rateOfFire = fireWeaponSO.rateOfFire;
        magazine = fireWeaponSO.magazine;
    }

    private void Update() {
        if (rateOfFireDelta >= 0) {
            rateOfFireDelta -= Time.deltaTime;
        }

        HandleReload();
    }

    private void HandleReload() {
        if (reloadTime >= 0) {
            reloadTime -= Time.deltaTime;
            if (reloadTime < 0) {
                magazine = fireWeaponSO.magazine;
                isReloading = false;
                GunOnFinishedReloading?.Invoke(this, new IGun.GunOnFinishedReloadingEventArgs {
                    magazine = magazine
                });
                Debug.Log("Reloaded");
            }
        }
    }

    private void Player_OnReload(object sender, System.EventArgs e) {
        Reload();
    }

    private void Player_OnShot(object sender, System.EventArgs e) {
        bool canShoot = rateOfFireDelta <= 0f && magazine > 0 && !isReloading;
        if (canShoot) {
            Shoot();
            rateOfFireDelta = rateOfFire;
        }
        if(magazine == 0) {
            Reload();
        }
    }

    public void Shoot() {
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity)) {
            magazine--;
            
            GunOnShot?.Invoke(this, new IGun.GunOnShotEventArgs {
                impactPosition = raycastHit.point,
                raycastHitNormal = raycastHit.normal,
                magazine = magazine
            });

            if (raycastHit.transform.TryGetComponent<Enemy>(out Enemy enemy)) {
                enemy.Damage(33f);
            }

            //Debug.Log(magazine);
        }
    }

    public void Reload() {
        if (!isReloading) {
            isReloading = true;
            reloadTime = fireWeaponSO.reloadTime;
            GunOnReload?.Invoke(this, EventArgs.Empty);
            Debug.Log("Reloading");
        }
    }

    public int GetMagazineCapacity() {
        return fireWeaponSO.magazine;
    }


}
