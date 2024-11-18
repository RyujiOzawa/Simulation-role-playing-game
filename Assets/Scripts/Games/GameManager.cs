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
    // 選択キャラの攻撃範囲の保持
    List<TileObj> attackableTiles = new List<TileObj>();

    [SerializeField] Phase phase;
    [SerializeField] CharactersManager charactersManager;
    [SerializeField] MapManager mapManager;
    [SerializeField] ActionCommnadUI actionCommnadUI;
    [SerializeField] StatusUI statusUI;
    [SerializeField] DamageUI damageUI;


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
            case Phase.PlayerCharacterTargetSelection:
                PlayerCharacterTargetSelection();
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
            statusUI.Show(selectedCharacter);
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
        }
    }

    // TODO：攻撃範囲内の敵をクリックしたら攻撃する
    // ・居ない場合は待機ボタンを押してターン終了
    void PlayerCharacterTargetSelection()
    {
        TileObj clickTileObj = mapManager.GetClickTileObj();

        // 攻撃の範囲内をクリックしたら
        if (attackableTiles.Contains(clickTileObj))
        {
            // 敵キャラクターが居るなら　
            Character targetChara = charactersManager.GetCharacter(clickTileObj.positionInt);
            if (targetChara && targetChara.IsEnemy)
            {
                // 攻撃処理
                int damage = selectedCharacter.Attack(targetChara);
                mapManager.ResetAttackablePanels(attackableTiles);
                actionCommnadUI.Show(false);
                damageUI.Show(targetChara, damage);
                OnPlayerTurnEnd();
            }
        }
    }
    public void OnAttackButton()
    {
        phase = Phase.PlayerCharacterTargetSelection;
        // 攻撃範囲を表示
        mapManager.ResetAttackablePanels(attackableTiles);
        mapManager.ShowAttackablePanels(selectedCharacter, attackableTiles);
        actionCommnadUI.ShowAttackButton(false);

    }
    public void OnWaiteButton()
    {
        OnPlayerTurnEnd();
    }
    void OnPlayerTurnEnd()
    {
        Debug.Log("相手ターン");
        phase = Phase.EnemyCharacterSelection;
        actionCommnadUI.Show(false);
        selectedCharacter = null;
        mapManager.ResetAttackablePanels(attackableTiles);
        EnemyCharacterSelection();
    }

    // キャラ選択
    void EnemyCharacterSelection()
    {
        Debug.Log("敵のキャラ選択");
        // charactersManagerからランダムに敵を持ってくる
        selectedCharacter = charactersManager.GetRandomEnemy();

        mapManager.ResetMovablepanels(movableTiles);
        // 移動範囲を表示
        mapManager.ShowMovablePanels(selectedCharacter, movableTiles);
        EnemyCharacterMoveSelection();
    }

    // 移動
    void EnemyCharacterMoveSelection()
    {
        Debug.Log("敵のキャラの移動");
        // ランダムに移動場所を決めて移動する
        int r = Random.Range(0,movableTiles.Count);
        selectedCharacter.Move(movableTiles[r].positionInt);
        mapManager.ResetMovablepanels(movableTiles);
        OnEnemyTurnEnd();
    }

    void OnEnemyTurnEnd()
    {
        //---
        Debug.Log("敵ターン終了");
        selectedCharacter = null;
        phase = Phase.PlayerCharacterSelection;
    }
}
