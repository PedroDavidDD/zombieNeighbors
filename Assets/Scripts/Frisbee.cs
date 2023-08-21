using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frisbee : MonoBehaviour
{
    [SerializeField] private float frisbeeSpeed = 5f;
    [SerializeField] private Transform player;

    private Rigidbody2D rb2D;

    [SerializeField, Header("Cooldown Para regresar")] private float skillTimeCooldown = .5f;
    [SerializeField] private float skillTime;

    [SerializeField] private int damage = 1;
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Buscamos el Player por su tag
    }
    private void FixedUpdate()
    {
        if (skillTime >= skillTimeCooldown)
        {
            ChangeDirectionAfterDelay();
        }
        else
        {
            transform.Translate(frisbeeSpeed * Time.deltaTime, 0, 0);
            skillTime += Time.deltaTime;
        }

    }
    private void ChangeDirectionAfterDelay()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        rb2D.MovePosition(rb2D.position + directionToPlayer * frisbeeSpeed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Die();
        }

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
            case "SpawnZombie":
                SpawnZombie spawnZombie = other.gameObject.GetComponent<SpawnZombie>();
                if (spawnZombie != null)
                {
                    spawnZombie.TakeDamage(damage);
                }
                break;
            default: break;
        }

    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
}
