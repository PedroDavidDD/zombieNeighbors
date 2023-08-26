using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private float maxLife = 1000;
    [SerializeField] private float currentLife;

    // Start is called before the first frame update
    void Awake()
    {
        currentLife = maxLife;
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
        Player player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        player.isLive = false;
        Time.timeScale = 0f;
    }

    public float getCurrentLife(){
        return this.currentLife;
    }

}
