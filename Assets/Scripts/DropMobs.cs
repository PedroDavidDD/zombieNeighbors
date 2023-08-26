using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMobs : MonoBehaviour
{
    [SerializeField] public GameObject[] drops;

    public  void CreateDropsForMobs(int amountRepeatMobs = 0)
    {

        Zombie.dead = true;
        if (Zombie.dead && drops != null)
        {
            GameObject createDrops = Instantiate(drops[amountRepeatMobs],
                                                     transform.position,
                                                     Quaternion.Euler(0f, 0f, transform.eulerAngles.z));
            Zombie.dead = false;
        }

    }
}
