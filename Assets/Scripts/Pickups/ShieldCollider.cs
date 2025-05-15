using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    public ShieldPickup owner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null && bullet.GetTargetTag() == "Player")
            {
                Vector2 reflectDir = Vector2.Reflect(bullet.transform.right, (bullet.transform.position - transform.position).normalized);
                bullet.transform.right = reflectDir;
                bullet.SetTargetTag("Enemy");
            }
        }
    }
}
