using Unity.Mathematics.Geometry;
using UnityEngine;

public class PistolGun_Visual : MonoBehaviour
{
    [SerializeField] private AutoGun pistolGun;
    private Animator animator;
    private const string HIT = "Hit";
    private const string RELOAD1 = "Reload1";
    private const string RELOAD2 = "Reload2";

    private void Start() {
        animator = GetComponent<Animator>();
        pistolGun.GunOnShot += PistolGun_GunOnShot;
        pistolGun.GunOnReload += PistolGun_GunOnReload;
    }

    private void PistolGun_GunOnReload(object sender, System.EventArgs e) {
        int randomReloadAnimationIndex = Random.Range(0, 2);
        
        if(randomReloadAnimationIndex == 0)
            animator.SetTrigger(RELOAD1);
        else
            animator.SetTrigger(RELOAD2);
    }

    private void PistolGun_GunOnShot(object sender, System.EventArgs e) {
        animator.SetTrigger(HIT);
    }
}
