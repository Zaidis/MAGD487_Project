using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageDealer : MonoBehaviour
{
    SpriteRenderer sr;
    float damage = 5;
    [SerializeField] Vector2 attackCenterPoint;
    [SerializeField] float radius;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void DamageZone()
    {
        float v = 1;
        if (sr.flipX)
        {
            v = -1;
        }
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector3(attackCenterPoint.x * v, attackCenterPoint.y) + this.transform.position, radius);
        print("Hits: " + hits.Length);
        for (int i = 0; i < hits.Length; i++)
        {
            Damageable dam = hits[i].GetComponentInParent<Damageable>();
            print(dam);
            if (dam != null && !hits[i].CompareTag("Player")){
                    print("Damaging: " + damage + "\n" + hits[i].name);
                    dam.Damage(damage);
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float v = 1;
        sr = GetComponent<SpriteRenderer>();
        if (sr.flipX)
        {
            v = -1;
        }
        Gizmos.DrawWireSphere(new Vector3(attackCenterPoint.x * v, attackCenterPoint.y) + this.transform.position, radius);
    }
}
