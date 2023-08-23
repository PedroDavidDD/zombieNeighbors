using NUnit.Framework;
using UnityEngine;
public class Zombie : MonoBehaviour
{
    [SerializeField] private int lifeZombie = 10;
    [SerializeField] private int damage = 10;
    public static bool dead = false;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        lifeZombie -= damage;
        if (lifeZombie <= 0)
        {
            DropMobs dropMobs = GetComponent<DropMobs>();
            dropMobs.CreateDropsForMobs();

            Die();
        }
    }
    void Die()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        string tag = other.gameObject.tag;

        switch (tag)
        {
            case "Player":
                Life life = other.gameObject.GetComponent<Life>();
                if (life != null)
                {
                    life.TakeDamage(damage);
                }
                break;
            default: break;
        }
    }
}
 