using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    public AudioClip throwProjectile;
    // Start is called before the first frame update
    void Awake()
    {
        // Unity doesn't call Start function when object is created.. so our rigidbody2D didn't have any rigidbody when we used Start()
        // Hence we are using Awake()
        // Contrary to Start, Awake is called immediately when the object is created
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);    
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        // if(other.gameObject.tag != "Player")
        // {
        //     Debug.Log("Projectile collided with " + other.gameObject.name);
        //     Destroy(gameObject);
        // }
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if(e != null)
        {
            e.Fix();
        }  
        Destroy(gameObject);          
    }
}
