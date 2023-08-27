using System;
using UnityEngine;
public class Zombie : MonoBehaviour
{
    [SerializeField] private int currentLifeZombie;
    [SerializeField] private int maxLifeZombie = 10;
    [SerializeField] private int damage = 10;
    public static bool dead = false;
    private Rigidbody2D rb2D;
    [SerializeField] private FloatingHealthBar floatingHealthBar;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        floatingHealthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    private void Start()
    {
        currentLifeZombie = maxLifeZombie;
        floatingHealthBar?.UpdateFloatingHealthBar(currentLifeZombie, maxLifeZombie);
    }
    private void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        currentLifeZombie -= damage;

        floatingHealthBar?.UpdateFloatingHealthBar(currentLifeZombie, maxLifeZombie);

        if (currentLifeZombie <= 0)
        {
            DropMobs dropMobs = GetComponent<DropMobs>();

            DropOption[] dropOptions = {
                new DropOption(0.1f, 2), // 0.1% para el valor 10
                new DropOption(0.2f, 1), // 0.2% para el valor 10
                new DropOption(0.7f, 0)    // 70% para el valor 0
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
