using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Orbes : MonoBehaviour
{
    [SerializeField] float points = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CanvasManager.amountSliderOrbs += (points);
            Destroy(gameObject);
        }
    }
}
