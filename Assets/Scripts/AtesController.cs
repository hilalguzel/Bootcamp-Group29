using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtesController : MonoBehaviour
{
    public float speed = 10f; // Merminin hýzýný belirler
    public float lifetime = 2f; // Merminin ömrü (kaç saniye sonra yok olacak)

    private void Start()
    {
        // Merminin ömrünü baþlat
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Mermiyi ileriye doðru hareket ettir
        // `transform.up` kullanarak mermiyi hareket ettiriyoruz
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Mermi bir nesneye çarptýðýnda yok olmasýný saðlar
    //    if (collision.gameObject.CompareTag("Dusman"))
    //    {
    //        // Düþmaný etkileyin (örneðin, düþmanýn saðlýðýna zarar verin)
    //        // Bu kýsmý ihtiyacýnýza göre düzenleyebilirsiniz

    //        Destroy(gameObject); // Mermiyi yok et
    //    }
    //}
}
