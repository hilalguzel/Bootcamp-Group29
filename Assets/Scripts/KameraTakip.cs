using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform karakter; // Takip edilecek karakter
    public float takipHizi = 2.0f; // Kameranýn karakteri takip etme hýzý
    public Vector2 kameraLimitleri; // Kameranýn hareket etme sýnýrlarý

    private void LateUpdate()
    {
        if (karakter != null)
        {
            Vector3 hedefPozisyon = new Vector3(karakter.position.x, karakter.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, hedefPozisyon, takipHizi * Time.deltaTime);

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -kameraLimitleri.x, kameraLimitleri.x),
                Mathf.Clamp(transform.position.y, -kameraLimitleri.y, kameraLimitleri.y),
                transform.position.z
            );
        }
    }
}
