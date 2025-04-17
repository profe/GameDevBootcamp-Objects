using UnityEngine;

public class ShooterEnemy : Enemy
{
    [SerializeField] private Color laserColor = Color.red;
    [SerializeField] private float width;

    private LineRenderer laser;

    protected override void Start()
    {
        base.Start();
        health = new Health(1, 0, 1);
        //weaponBulletPrefab.SetTargetTag("Player");
        weapon = new Weapon("Death Ray", weaponBulletPrefab);

        //setup laser
        laser = gameObject.AddComponent<LineRenderer>();
        laser.startWidth = width;
        laser.endWidth = width;
        laser.startColor = laserColor;
        laser.endColor = laserColor;
    }

    protected override void Update()
    {
        if (target != null)
        {
            Move(target.position);
            if (Vector2.Distance(transform.position, target.position) < attackRange)
            {
                speed = 0; //stop moving while attacking within range
                //draw laser
                laser.positionCount = 2;
                laser.SetPosition(0, gameObject.transform.position);
                laser.SetPosition(1, target.position);

                Attack(attackTime);
            }
            else
            {
                speed = setSpeed; //move again while not in attack range
                //remove laser
                laser.positionCount = 0;
            }
        }
        else
        {
            Move(speed);
        }
    }

    //not used but have to implement for abstract class
    public override void Move(Vector2 direction, Vector2 target)
    {

    }
    //melee enemies don't shoot, so leaving it blank
    public override void Shoot()
    {
        GameManager.GetInstance().PlaySound(Sound.ShooterEnemyShoot);
        //shoot bullet
        weapon.Shoot(this);
    }
}
