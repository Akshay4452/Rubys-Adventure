using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyDamage = 2;
    public float enemySpeed = 3.0f;
    public bool vertical;
    new Rigidbody2D rigidbody2D;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    // Start is called before the first frame update
    Animator animator;
    bool isBroken = true;
    // Particle Effects
    public ParticleSystem smokeEffect;
    
    public AudioClip enemyHit;
    void Start()
    {
        timer = changeTime; // define the timer
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(!isBroken)
        {
            // When enemy robot is fixed by hitting bullet, it dances ("Fixed" Animation plays)
            // and code will exit if it is fixed
            return;
        }
        Vector2 position = rigidbody2D.position;
        if(vertical)
        {
            position.y += enemySpeed * Time.deltaTime * direction;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x += enemySpeed * Time.deltaTime * direction;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
        rigidbody2D.MovePosition(position);   
    }

    void Update()
    {
        if(!isBroken)
        {
            return;
        }
        timer -= Time.deltaTime; // reduce the time
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        // Here we are using other.gameObject.GetComponent<> instead of other.GameComponent<> because Collision2D doesn't have GetComponent<> method
        if(player != null)
        {
            player.changeHealth(-enemyDamage);
        }
    }
    public void Fix()
    {
        isBroken = false; // it gets healed when bullet is hit to it
        rigidbody2D.simulated = false;
        // smokeEffect.Stop();
        animator.SetTrigger("Fixed");
    }
}
