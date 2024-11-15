using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommnadUI : MonoBehaviour
{
    // ActionCommandのUIを管理する
    [SerializeField] GameObject attackbutton;
    [SerializeField] GameObject waiteButton;

    public void Show(bool isActive)
    {
        attackbutton.SetActive(isActive);
        waiteButton.SetActive(isActive);
    }

    public void ShowAttackButton(bool isActive)
    {
        attackbutton.SetActive(isActive);
    }
    public void ShowWaitButton(bool isActive)
    {
        waiteButton.SetActive(isActive);
    }
}
