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
    public Transform atakKonum;

    public int puan;
    public float currentHealth = 10f;
    public float maxHealth = 10f;

    public float attackInterval = 1.5f;
    private float attackTimer = 0f;

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

            if (h != 0)
            {
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
            mousePosition.z = 0;

            Vector2 direction = (mousePosition - transform.position).normalized;
            atakKonum.up = direction;

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
                GameObject buyu = Instantiate(atak, atakKonum.position, Quaternion.identity);
                AtesController fireball = buyu.GetComponent<AtesController>();
                if (fireball != null)
                {
                    fireball.SetDirection(atakKonum.right);
                    fireball.FindClosestEnemy();
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
