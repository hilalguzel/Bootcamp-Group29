using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float hareketHizi;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        KarakterHareket();
    }
    void KarakterHareket()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float hCoor = h * hareketHizi;
        float vCoor = v * hareketHizi;

        rb.velocity = new Vector2(hCoor, vCoor);
    }
}
