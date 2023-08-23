using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMobs : MonoBehaviour
{
    [SerializeField] public GameObject[] drops;

    public  void CreateDropsForMobs(int amountRepeatMobs = 1)
    {

        Zombie.dead = true;
        if (Zombie.dead && drops != null)
        {
            for (var i = 0; i < amountRepeatMobs; i++)
            {
                GameObject createDrops = Instantiate(drops[i],
                                                     transform.position,
                                                     Quaternion.Euler(0f, 0f, transform.eulerAngles.z));
            }
            Zombie.dead = false;
        }

    }
}
