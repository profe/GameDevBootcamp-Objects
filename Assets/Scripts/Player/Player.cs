using System;
using System.Collections;
using UnityEngine;

public class Player : PlayableObject
{

    private string nickName;

    [SerializeField] private float speed;
    [SerializeField] private float weaponDamage = 1, bulletSpeed = 10;
    [SerializeField] private float maxHealth;
    [SerializeField] private float startHealth;
    [SerializeField] private float regenRate;

    public Action OnDeath;

    private bool hasGunPowerup;
    private Rigidbody2D playerRB;
    private Camera cam;

    //changed from Start to awake to ensure that it runs first so action subscription stuff works (that depends on players health existing)
    private void Awake()
    {
        health = new Health(maxHealth, regenRate, startHealth);
        playerRB = GetComponent<Rigidbody2D>();
        cam = Camera.main; //important for prefab player since camera cant be set in unity inspector

        weaponBulletPrefab.SetBullet(weaponDamage, "Enemy", bulletSpeed);
        weapon = new Weapon("Player Weapon", weaponBulletPrefab);
    }

    private void Update()
    {
        health.RegenHealth();
    }

    public override void Move(Vector2 direction, Vector2 target)
    {
        //Debug.Log("Player is moving");
        playerRB.linearVelocity = direction * speed * Time.deltaTime;

        var playerScreenPos = cam.WorldToScreenPoint(transform.position); //using player as reference point to get screen position
        target.x -= playerScreenPos.x;
        target.y -= playerScreenPos.y;

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg; //inverse tangent of y (opposite) / x (adjacent) and converted to degrees
        transform.rotation = Quaternion.Euler(0, 0, angle); //rotating on Z axis!
    }

    public override void Shoot()
    {
        //Debug.Log("Shooting a bullet");
        GameManager.GetInstance().PlaySound(Sound.PlayerShoot);
        weapon.Shoot(this);
    }

    public override void Attack(float interval)
    {
        Debug.Log($"Attacking at {interval} interval");
    }

    public override void Die()
    {
        Debug.Log("Player died");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        //Debug.Log($"Player took {damage} damage");
        GameManager.GetInstance().PlaySound(Sound.PlayerHurt);
        health.DeductHealth(damage);
        if (health.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public bool HasGunPowerup()
    {
        return hasGunPowerup;
    }

    public void StartPowerUpCoroutine(float time)
    {
        hasGunPowerup = true;
        StartCoroutine(TurnOffGunPowerup(time));
    }

    IEnumerator TurnOffGunPowerup(float time)
    {
        yield return new WaitForSeconds(time);
        hasGunPowerup = false;
    }
}
