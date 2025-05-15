using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : Enemy
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float firingRate;
    private float shootingTimer = 0;

    [SerializeField] private int sprayBulletCount = 5;
    [SerializeField] private float sprayAngle = 45f;

    protected override void Start()
    {
        base.Start();
        health = new Health(15, 0, 15);
        weapon = new Weapon("Spray Bullet", weaponBulletPrefab);
        healthSlider.value = 1.0f;
    }

    protected override void Update()
    {
        base.Update();
        SprayShoot();
    }

    private void SprayShoot()
    {
        shootingTimer += Time.deltaTime;
        if (shootingTimer >= (1.0f / firingRate))
        {
            GameManager.GetInstance().PlaySound(Sound.MachineGunEnemyShoot);

            float angleStep = sprayAngle / (sprayBulletCount - 1);
            float startAngle = -sprayAngle / 2f;

            for (int i = 0; i < sprayBulletCount; i++)
            {
                float currentAngle = startAngle + angleStep * i;
                Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);

                Bullet tempBullet = GameObject.Instantiate(weaponBulletPrefab, transform.position, transform.rotation * rotation);
                tempBullet.SetBullet(weapon.GetDamage(), weapon.GetTarget(), weapon.GetSpeed());

                GameObject.Destroy(tempBullet.gameObject, 5f);
            }

            shootingTimer = 0;
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        //update slider to reflect damage
        healthSlider.value = health.CurrentHealth / health.GetMaxHealth();
    }

    public override void Move(Vector2 direction, Vector2 target)
    {

    }

    public override void Shoot()
    {

    }
}
