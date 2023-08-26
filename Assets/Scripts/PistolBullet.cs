using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet
{
    private void Start()
    {
        damage = 5;
        speed = 3f;
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        Invoke("Die", .5f);
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.gameObject.tag;

        switch (tag)
        {
            case "Door":
                Door door = other.gameObject.GetComponent<Door>();
                if (door != null)
                {
                    door.TakeDamage(damage);
                }
                break;
            case "Zombie":
                Zombie zombie = other.gameObject.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.TakeDamage(damage);
                }
                break;
            case "Skeleton":
                SpawnZombie spawnZombie = other.gameObject.GetComponent<SpawnZombie>();
                if (spawnZombie != null)
                {
                    spawnZombie.TakeDamage(damage);
                }
                break;
            default: break;
        }
        Destroy(this.gameObject);
    }

}