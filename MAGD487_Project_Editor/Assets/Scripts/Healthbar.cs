using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Healthbar : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public void UpdateHealthbar() {
        GetComponent<Image>().fillAmount = (StatisticsManager.instance.m_healthAmount / StatisticsManager.instance.m_maxHealthAmount);
        healthText.text = StatisticsManager.instance.m_healthAmount.ToString() + " / " + StatisticsManager.instance.m_maxHealthAmount.ToString();
    }

}
