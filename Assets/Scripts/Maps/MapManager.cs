using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Cursor cursor;
    [SerializeField] CharactersManager charactersManager;
    [SerializeField] MapGenerator mapGenerator;

    Character selectedCharacter;
    List<TileObj> tileObjs = new List<TileObj>();

    List<TileObj> movableTiles = new List<TileObj>();
    private void Start()
    {
       tileObjs = mapGenerator.Generate();
    }

    // クリックしたオブジェクトを取得したい
    // クリック判定　=> Update関数の中でInputを使う
    // クリックしたオブジェクトを取得したい　=> クリックした場所にRayを飛ばしてオブジェクトを取得する
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(
                clickPosition,
                Vector2.down
                );
            // Rayを飛ばしてヒットしたタイルを取得する
            if (hit2D && hit2D.collider)
            {
                cursor.SetPosition(hit2D.transform);
                TileObj tileObj = hit2D.collider.GetComponent<TileObj>();
                // ヒットしたタイル上のキャラを取得する
                Character character = charactersManager.GetCharacter(tileObj.positionInt);
                if (character)
                {
                    Debug.Log("居る");
                    // 選択キャラの保持
                    selectedCharacter = character;
                    ResetMovablepanels();
                    // 移動範囲を表示
                    ShowMovablePanels(selectedCharacter);
                }
                else
                {
                    Debug.Log("クリックした場所にキャラが居ない");
                    // キャラを保持しているなら、クリックしたタイルの場所に移動させる
                    if (selectedCharacter)
                    {
                        // クリックしたタイルtileObjが移動範囲に含まれるなら
                        if (movableTiles.Contains(tileObj))
                        {
                            // selectedCharacterをtileObj迄移動させる
                            selectedCharacter.Move(tileObj.positionInt);
                        }
                        ResetMovablepanels();
                        selectedCharacter = null;
                    }
                }
            }
        }
    }

    // TODO 移動範囲を表示する
    void ShowMovablePanels(Character character)
    {
        // characterから上下左右のタイルを探す

        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.up));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.down));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.left));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.right));

        foreach (var tile in movableTiles)
        {
            tile.ShowMovablePanel(true);
        }
    }

    void ResetMovablepanels()
    {
        foreach (var tile in movableTiles)
        {
            tile.ShowMovablePanel(false);
        }
        movableTiles.Clear();
    }
}
