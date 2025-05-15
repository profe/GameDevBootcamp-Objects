using UnityEngine;

public class ShieldPickup : Pickup
{
    [SerializeField] private float shieldPowerUpTime = 5f;
    [SerializeField] private GameObject shieldRingPrefab;
    private GameObject activeShield;

    public override void OnPicked()
    {
        base.OnPicked();
        Debug.Log("Shield picked up");

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && shieldRingPrefab != null)
        {
            activeShield = Instantiate(shieldRingPrefab, player.transform.position, Quaternion.identity);
            activeShield.transform.SetParent(player.transform);
            activeShield.transform.localPosition = Vector3.zero;
            activeShield.transform.localRotation = Quaternion.identity;

            ShieldCollider shieldLogic = activeShield.AddComponent<ShieldCollider>();
            shieldLogic.owner = this;

            Destroy(activeShield, shieldPowerUpTime);
        }
    }
}
