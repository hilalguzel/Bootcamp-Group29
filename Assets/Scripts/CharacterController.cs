using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float hareketHizi = 5f;

    private Rigidbody2D rb;
    private Animator anim;

    private bool atakYaptim;
    private bool oyunBittimi;

    public GameObject atak;
    public GameObject atakKonum;

    public int puan;

    public float currentHealth = 10f;
    public float maxHealth = 10f;

    public float attackInterval = 1.5f; // Ate� etme aral���
    private float attackTimer = 0f; // Zamanlay�c�

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        oyunBittimi = false;
        puan = 0;
        anim = GetComponent<Animator>();
        atakYaptim = false;
        currentHealth = 10f;
        maxHealth = 10f;
    }

    void Update()
    {
        HareketEttir();
        FaceMouse();
        AttackYap();
    }

    void HareketEttir()
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
        mousePosition.z = 0; // Z eksenini s�f�rla

        // Karakter ile mouse aras�ndaki y�n
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Karakterin d�nmesi
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 1); // Sa�
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-0.4f, 0.4f, 1); // Sol
        }

        // Karakterin bak�� y�n�n� ayarla
        transform.up = direction;

        // `atakKonum`'un mouse'a bakmas�n� sa�la
        atakKonum.transform.up = direction;
    }

    void AttackYap()
    {
        if (!oyunBittimi)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackInterval)
            {
                attackTimer = 0f;
                atakYaptim = true;

                // Ate� topunu instantiate et
                GameObject buyu = Instantiate(atak, atakKonum.transform.position, Quaternion.identity);

                // Merminin y�n�n� ayarla
                Vector2 fireDirection = (atakKonum.transform.position - transform.position).normalized;
                buyu.transform.right = fireDirection; // Merminin y�n�n� ayarla
                buyu.GetComponent<Rigidbody2D>().velocity = fireDirection * 10f; // H�z� belirle

                atakYaptim = false;
            }
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Dusman"))
        {
            currentHealth -= 1;
            if (currentHealth <= 0)
            {
                oyunBittimi = true;
                // Oyun biti�i ile ilgili i�lemler burada yap�labilir
            }
        }
    }
}

