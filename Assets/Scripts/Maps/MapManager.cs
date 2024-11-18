using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Cursor cursor;
    [SerializeField] MapGenerator mapGenerator;

    // 生成したマップを管理する
    List<TileObj> tileObjs = new List<TileObj>();

    private void Start()
    {
       tileObjs = mapGenerator.Generate();
    }

    // クリックしたタイル取得する
    public TileObj GetClickTileObj()
    {
        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast(clickPosition,Vector2.down);
        // Rayを飛ばしてヒットしたタイルを取得する
        if (hit2D && hit2D.collider)
        {
           cursor.SetPosition(hit2D.transform);
           return hit2D.collider.GetComponent<TileObj>();
        }
        return null;
    }

    // 移動範囲を表示する
    public void ShowMovablePanels(Character character, List<TileObj> movableTiles)
    {
        // characterから上下左右のタイルを探す

        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.up));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.down));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.left));
        movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.right));

        // nullを除去
        movableTiles.RemoveAll(tile => tile == null);

        foreach (var tile in movableTiles)
        {
            tile.ShowMovablePanel(true);
        }
    }

    // 移動範囲表示をリセットする
    public void ResetMovablepanels(List<TileObj> movableTiles)
    {
        foreach (var tile in movableTiles)
        {
            tile.ShowMovablePanel(false);
        }
        movableTiles.Clear();
    }

    // 攻撃範囲を表示する
    public void ShowAttackablePanels(Character character, List<TileObj> tiles)
    {
        // characterから上下左右のタイルを探す
        tiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position));
        tiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.up));
        tiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.down));
        tiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.left));
        tiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.right));

        foreach (var tile in tiles)
        {
            // TODO:攻撃用に変更
            tile.ShowMovablePanel(true);
        }
    }

    // 移動範囲表示をリセットする
    public void ResetAttackablePanels(List<TileObj> tiles)
    {
        foreach (var tile in tiles)
        {
            tile.ShowMovablePanel(false);
        }
        tiles.Clear();
    }
}
