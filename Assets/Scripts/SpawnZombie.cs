using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour
{
    [SerializeField] float cooldown = 3f;
    [SerializeField] float time;
    [SerializeField] GameObject[] zombie;
    [SerializeField] private int currentLife = 5;
    int zombieCount = 0; // Contador de zombies en la escena
    [SerializeField] int maxZombie = 20;
    [SerializeField] private Transform objBborn;

    [SerializeField] bool finCreacionZombies = false;
    void Update()
    {
        // Obtener la cantidad inicial de zombies en la escena
        Zombie[] zombies = FindObjectsOfType<Zombie>();
        zombieCount = zombies.Length;

        if (zombieCount < maxZombie && !finCreacionZombies)
        {
            if (time >= cooldown && zombie != null)
            {
                int getIndexZombie = Random.Range(0, zombie.Length);
                // Instanciar zombies cooldown
                GameObject SpawnZombies = Instantiate(zombie[getIndexZombie], objBborn.position, Quaternion.identity);

                time = 0f;
            }
            else
            {
                time += Time.deltaTime;
            }
        }
        else
        {
            finCreacionZombies = true;
        }
    }
    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        if (currentLife <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(this.gameObject);
    }

}
