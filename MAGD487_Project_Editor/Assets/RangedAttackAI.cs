using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangedAttackAI : MonoBehaviour
{
    public UnityEvent projectileFire;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform launchPoint;
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

        if (playerDetector.detected)
        {
            LaunchProjectile();
        }
    }
    public void LaunchProjectile()
    {
        if (!coolDown)
        {
            if (requireLineOfSight && CanSeePlayer())
            {
                projectileFire.Invoke();
            }
            else if(!requireLineOfSight)
            { //Fire anyway
                projectileFire.Invoke();
            }
            coolDown = true;
        }
            
    }

    public void SpawnProjectile()
    {
        if (this.transform.localScale.x < 0)
        {
            GameObject proj = Instantiate(projectile, launchPoint.position, Quaternion.identity);
            proj.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
            Instantiate(projectile, launchPoint.position, Quaternion.identity);
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
}
