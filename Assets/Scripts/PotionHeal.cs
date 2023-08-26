using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHeal : MonoBehaviour
{
    [SerializeField] private float incLife = 100f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Life life = other.gameObject.GetComponent<Life>();
            if (life != null && life.getCurrentLife() < life.getMaxLife())
            {
                life.setCurrentLife(incLife);
                Destroy(this.gameObject);
            }
        }
    }
}
