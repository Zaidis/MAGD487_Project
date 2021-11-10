using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class menu_slot : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI t_title, t_desc, t_stats;
    [SerializeField] private Image i_statsImage, i_icon;
    public menu_slot leftLink, rightLink;
    public void EmptyThisSlot() {

    }

    public void FillSlot(Item item) {
        t_title.text = item.name;
        t_desc.text = item.description;
        i_icon.sprite = item.icon;
    }
}
