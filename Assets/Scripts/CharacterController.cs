using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float hareketHizi;

    Rigidbody2D rb;
    Animator anim;

    public GameObject atak;
    public GameObject atakKonum;  // karakter önüne koyulacak boþ obje

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        KarakterHareket();
        FaceMouse();
    }
    void KarakterHareket()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float hCoor = h * hareketHizi;
        float vCoor = v * hareketHizi;

        rb.velocity = new Vector2(hCoor, vCoor);
    }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        transform.right = direction;
    }

    void AttackYap()
    {
        //karakterin koþulsuz devamlý hasar vereceði kýsým
        GameObject buyu = Instantiate(atak) as GameObject;
        buyu.transform.position = atakKonum.transform.position;
        buyu.transform.rotation = atakKonum.transform.rotation;
    }
}
