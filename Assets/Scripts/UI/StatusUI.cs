using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    // 役割:Statusの表示
    // 渡されたCharacterのデータの表示

    [SerializeField] Text nameText;
    [SerializeField] Text hpText;
    [SerializeField] Text atText;
    [SerializeField] Text dfText;
    // Statusの表示
    public void Show(Character character)
    {
        gameObject.SetActive(true);
        nameText.text = character.Name;
        hpText.text = $"{character.Hp}/{character.MaxHp}";
        atText.text = $"AT:{character.At}";
        dfText.text = $"DF:{character.Df}";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
