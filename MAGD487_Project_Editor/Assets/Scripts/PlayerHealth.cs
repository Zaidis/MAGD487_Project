using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : Damageable
{
    public override void Damage(float amt) {

        if(StatisticsManager.instance.m_healthAmount > 0) {
            StatisticsManager.instance.m_healthAmount -= amt;
        }

        if(StatisticsManager.instance.m_healthAmount <= 0) {
            //player is dead
            this.Death();
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

    public override void Death() {
        print(gameObject.name + " is Dead");
        SceneManager.LoadScene(5); //death scene
    }
}
