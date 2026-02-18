using UnityEngine;

public class AutoGun : MonoBehaviour
{
    //WeaponSO properties
    private float rateOfFire;
    private int magazine;
    private float reloadTime;
    
    private float rateOfFireDelta;
    private bool isReloading;
    private Vector2 screenCenterPoint;

    [SerializeField] private FireWeaponSO fireWeaponSO;
    [SerializeField] private Transform bulletPrefab;

    private void Start()
    {
        Player.Instance.OnShot += Player_OnShot;
        Player.Instance.OnReload += Player_OnReload;
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
            var bullet = Instantiate(bulletPrefab, raycastHit.point, Quaternion.identity);
            Destroy(bullet.gameObject, 20f);
            Debug.Log(magazine);
        }
    }

    public void Reload() {
        if (!isReloading) {
            isReloading = true;
            reloadTime = fireWeaponSO.reloadTime;
            Debug.Log("Reloading");
        }
    }


}
