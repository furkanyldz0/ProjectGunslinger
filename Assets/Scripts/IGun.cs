using System;
using UnityEngine;

public interface IGun
{//sadece genel tip belirtmek için kullandýk

    public event EventHandler<GunOnShotEventArgs> GunOnShot;
    public event EventHandler GunOnReload;
    public event EventHandler<GunOnFinishedReloadingEventArgs> GunOnFinishedReloading;

    public class GunOnShotEventArgs : EventArgs {
        public int magazine;
        public Vector3 impactPosition;
        public Vector3 raycastHitNormal;
    }

    public class GunOnFinishedReloadingEventArgs : EventArgs {
        public int magazine;
    }

    public void Shoot();

    public void Reload();

    public int GetMagazineCapacity();
}
