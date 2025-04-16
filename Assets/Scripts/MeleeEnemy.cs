using UnityEngine;

public class MeleeEnemy : Enemy
{

    protected override void Start()
    {
        base.Start();
        health = new Health(1, 0, 1);
        weapon = new Weapon("Melee Detonator", weaponBulletPrefab);
    }



    //not used but have to implement for abstract class
    public override void Move(Vector2 direction, Vector2 target)
    {

    }
    //melee enemies don't shoot, so leaving it blank
    public override void Shoot()
    {
        //target.GetComponent<IDamageable>().TakeDamage(weapon.GetDamage()); //this would be touch damage for being within range if left in
        weapon.Shoot(this, attackTime * 10); //should show detonation "bullet" based on attack time (which is very short, so *10)
        Debug.Log($"Melee enemy detonated, trying for damage : {weapon.GetDamage()}");
        Destroy(gameObject);
    }
}
