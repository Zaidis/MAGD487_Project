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
        for (int i = 0; i < hits.Length; i++)
        {
            
            Damageable dam = hits[i].GetComponentInParent<Damageable>();
            if (dam != null && !dam.gameObject.CompareTag("Player")){
                Weapon item = (Weapon)InventoryManager.instance.m_slots[(int)InventoryManager.instance.GetCurrentItem()].m_item;
                    dam.Damage(item.damage);
                    break;
            }
        }
    }

    public void PlaySound() {
        
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
