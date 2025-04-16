using UnityEngine;

public class Bullet : MonoBehaviour
{
    public const float DEFAULT_SPEED = 10f;
    public const float DEFAULT_DAMAGE = 1f;
    public const string DEFAULT_TARGET_TAG = "Player";

    [SerializeField] private float speed, damage;
    private string targetTag;

    public Bullet(float damage, string targetTag, float speed)
    {
        SetBullet(damage, targetTag, speed);
    }

    public Bullet() : this(DEFAULT_DAMAGE, DEFAULT_TARGET_TAG, DEFAULT_SPEED) { }

    public Bullet(float damage) : this(damage, DEFAULT_TARGET_TAG, DEFAULT_SPEED) { }

    public Bullet(float damage, string targetTag) : this(damage, targetTag, DEFAULT_SPEED) { }

    public void SetBullet(float damage, string targetTag, float speed = DEFAULT_SPEED)
    {
        this.damage = damage;
        this.targetTag = targetTag;
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return speed;
    }
    public float GetDamage()
    {
        return damage;
    }

    public string GetTargetTag()
    {
        return targetTag;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void Damage(IDamageable damageable)
    {
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            //Debug.Log("Damaged something!");
            GameManager.GetInstance().scoreManager.IncrementScore();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(targetTag))
        {
            return; //tags dont match, end this ontriggerenter method
        }

        IDamageable damageable = other.GetComponent<IDamageable>();
        Damage(damageable);
    }
}
