using UnityEngine;

public class ShooterEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        health = new Health(1, 0, 1);
        //weaponBulletPrefab.SetTargetTag("Player");
        weapon = new Weapon("Death Ray", weaponBulletPrefab);
    }



    //not used but have to implement for abstract class
    public override void Move(Vector2 direction, Vector2 target)
    {

    }
    //melee enemies don't shoot, so leaving it blank
    public override void Shoot()
    {

    }
}
