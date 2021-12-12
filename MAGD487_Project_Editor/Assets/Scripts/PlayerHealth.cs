using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damageable
{

    public override void Damage(float amt) {

        base.Damage(amt);
        StatisticsManager.instance.healthbar.UpdateHealthbar();
    }

    public override void Heal(float amt) {

        base.Heal(amt);
        StatisticsManager.instance.healthbar.UpdateHealthbar();
    }

}
