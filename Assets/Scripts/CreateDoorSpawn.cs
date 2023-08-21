using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDoorSpawn : MonoBehaviour
{
    [SerializeField] GameObject spawn;
    [SerializeField] private Transform[] pointSpawnZombie;
    [SerializeField] Collider2D coll;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {           
            for (int i = 0; i < pointSpawnZombie.Length; i++)
            {
                GameObject SpawnZombies = Instantiate(spawn, pointSpawnZombie[i].position, Quaternion.identity);
            }
            coll.isTrigger = false;
        }
    }
}
