using System;
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

            DropOption[] dropOptions = {
                new DropOption(0.5f, 0),    // 50% para el valor 0
                new DropOption(0.3f, 1) // 0.3% para el valor 10
                // Puedes agregar más opciones aquí
            };

            int selectedDrop = ChooseDrop(dropOptions);

            dropMobs.CreateDropsForMobs(selectedDrop);

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

    private int ChooseDrop(DropOption[] dropOptions)
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        float cumulativeProbability = 0f;

        foreach (var option in dropOptions)
        {
            cumulativeProbability += option.probability;
            if (randomValue <= cumulativeProbability)
            {
                return option.dropValue;
            }
        }

        return dropOptions[dropOptions.Length - 1].dropValue; // Por si acaso [ultimo obj]
    }
}
struct DropOption
{
    public float probability;
    public int dropValue;

    public DropOption(float probability, int dropValue)
    {
        this.probability = probability;
        this.dropValue = dropValue;
    }
}
