using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoPanelText;
    private IGun currentGun;
    private int currentGunMaxCapacity;

    private void Start()
    {
        currentGun = Player.Instance.GetCurrentGun();
        currentGun.GunOnShot += CurrentGun_GunOnShot;
        currentGun.GunOnFinishedReloading += CurrentGun_GunOnFinishedReloading;

        currentGunMaxCapacity = currentGun.GetMagazineCapacity();
        UpdateAmmoInfo(currentGunMaxCapacity);
    }

    private void CurrentGun_GunOnShot(object sender, IGun.GunOnShotEventArgs e) {
        UpdateAmmoInfo(e.magazine);
    }

    private void CurrentGun_GunOnFinishedReloading(object sender, IGun.GunOnFinishedReloadingEventArgs e) {
        UpdateAmmoInfo(e.magazine);
    }

    private void UpdateAmmoInfo(int ammo) { //?
        ammoPanelText.SetText(ammo + " / " + currentGunMaxCapacity);
    }

}
