using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseAttack : MonoBehaviour
{
    public float pullStrength;
    [SerializeField] float damage;
    [SerializeField] float coolDown;
    float timer = 0;
    public EnemyWalk enemyWalk;
    public PlayerDetector playerDetector;
    public bool attack;
    public bool doCoolDown = false;
    public bool canSeePlayer = false;
    public float pullLength;
    private void Awake()
    {
        enemyWalk = GetComponent<EnemyWalk>();
        playerDetector = GetComponentInChildren<PlayerDetector>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            Pull();
        }
        else
        {
            if (playerDetector.detected && !doCoolDown)
            {
                //Calculate direction to pull player in
                Vector3 dir = (playerDetector.player.position - this.transform.position);
                //Check to see if we can see the player
                RaycastHit2D[] cols = Physics2D.RaycastAll(this.transform.position, dir, Vector2.Distance(this.transform.position, playerDetector.player.position));
                for (int i = 0; i < cols.Length; i++)
                {
                    //If we hit ground then we cant see the player properly so just return for now.
                    if (cols[i].collider.CompareTag("Ground"))
                    {
                        canSeePlayer = false;
                        doCoolDown = true;
                        return;
                    }
                }
                canSeePlayer = true;
                attack = true;

            }
            else
            {
                attack = false;
                Cooldown();
            }
        }
        
    }

    void Pull()
    {
        timer += Time.deltaTime;
        if (timer >= pullLength)
        {
            timer = 0;
            attack = false;
            doCoolDown = true;
        }
    }
    void Cooldown()
    {
        if (doCoolDown)
        {
            timer += Time.deltaTime;
            if (timer >= coolDown)
            {
                timer = 0;
                doCoolDown = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Damageable>().Damage(damage);
            attack = false;
            doCoolDown = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position);

    }
}
