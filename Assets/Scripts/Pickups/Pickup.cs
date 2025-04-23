using UnityEngine;

public abstract class Pickup : MonoBehaviour, IDamageable
{
    public virtual void OnPicked()
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //only destroy and heal if touched by player or player's bullet
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerBullet"))
        {
            OnPicked();
        }
        //else keep on screen
    }


    public void TakeDamage(float damage)
    {

    }
}
