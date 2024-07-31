using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtesController : MonoBehaviour
{
    public float speed = 10f; // Merminin hýzýný belirler
    public float lifetime = 2f; // Merminin ömrü (kaç saniye sonra yok olacak)
    private Transform target;
    private Vector2 direction;

    private void Start()
    {
        // Merminin ömrünü baþlat
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (direction != Vector2.zero)
        {
            // Mermiyi ileriye doðru hareket ettir
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
            transform.right = direction; // Ateþin ucunu hedefe doðru yönlendir
        }
        else
        {
            direction = transform.right; // Hedef yoksa düz ileriye doðru hareket et
            Debug.Log("No enemy found. Default direction is set.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mermi bir nesneye çarptýðýnda yok olmasýný saðlar
        if (collision.gameObject.CompareTag("Dusman"))
        {
            // Düþmaný etkileyin (örneðin, düþmanýn saðlýðýna zarar verin)
            // Bu kýsmý ihtiyacýnýza göre düzenleyebilirsiniz

            Destroy(collision.gameObject); // Düþmaný yok et
            Destroy(gameObject); // Mermiyi yok et
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
