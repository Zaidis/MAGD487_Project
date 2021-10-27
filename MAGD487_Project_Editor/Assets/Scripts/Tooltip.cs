using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class Tooltip : MonoBehaviour {
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private RectTransform bgRect;
    [SerializeField] private TextMeshProUGUI textUI;
    private RectTransform rect;
    private void Awake() {
        rect = GetComponent<RectTransform>();
    }

    public void SetText(string text) {
        textUI.SetText(text);
        textUI.ForceMeshUpdate();
        Vector2 textSize = textUI.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);
        bgRect.sizeDelta = textSize + paddingSize;
    }

    private void Update() {
        Vector2 anchoredPosition = Mouse.current.position.ReadValue() / canvasTransform.localScale.x + new Vector2(6, 6);

        if(anchoredPosition.x + bgRect.rect.width > canvasTransform.rect.width) {
            anchoredPosition.x = canvasTransform.rect.width - bgRect.rect.width;
        }
        if(anchoredPosition.y + bgRect.rect.height > canvasTransform.rect.height) {
            anchoredPosition.y = canvasTransform.rect.height - bgRect.rect.height;
        } 

        Rect screenRect = new Rect(0, 0, Screen.currentResolution.width, Screen.currentResolution.height);
        if (anchoredPosition.x < screenRect.x) {
            anchoredPosition.x = screenRect.x;
        }
        if (anchoredPosition.y < screenRect.y) {
            anchoredPosition.y = screenRect.y;
        }
        rect.anchoredPosition = anchoredPosition;
    }

}
