using UnityEngine;

public class PlayerBullet : Bullet
{

    //for player bullet, just want to increment score
    public override void Damage(IDamageable damageable)
    {
        base.Damage(damageable);
    }
}