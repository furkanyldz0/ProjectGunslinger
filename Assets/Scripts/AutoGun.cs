using UnityEngine;

public class AutoGun : MonoBehaviour
{
    public float rateOfFire;
    public int magazine;

    private float rateOfFireDelta;
    private Vector2 screenCenterPoint;

    [SerializeField] private FireWeaponSO fireWeaponSO;
    [SerializeField] private Transform bulletPrefab;

    private void Start()
    {
        Player.Instance.OnShot += Instance_OnShot;
        screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        rateOfFire = fireWeaponSO.rateOfFire;
        magazine = fireWeaponSO.magazine;

    }

    private void Update() {
        if(rateOfFireDelta >= 0) {
            rateOfFireDelta -= Time.deltaTime;
        }
    }

    private void Instance_OnShot(object sender, System.EventArgs e) {
        bool canShoot = rateOfFireDelta <= 0f && magazine > 0;
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
        Debug.Log("Reload");
        magazine = fireWeaponSO.magazine;
    }


}
