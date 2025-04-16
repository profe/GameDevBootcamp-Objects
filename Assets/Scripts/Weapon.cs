using UnityEngine;

public class Weapon
{
    private string name;
    private Bullet bullet;

    public Weapon(string name, Bullet bullet)
    {
        this.name = name;
        this.bullet = bullet;
    }

    public Weapon() : this("Default", null) { } //creates "empty" weapon

    public void Shoot(PlayableObject player, float timeToDie = 5)
    {
        Debug.Log($"Shooting from weapon {name} with {GetDamage()} damage");
        Bullet tempBullet = GameObject.Instantiate(bullet, player.transform.position, player.transform.rotation);
        tempBullet.SetBullet(GetDamage(), GetTarget(), GetSpeed());

        GameObject.Destroy(tempBullet.gameObject, timeToDie);
    }

    public float GetDamage()
    {
        return bullet.GetDamage();
    }

    public float GetSpeed()
    {
        return bullet.GetSpeed();
    }

    public string GetTarget()
    {
        return bullet.GetTargetTag();
    }

}
