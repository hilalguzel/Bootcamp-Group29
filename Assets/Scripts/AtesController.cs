using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtesController : MonoBehaviour
{
    public float speed = 10f; // Merminin hýzýný belirler
    public float lifetime = 2f; // Merminin ömrü (kaç saniye sonra yok olacak)

    private Transform target;

    private void Start()
    {
        // Merminin ömrünü baþlat
        Destroy(gameObject, lifetime);

        // En yakýndaki düþmaný bul
        FindClosestEnemy();
    }

    private void Update()
    {
        if (target != null)
        {
            // En yakýndaki düþmana doðru hareket et
            Vector2 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        else
        {
            // Hedef yoksa düz ileriye doðru hareket et
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
        // Mermi bir nesneye çarptýðýnda yok olmasýný saðlar
        if (collision.gameObject.CompareTag("Dusman"))
        {
            // Düþmaný etkileyin (örneðin, düþmanýn saðlýðýna zarar verin)
            // Bu kýsmý ihtiyacýnýza göre düzenleyebilirsiniz

            Destroy(gameObject); // Mermiyi yok et
        }
    }
}
