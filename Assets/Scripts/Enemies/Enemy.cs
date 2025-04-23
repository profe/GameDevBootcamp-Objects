using UnityEngine;
using System;

public abstract class Enemy : PlayableObject
{
    [SerializeField] protected float speed;
    [SerializeField] protected float attackRange, attackTime = 0;
    protected Transform target;

    private float timer = 0;
    protected float setSpeed = 0;

    public void SetEnemy(float attackRange, float attackTime)
    {
        this.attackRange = attackRange;
        this.attackTime = attackTime;
    }

    protected virtual void Start()
    {
        setSpeed = speed;
        try
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        catch (NullReferenceException nre)
        {
            Debug.Log("There is no player in the scene. Enemy destroying self " + nre);
            Destroy(gameObject);
            GameManager.GetInstance().SetEnemySpawnState(false);
        }

    }
    protected virtual void Update()
    {
        if (target != null)
        {
            Move(target.position);
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
        else
        {
            Move(speed);
        }
    }

    public override void Move(float speed) //to be used if there is no player in the scene
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void Move(Vector2 direction) //if player in scene, move towards player
    {
        direction.x -= transform.position.x;
        direction.y -= transform.position.y;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
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
            Shoot();
        }
    }
    public override void Die()
    {
        //Debug.Log("Enemy died");
        //GameManager.GetInstance().scoreManager.IncrementScore(); //could refactor method to take in a score to increment by. then each enemy can have their own value for incrementing score by
        GameManager.GetInstance().NotifyDeath(this);
        GameManager.GetInstance().PlaySound(Sound.EnemyDestroyed);
        GameManager.GetInstance().scoreManager.IncrementScore();
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        //Debug.Log($"Enemy amaged");
        health.DeductHealth(damage);
        if (health.CurrentHealth <= 0)
        {
            Die();
        }
    }
}
