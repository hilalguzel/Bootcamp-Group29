using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AtesController : MonoBehaviour
{
    public float speed = 10f; // Merminin h�z�n� belirler
    public float lifetime = 2f; // Merminin �mr� (ka� saniye sonra yok olacak)
    private Transform target;
    private Vector2 direction;

    private void Start()
    {
        // Merminin �mr�n� ba�lat
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (direction != Vector2.zero)
        {
            // Mermiyi ileriye do�ru hareket ettir
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        else
        {
            Debug.LogError("Direction is not set for the fireball.");
        }
    }

    public void FindClosestEnemy()
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
            direction = (target.position - transform.position).normalized;
            transform.right = direction; // Ate�in ucunu hedefe do�ru y�nlendir
        }
        else
        {
            direction = transform.right; // Hedef yoksa d�z ileriye do�ru hareket et
            Debug.Log("No enemy found. Default direction is set.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mermi bir nesneye �arpt���nda yok olmas�n� sa�lar
        if (collision.gameObject.CompareTag("Dusman"))
        {
            // D��man� etkileyin (�rne�in, d��man�n sa�l���na zarar verin)
            // Bu k�sm� ihtiyac�n�za g�re d�zenleyebilirsiniz

            Destroy(collision.gameObject); // D��man� yok et
            Destroy(gameObject); // Mermiyi yok et
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
