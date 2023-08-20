using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] int lifeDoor = 3;

    void Die()
    {
        Destroy(this.gameObject);
    }
    public void TakeDamage(int damage)
    {
        lifeDoor -= damage;
        if (lifeDoor <= 0){
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D other1)
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
        }
    }
}
