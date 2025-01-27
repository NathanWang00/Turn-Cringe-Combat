using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    protected float health, fallSpeed, maxFallSpeed;

    // for when you want the object to be hit but never destroyed
    [SerializeField]
    protected bool indestructable = false;

    // To toggle on and off within the script
    protected bool damageable = true;

    protected Rigidbody2D rb2D;

    protected virtual void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        // linear fallspeed equation, someone can make it quadratic if they want
        rb2D.AddForce(new Vector2(0, Mathf.Clamp(-rb2D.velocity.y - maxFallSpeed, -fallSpeed, fallSpeed)), ForceMode2D.Impulse);
    }

    // No knockback, since this emcompasses static objects
    public virtual void Hurt(float damage, Vector2 hitForce)
    {
        if (damageable && !indestructable)
        {
            health -= damage;
        }
    }

    protected virtual void Die()
    {
        Debug.Log(this.gameObject.name + " is dead");
    }

    public float GetHealth() {
        return health;
    }
}
