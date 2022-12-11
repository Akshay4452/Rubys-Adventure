using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public int damage = 1;
    public ParticleSystem damageEffect;

    void OnTriggerStay2D(Collider2D other) {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if (controller.health > 0)
            {
                controller.changeHealth(-damage);
                Debug.Log("Ruby's health: " + controller.health);
            }
            else
            {
                // Destroy Ruby
                Debug.Log("Ruby Controller script not found :(");
            }
        }
    }

    public int damaged {get {return damage; }}
}
