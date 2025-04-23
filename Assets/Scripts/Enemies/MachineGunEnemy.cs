using UnityEngine;

public class MachineGunEnemy : Enemy
{
    [SerializeField] private float firingRate;
    private float shootingTimer = 0;

    protected override void Start()
    {
        base.Start();
        health = new Health(1, 0, 1);
        //weaponBulletPrefab.SetTargetTag("Player");
        weapon = new Weapon("Machine Gun", weaponBulletPrefab);
    }



    //not used but have to implement for abstract class
    public override void Move(Vector2 direction, Vector2 target)
    {

    }
    //melee enemies don't shoot, so leaving it blank
    public override void Shoot()
    {
        shootingTimer += Time.deltaTime;
        if (shootingTimer < (1.0f / firingRate))
        {
            GameManager.GetInstance().PlaySound(Sound.MachineGunEnemyShoot);
            weapon.Shoot(this);
            shootingTimer = 0;
        }
    }
}
