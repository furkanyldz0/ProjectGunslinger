using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class PistolGun_Visual : MonoBehaviour
{
    [SerializeField] private AutoGun pistolGun;
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private ParticleSystem impactEffect;
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

    private void PistolGun_GunOnShot(object sender, AutoGun.GunOnReloadEventArgs e) {
        animator.SetTrigger(HIT);
        muzzleEffect.Play();
        //ParticleSystem hitEffect = Instantiate(impactEffect, e.impactPosition, Quaternion.identity);
        GameObject hitEffect = ObjectPool.SharedInstance.GetPooledObject();

        if (hitEffect.TryGetComponent<ParticleSystem>(out ParticleSystem particleSystem)) {
            particleSystem.transform.position = new Vector3(e.impactPosition.x,
                e.impactPosition.y, e.impactPosition.z);
            particleSystem.transform.rotation = Quaternion.Euler(Vector3.zero);
            particleSystem.gameObject.SetActive(true);
            StartCoroutine(DisableHitEffect(particleSystem, 1f));
        }
    }

    private IEnumerator DisableHitEffect(ParticleSystem particleSystem ,float delay) {
        yield return new WaitForSeconds(delay);
        particleSystem.gameObject.SetActive(false);
    }

}
