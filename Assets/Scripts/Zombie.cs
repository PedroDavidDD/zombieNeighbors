using UnityEngine;
public class Zombie : MonoBehaviour
{
    [SerializeField] private int lifeZombie = 5;
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
            Die();
        }
    }
    void Die()
    {
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
 