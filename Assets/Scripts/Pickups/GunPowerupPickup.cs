using System.Collections;
using UnityEngine;

public class GunPowerupPickup : Pickup
{
    public override void OnPicked()
    {
        base.OnPicked();
        GameManager.GetInstance().GetPlayer().SetHasGunPowerup(true); //turn on gunpowerup

        GameManager.GetInstance().PlaySound(Sound.GunPowerupPickup); //start sound, plays 10 seconds
        StartCoroutine(TurnOffGunPowerup(2.0f));//set player hasgunpickup to false after 10 seconds
        // Invoke("TurnOffGunPowerup", 2.0f);

    }

    // public void TurnOffGunPowerup()
    // {
    //     GameManager.GetInstance().GetPlayer().SetHasGunPowerup(false);
    //     Debug.Log("Turned off powerup!");
    // }
    IEnumerator TurnOffGunPowerup(float time)
    {
        //Debug.Log("before turning off gun powerup");
        yield return new WaitForSeconds(time);
        Debug.Log("after turning off gun powerup");
        GameManager.GetInstance().GetPlayer().SetHasGunPowerup(false);
    }
}
