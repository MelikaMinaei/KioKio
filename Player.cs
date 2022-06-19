using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float padding = 1f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float deathSoundVol = 0.75f;
    [SerializeField] [Range(0, 1)] float shootSoundVol = 0.25f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 30f;
    [SerializeField] float projectileFire = 0.05f;

    Coroutine firingCoroutine;

    float Xmin;
    float Xmax;
    float Ymin;
    float Ymax;

    void Start()
    {
        SetUpMoveBounds();
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVol);
    }

    public int GetHealth() {
        if(health <= 0)
        {
            return 0;
        }
        else { return health; }  
    }

    IEnumerator FireCont()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                    laserPrefab, transform.position,
                    Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVol);
            yield return new WaitForSeconds(projectileFire);
        }
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireCont());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, Xmin, Xmax);
        var newyPos = Mathf.Clamp(transform.position.y + deltaY, Ymin, Ymax);
        transform.position = new Vector2(newXPos, newyPos);
    }

    private void SetUpMoveBounds()
    {
        Camera gameCamera = Camera.main;
        Xmin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        Ymin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        Xmax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        Ymax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
