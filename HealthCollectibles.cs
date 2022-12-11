using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectibles : MonoBehaviour
{
    public int healthGain = 1;
    public ParticleSystem healthGainEffect;
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        //Debug.Log("Ruby has entered the trigger: " + other);  
        if (controller != null)
        { 
            if (controller.health < controller.maxHealth)
            { 
                controller.changeHealth(healthGain);
                ParticleSystem particleEffect = Instantiate(healthGainEffect);
                particleEffect.Play();
                Destroy(gameObject);
                Destroy(particleEffect);
                controller.PlaySound(collectedClip);
            }
            else
            {
                Debug.Log("Ruby's Health is already full!");
            }
        } 
    }
}
