using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] UnityEvent swing, block;
    [SerializeField] float swingStartDistance;
    [SerializeField] PlayerDetector swingZone;
    [SerializeField] PlayerDetector playerDetector;
    [SerializeField] float coolDownTime;
    [SerializeField] float damage;
    EnemyWalk enemyWalk;
    float timer = 0;
    bool coolDown = false;
    [HideInInspector] public bool blocking = false;
    public bool blockCapable;
    [HideInInspector] public bool swinging = false;

    private void Awake()
    {
        enemyWalk = GetComponent<EnemyWalk>();
    }
    private void Start()
    {
        if (blockCapable)
            blocking = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (coolDown)
            CoolDown();
        else
        {
            if (playerDetector.detected && !swinging && Vector2.Distance(this.transform.position, playerDetector.player.position) < swingStartDistance)
            {
                //Start swinging
                Swing();
            }
        }
    }

    void StopMoving()
    {
        enemyWalk.GetRigidbody2D().velocity = new Vector2(0, enemyWalk.GetRigidbody2D().velocity.y);
        enemyWalk.enabled = false;
    }
    public void StartMoving()
    {
        enemyWalk.enabled = true;
    }
    void CoolDown()
    {
        timer += Time.deltaTime;
        if(timer > coolDownTime)
        {
            timer = 0;
            coolDown = false;
        }
    }

    void Swing()
    {
        swing.Invoke();
        swinging = true;
        coolDown = true;
        blocking = false;
        StopMoving();
    }
    public void Damage()
    {
        if (swingZone.detected)
        {
            swingZone.player.GetComponent<Damageable>().Damage(damage);
        }
    }
    public void Block()
    {
        block.Invoke();
        coolDown = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, swingStartDistance);
    }
}
