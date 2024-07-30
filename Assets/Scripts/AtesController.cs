using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtesController : MonoBehaviour
{
    public float speed = 10f; // Merminin h�z�n� belirler
    public float lifetime = 2f; // Merminin �mr� (ka� saniye sonra yok olacak)

    private void Start()
    {
        // Merminin �mr�n� ba�lat
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Mermiyi ileriye do�ru hareket ettir
        // `transform.up` kullanarak mermiyi hareket ettiriyoruz
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Mermi bir nesneye �arpt���nda yok olmas�n� sa�lar
    //    if (collision.gameObject.CompareTag("Dusman"))
    //    {
    //        // D��man� etkileyin (�rne�in, d��man�n sa�l���na zarar verin)
    //        // Bu k�sm� ihtiyac�n�za g�re d�zenleyebilirsiniz

    //        Destroy(gameObject); // Mermiyi yok et
    //    }
    //}
}
