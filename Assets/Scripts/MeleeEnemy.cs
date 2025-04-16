using UnityEngine;

public class MeleeEnemy : Enemy
{

    [SerializeField] private float attackRange, attackTime = 0;

    private float timer = 0;

    private float setSpeed = 0;

    protected override void Start()
    {
        base.Start();
        health = new Health(1, 0, 1);
        setSpeed = speed;
        weapon = new Weapon("Melee Detonator", weaponBulletPrefab);

    }

    protected override void Update()
    {
        base.Update();
        if (target == null)
        {
            return;
        }

        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            speed = 0; //stop moving while attacking within range
            Attack(attackTime);
        }
        else
        {
            speed = setSpeed; //move again while not in attack range
        }
    }


    public override void Attack(float interval)
    {
        if (timer <= interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            //target.GetComponent<IDamageable>().TakeDamage(weapon.GetDamage()); //this would be touch damage for being within range if left in
            weapon.Shoot(this, attackTime * 10); //should show detonation "bullet" based on attack time (which is very short, so *10)
            Debug.Log($"Melee enemy detonated, trying for damage : {weapon.GetDamage()}");
            Destroy(gameObject);
        }
    }

    public void SetMeleeEnemy(float attackRange, float attackTime)
    {
        this.attackRange = attackRange;
        this.attackTime = attackTime;
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
