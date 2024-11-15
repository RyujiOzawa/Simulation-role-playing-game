using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // フェーズの管理
        enum Phase
    {
        PlayerCharacterSelection,  //  キャラ選択
        PlayerCharacterMoveSelection,  //  キャラ移動
        EnemyCharacterSelection,
        EnemyCharacterMoveSelection,
    }

    [SerializeField] Phase phase;

    private void Start()
    {
        phase = Phase.PlayerCharacterSelection;
    }

    //PlayerCharacterSelection, // キャラ選択
    // Playerがクリックしたら処理したい
    // Playerがクリックしたら処理したい
    private void Update()
    {
        // Playerがクリックしたら処理したい
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    void PlayerClickAction()
    {
        switch (phase)
        {
            case Phase.PlayerCharacterSelection:
                PlayerCharacterSelection();
                break;
            case Phase.PlayerCharacterMoveSelection:
                PlayerCharacterMoveSelection();
                break;
        }
    }

    // PlayerCharacterSelectionフェーズでクリックした時にやりたいこと
    void PlayerCharacterSelection()
    {
        // キャラを選択したら、移動範囲を表示して、移動フェーズへ
        phase = Phase.PlayerCharacterMoveSelection;
    }
    // PlayerCharacterMoveSelectionフェーズでクリックした時にやりたいこと
    void PlayerCharacterMoveSelection()
    {
        // クリックした場所が移動範囲なら移動させて、敵のフェーズへ
        phase = Phase.EnemyCharacterSelection;
    }

}
