using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanController : MonoBehaviour
{
    public float speed = 3f; // Düþmanýn hýzýný belirler
    private Transform player; // Hedef oyuncunun transformu

    private void Start()
    {
        // Player tagine sahip oyuncuyu bul ve hedef olarak belirle
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Oyuncuya doðru hareket et
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Düþmanýn yüzünü oyuncuya doðru döndür
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Sola bak
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1); // Saða bak
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Oyuncuya çarptýðýnda yapýlacak iþlemler

            Destroy(gameObject); // Düþmaný yok et
        }

        if (collision.gameObject.CompareTag("Ates"))
        {
            Destroy(gameObject); // Ates gelince düþmanýn ölmesi
        }
    }
}


