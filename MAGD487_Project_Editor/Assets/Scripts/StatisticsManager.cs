using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour
{

    //this will hold ALL stats for the player
    //damage, health, gold, movement speed
    public static StatisticsManager instance;

    private int m_goldAmount = 0;
    public int m_damageAmount { get; set; }
    public int m_healthAmount { get; set; }
    public int m_movementSpeedAmount { get; set; }
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
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
