using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtesController : MonoBehaviour
{
    public float speed = 10f; // Merminin hızını belirler
    public float lifetime = 2f; // Merminin ömrü (kaç saniye sonra yok olacak)
    private Transform target;
    private Vector2 direction;

    public CharacterController karakter;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Merminin ömrünü başlat
    }

    private void Update()
    {
        if (direction != Vector2.zero)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World); // Mermiyi ileriye doğru hareket ettir
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
            transform.right = direction; // Ateşin ucunu hedefe doğru yönlendir
        }
        else
        {
            direction = transform.right; // Hedef yoksa düz ileriye doğru hareket et
            Debug.Log("No enemy found. Default direction is set.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dusman"))
        {
            Destroy(collision.gameObject); // Düşmanı yok et
            Destroy(gameObject); // Mermiyi yok et
            CharacterController.Instance.puan++;
            CharacterController.Instance.scoreText.text = CharacterController.Instance.puan.ToString() + " x ";
           
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
