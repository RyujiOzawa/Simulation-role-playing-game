using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // フェーズの管理
    enum Phase
    {
        PlayerCharacterSelection,           //  キャラ選択
        PlayerCharacterMoveSelection,       //  キャラ移動
        PlayerCharacterCommandSelection,    //  コマンド対象
        PlayerCharacterTargetSelection,     //  攻撃対象選択
        EnemyCharacterSelection,
        EnemyCharacterMoveSelection,
    }

    // 選択したキャラの保持
    Character selectedCharacter;
    // 選択キャラの移動可能範囲の保持
    List<TileObj> movableTiles = new List<TileObj>();


    [SerializeField] Phase phase;
    [SerializeField] CharactersManager charactersManager;
    [SerializeField] MapManager mapManager;
    [SerializeField] ActionCommnadUI actionCommnadUI;

    private void Start()
    {
        phase = Phase.PlayerCharacterSelection;
        actionCommnadUI.Show(false);
    }

    //PlayerCharacterSelection, // キャラ選択
    // PlayerCharacterMoveSelection, // キャラ移動

    private void Update()
    {
        // Playerがクリックしたら処理したい
        if (Input.GetMouseButtonDown(0))
        {
            PlayerClickAction();
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

    bool IsClickCharacter(TileObj clickTileObj)
    {
        Character character = charactersManager.GetCharacter(clickTileObj.positionInt);
        if (character)
        {
            // 選択キャラの保持
            selectedCharacter = character;
            mapManager.ResetMovablepanels(movableTiles);
            // 移動範囲を表示
            mapManager.ShowMovablePanels(selectedCharacter, movableTiles);
            return true;
        }
        return false;
    }

    // PlayerCharacterSelectionフェーズでクリックした時にやりたいこと
    void PlayerCharacterSelection()
    {
        // クリックしたタイルを取得して
        TileObj clickTileObj = mapManager.GetClickTileObj();

        // 其の上にキャラが居るなら
        if (IsClickCharacter(clickTileObj))
        {
            phase = Phase.PlayerCharacterMoveSelection;
        }
    }

    // PlayerCharacterMoveSelectionフェーズでクリックした時にやりたいこと
    void PlayerCharacterMoveSelection()
    {
        // クリックした場所が移動範囲なら移動させて、敵のフェーズへ
        TileObj clickTileObj = mapManager.GetClickTileObj();

        // キャラクターが居るなら
        if (IsClickCharacter(clickTileObj))
        {
            return;
        }

        if (selectedCharacter)
        {
            // クリックしたタイルtileObjが移動範囲に含まれるなら
            if (movableTiles.Contains(clickTileObj))
            {
                // selectedCharacterをtileObj迄移動させる
                selectedCharacter.Move(clickTileObj.positionInt);
                phase = Phase.PlayerCharacterCommandSelection;
                // コマンドの表示
                actionCommnadUI.Show(true);
            }
            mapManager.ResetMovablepanels(movableTiles);
            selectedCharacter = null;
        }
    }

    public void OnAttackButton()
    {
        Debug.Log("攻撃選択");
    }

    public void OnWaiteButton()
    {
        Debug.Log("待機選択");
        OnPlayerTurnEnd();
    }

    void OnPlayerTurnEnd()
    {
        Debug.Log("相手ターン");
        phase = Phase.EnemyCharacterSelection;
        actionCommnadUI.Show(false);
    }
}
