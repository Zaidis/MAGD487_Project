using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damageable
{
    public override void Damage(float amt) {

        if(StatisticsManager.instance.m_healthAmount > 0) {
            StatisticsManager.instance.m_healthAmount -= amt;
        }

        if(StatisticsManager.instance.m_healthAmount <= 0) {
            //player is dead
            Death();
        }
        StatisticsManager.instance.healthbar.UpdateHealthbar();
    }

    public override void Heal(float amt) {

        StatisticsManager.instance.m_healthAmount += amt;
        if(StatisticsManager.instance.m_healthAmount > StatisticsManager.instance.m_maxHealthAmount) {
            StatisticsManager.instance.m_healthAmount = StatisticsManager.instance.m_maxHealthAmount;
        }
        StatisticsManager.instance.healthbar.UpdateHealthbar();
    }

}
