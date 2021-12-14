using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{

    //this will hold ALL stats for the player
    //damage, health, gold, movement speed
    public static StatisticsManager instance;
    public Healthbar healthbar;
    private int m_goldAmount = 0;
    public float m_damageAmount { get; set; }
    public float m_healthAmount { get; set; }
    public float m_maxHealthAmount { get; set; }
    public int m_movementSpeedAmount { get; set; }
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        m_maxHealthAmount = GameObject.FindObjectOfType<PlayerHealth>().GetCurrentHealth();
        m_healthAmount = m_maxHealthAmount;
    }
    public void AddGoldAmount(int num) {
        m_goldAmount += num;
        if(m_goldAmount < 0) {
            m_goldAmount = 0;
        }
    }

    public int GetGoldAmount() {
        return m_goldAmount;
    }


}
