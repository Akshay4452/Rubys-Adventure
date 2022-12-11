using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    float horizontal;
    float vertical;
    public int maxHealth = 5;
    int currentHealth; // Private variable
    public float timeInvincible = 2.0f;
    // Property Definition
    public int health {get {return currentHealth; }}
    // access=public; return type=int; name=health
    // we can directly use it as an variable
    bool isInvincible;
    float invincibleTimer;
    
    public new Rigidbody2D rigidbody2D;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0); // default direction when Ruby goes Idle
    public GameObject projectilePrefab;
    AudioSource audioSource;
    public AudioClip playerHit;
    
    // Start is called before the first frame update
    void Start()
    {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 60;
        rigidbody2D = GetComponent<Rigidbody2D>(); // stores rigidbody2D component into variable
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        // Taking input from the user
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
                isInvincible = false;
        }
        if(Input.GetButtonDown("Fire1"))
        {
            Launch();
        }

        // Code for Raycast
        if(Input.GetKeyDown(KeyCode.X))
        {
           RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC")); 
           if(hit.collider != null)
           {
                // Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
                NonPlayerCharacter jambi = hit.collider.GetComponent<NonPlayerCharacter>();
                if(jambi != null)
                {
                    jambi.DisplayDialog();
                }
           }
        }
    }

    void FixedUpdate() {
        // Moving rigidbody in this case instead of Player to avoid jittering of the Player
        Vector2 position = rigidbody2D.position;
        position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
        position.y = position.y + moveSpeed * vertical * Time.deltaTime;

        rigidbody2D.MovePosition(position);
    }

    public void changeHealth(int amount) 
    {
        if (amount < 0) // when we are dealing damage
        {
            if(isInvincible) 
            {
                return; // dont do anything if ruby is invincible
            }
            else 
            {
                isInvincible = true;
                invincibleTimer = timeInvincible; // resetting 
                animator.SetTrigger("Hit");
                PlaySound(playerHit);
            }
        }
        // Health Gain code
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); 
        UIHealthBar.instance.SetValue(currentHealth / (float) maxHealth);   
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up*0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        // Play sound after projectile launch
        AudioClip launchSound = projectile.throwProjectile;
        PlaySound(launchSound);

        animator.SetTrigger("Launch");
    }
}


