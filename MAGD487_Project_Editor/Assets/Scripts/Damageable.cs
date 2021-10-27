using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected float health;    
    protected float maxHealth;

    private void Awake()
    {
        maxHealth = health;
    }
    /// <summary>
    /// Damage the entity
    /// </summary>
    /// <param name="amt"></param>
    public virtual void Damage(float amt)
    {
        //health -= amt;
        //if(health <= 0)
        //{
        //    print(gameObject.name + "is Dead");
        //}
        if (health > 0)
        {
            health -= amt;
            if (health <= 0)
            {
                Death();
            }
        }
    }

    /// <summary>
    /// Heal the entity
    /// </summary>
    /// <param name="amt"></param>
    public virtual void Heal(float amt)
    {
        health += amt;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    /// <summary>
    /// Destroy this gameobject
    /// </summary>
    public virtual void Destroy()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Increase the max health of the entity.
    /// </summary>
    /// <param name="amt"></param>
    public void IncreaseMaxHealth(float amt)
    {
        maxHealth += amt;
    }

    public virtual void Death()
    {
        print(gameObject.name + " is Dead");
        Destroy();
    }

    public float GetCurrentHealth() { return health; }
    public float GetMaxHealth() { return maxHealth; }


}
