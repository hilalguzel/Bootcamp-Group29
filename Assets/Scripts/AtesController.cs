using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtesController : MonoBehaviour
{
    public float speed = 10f; // Merminin h�z�n� belirler
    public float lifetime = 2f; // Merminin �mr� (ka� saniye sonra yok olacak)

    private Transform target;

    private void Start()
    {
        // Merminin �mr�n� ba�lat
        Destroy(gameObject, lifetime);

        // En yak�ndaki d��man� bul
        FindClosestEnemy();
    }

    private void Update()
    {
        if (target != null)
        {
            // En yak�ndaki d��mana do�ru hareket et
            Vector2 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        else
        {
            // Hedef yoksa d�z ileriye do�ru hareket et
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Dusman");
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mermi bir nesneye �arpt���nda yok olmas�n� sa�lar
        if (collision.gameObject.CompareTag("Dusman"))
        {
            // D��man� etkileyin (�rne�in, d��man�n sa�l���na zarar verin)
            // Bu k�sm� ihtiyac�n�za g�re d�zenleyebilirsiniz

            Destroy(gameObject); // Mermiyi yok et
        }
    }
}
