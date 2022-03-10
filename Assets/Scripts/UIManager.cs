using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text notification;

    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void ShowNotification(string text, float duration = 1f)
    {
        notification.text = text;
        Invoke(nameof(ClearTesxt), duration);
    }

    private void ClearTesxt()
    {
        notification.text = string.Empty;
    }
}
