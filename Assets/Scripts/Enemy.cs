using UnityEngine;
using System;

public abstract class Enemy : PlayableObject
{
    [SerializeField] protected float speed;
    protected Transform target;
    private EnemyType enemyType;


    protected virtual void Start()
    {
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
        }
        else
        {
            Move(speed);
        }
    }

    public void SetEnemyType(EnemyType enemyType)
    {
        this.enemyType = enemyType;
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


    public override void Die()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log($"Enemy amaged");
        health.DeductHealth(damage);
        if (health.CurrentHealth <= 0)
        {
            Die();
        }
    }
}
