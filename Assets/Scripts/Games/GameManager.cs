using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

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
        EnemyCharacterTargetSelection,     //  攻撃対象選択
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
    [SerializeField] PhasePanelUI phasePanelUI;
    [SerializeField] GameObject turnEndButton;

    private void Start()
    {
        damageUI.OnEndAnim += OnAttacked;
        phase = Phase.PlayerCharacterSelection;
        actionCommnadUI.Show(false);
        StartCoroutine(phasePanelUI.PanelAnim("PLAYER TURN"));
        turnEndButton.SetActive(true);
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //UIをクリックした場合
            return;
        }
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
            // キャラのステータスを表示
            statusUI.Show(selectedCharacter);
            // もし自分のキャラが動いていないなら、移動範囲表示
            if (character.IsMoved == false　&& character.IsEnemy == false)
            {
                mapManager.ResetMovablepanels(movableTiles);
                // 移動範囲を表示
                mapManager.ShowMovablePanels(selectedCharacter, movableTiles);
                return true;
            }
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
                //TODO: 経路を取得して、移動する
                selectedCharacter.Move(clickTileObj.positionInt,mapManager.GetRoot(selectedCharacter, clickTileObj), null);
                phase = Phase.PlayerCharacterCommandSelection;
                // コマンドの表示
                actionCommnadUI.Show(true);
            }
            mapManager.ResetMovablepanels(movableTiles);
        }
    }

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
        // OnPlayerTurnEnd();
        actionCommnadUI.Show(false);
        selectedCharacter = null;
        mapManager.ResetAttackablePanels(attackableTiles);
        phase = Phase.PlayerCharacterSelection;
    }

    // 攻撃が終わったよ
    void OnAttacked()
    {
        if (phase == Phase.PlayerCharacterTargetSelection)
        {
            actionCommnadUI.Show(false);
            selectedCharacter = null;
            mapManager.ResetAttackablePanels(attackableTiles);
            phase = Phase.PlayerCharacterSelection;
        }

        if (phase == Phase.EnemyCharacterTargetSelection)
        {
            actionCommnadUI.Show(false);
            selectedCharacter = null;
            mapManager.ResetAttackablePanels(attackableTiles);
            OnEnemyTurnEnd();
        }
    }

    void OnPlayerTurnEnd()
    {
        Debug.Log("相手ターン");
        phase = Phase.EnemyCharacterSelection;
        actionCommnadUI.Show(false);
        selectedCharacter = null;
        mapManager.ResetAttackablePanels(attackableTiles);
        StartCoroutine(phasePanelUI.PanelAnim("ENEMY TURN", EnemyCharacterSelection));  //  フェーズアニメを実行
        // EnemyCharacterSelection(); // フェーズアニメが終わってから実行したい
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
        // 手順
        // ・ターゲットとなるPlayerを見付ける　=> 一番近いPlayer
        Character target = charactersManager.GetClosestCharacter(selectedCharacter);
        // ・移動範囲の中で、Playerに近い場所を探す
        TileObj targetTile = movableTiles
            .OrderBy(tile => Vector2.Distance(target.Position, tile.positionInt)) // 小さい順に並べ替える
            .FirstOrDefault(); // 最初のタイルを渡す

        selectedCharacter.Move(targetTile.positionInt, mapManager.GetRoot(selectedCharacter, targetTile), EnemyrCharacterTargetSelection);
        mapManager.ResetMovablepanels(movableTiles);
    }

    // 敵の攻撃
    void EnemyrCharacterTargetSelection()
    {
        phase = Phase.EnemyCharacterTargetSelection;
        // 攻撃範囲の取得
        mapManager.ResetAttackablePanels(attackableTiles);
        mapManager.ShowAttackablePanels(selectedCharacter, attackableTiles);
        // 範囲内にPlayerのキャラが居れば取得
        Character targetChara = null;
        foreach (var tile in attackableTiles)
        {
            Character character = charactersManager.GetCharacter(tile.positionInt);
            if (character && character.IsEnemy == false)
            {
                targetChara = character;
            }
        }
        // ターゲットが居るなら攻撃を実行する
        if (targetChara)
        {
            // 攻撃処理
            int damage = selectedCharacter.Attack(targetChara);
            mapManager.ResetAttackablePanels(attackableTiles);
            actionCommnadUI.Show(false);
            damageUI.Show(targetChara, damage);
            OnEnemyTurnEnd();
        }
        else
        {
            OnEnemyTurnEnd();
        }
    }

    void OnEnemyTurnEnd()
    {
        selectedCharacter = null;
        phase = Phase.PlayerCharacterSelection;
        StartCoroutine(phasePanelUI.PanelAnim("PLAYER TURN"));
        mapManager.ResetAttackablePanels(attackableTiles);
        turnEndButton.SetActive(true);
        foreach (var chara in charactersManager.characters)
        {
            if (chara.IsEnemy == false)
            {
                chara.OnBeginTurn();
            }
        }
    }

    public void OnTurnEndButton()
    {
        OnPlayerTurnEnd();
        turnEndButton.SetActive(false);
    }
}
// ・Playerに近付ける

// ・Enemyが攻撃した場合にターンの切り替えがされない 
//  理由：処理書いてないから
// ・移動と同時に攻撃をしてしまう => UnityActionを活用する

// ・エリアの端だとエラーが出る TODO
//  ・範囲外だから
