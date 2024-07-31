using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanController : MonoBehaviour
{
    public float speed = 3f; // D��man�n h�z�n� belirler
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
            // Oyuncuya do�ru hareket et
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // D��man�n y�z�n� oyuncuya do�ru d�nd�r
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Sola bak
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1); // Sa�a bak
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Oyuncuya �arpt���nda yap�lacak i�lemler

            Destroy(gameObject); // D��man� yok et
        }

        if (collision.gameObject.CompareTag("Ates"))
        {
            Destroy(gameObject); // Ates gelince d��man�n �lmesi
        }
    }
}


