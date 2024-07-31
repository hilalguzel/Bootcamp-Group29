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
    public bool oyunBittimi;

    public GameObject atak;
    public Transform atakKonum; // Transform olarak de�i�tirdik

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
        if (!oyunBittimi)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float hCoor = h * hareketHizi;
            float vCoor = v * hareketHizi;

            rb.velocity = new Vector2(hCoor, vCoor);

            // Karakterin y�n�n� belirle
            if (h != 0)
            {
                // Karakter sa�a ya da sola hareket ediyorsa do�ru y�ne bakacak �ekilde d�nd�r
                transform.localScale = new Vector3(Mathf.Sign(h) * 0.4f, 0.4f, 1);
            }
        }
    }

    void FaceMouse()
    {
        if (!oyunBittimi)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0; // Z eksenini s�f�rla

            // Karakter ile mouse aras�ndaki y�n
            Vector2 direction = (mousePosition - transform.position).normalized;

            // Karakterin bak�� y�n�n� ayarla
            atakKonum.up = direction;

            // Yukar� ve a�a�� hareket ederken z rotasyonunu s�f�rla
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
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
                GameObject buyu = Instantiate(atak, atakKonum.position, Quaternion.identity);

                // Merminin y�n�n� ayarla
                AtesController fireball = buyu.GetComponent<AtesController>();
                if (fireball != null)
                {
                    fireball.SetDirection(atakKonum.right); // At�� y�n�n� ayarla
                    fireball.FindClosestEnemy(); // En yak�n d��man� bul ve hedefle
                }
                else
                {
                    Debug.LogError("Fireball component could not be found on the instantiated object.");
                }

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
                Destroy(gameObject);
            }
        }
    }
}



