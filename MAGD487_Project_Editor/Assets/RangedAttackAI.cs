using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackAI : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Vector2 launchPoint;
    [SerializeField] float coolDownTime;
    [SerializeField] bool requireLineOfSight;
    bool coolDown = false;
    float timer = 0;
    PlayerDetector playerDetector;

    private void Awake()
    {
        playerDetector = GetComponentInChildren<PlayerDetector>();
    }

    private void Update()
    {
        if (coolDown)
        {
            timer += Time.deltaTime;
            if(timer >= coolDownTime)
            {
                timer = 0;
                coolDown = false;
            }
        }
    }
    public void LaunchProjectile()
    {
        if (!coolDown)
        {
            if (requireLineOfSight && CanSeePlayer())
            {
                Instantiate(projectile, launchPoint, Quaternion.identity);
            }
            else
            { //Fire anyway
                Instantiate(projectile, launchPoint, Quaternion.identity);
            }
            coolDown = true;
        }
            
    }

    bool CanSeePlayer()
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
                return false;
            }
        }
        return true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(launchPoint, 0.1f);
    }
}
