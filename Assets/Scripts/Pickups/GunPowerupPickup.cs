using System.Collections;
using UnityEngine;

public class GunPowerupPickup : Pickup
{
    [SerializeField] private float gunPowerupTime;

    public override void OnPicked()
    {
        base.OnPicked();

        GameManager.GetInstance().GetPlayer().StartPowerUpCoroutine(gunPowerupTime);
        GameManager.GetInstance().PlaySound(Sound.GunPowerupPickup);
    }

}
