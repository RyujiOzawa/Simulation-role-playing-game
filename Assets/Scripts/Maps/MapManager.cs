using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Cursor cursor;
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] CalcMoveRange calcMoveRange;
    // 生成したマップを管理する
    TileObj[,] tileObjs;

    private void Start()
    {
       tileObjs = mapGenerator.Generate();
       calcMoveRange.SetMovecost(tileObjs);
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

    TileObj GetTileOn(Character character)
    {
        for (int i = 0; i < tileObjs.GetLength(0); i++)
        {
            for (int j = 0; j < tileObjs.GetLength(1); j++)
            {
                if (tileObjs[i, j].positionInt == character.Position)
                {
                    return tileObjs[i, j];
                }
            }
        }
        return null;
    }
    // 移動範囲を表示する
    public void ShowMovablePanels(Character character, List<TileObj> movableTiles)
    {
        // エラーの原因:PlayerのPositionを入れているから、マイナスを入れてしまう
        // 何番目のタイルなのか（index）が入る


        // characterが乗っているタイルのIndexを取得する
        Vector2Int index = GetTileOn(character).Index;

        int[,] result = calcMoveRange.StartSearch(index.x, index.y, character.MoveRange);

        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0;j < result.GetLength(1); j++)
            {
                // 0以上なら移動範囲として追加する
                if ((result[i, j] >= 0))
                {
                    movableTiles.Add(tileObjs[i, j]);
                }
            }
        }

        // characterから上下左右のタイルを探す
        //movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position));
        //movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.up));
        //movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.down));
        //movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.left));
        //movableTiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.right));

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
        //tiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position));
        //tiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.up));
        //tiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.down));
        //tiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.left));
        //tiles.Add(tileObjs.Find(tile => tile.positionInt == character.Position + Vector2Int.right));

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


    public List<TileObj> GetRoot(Character character, TileObj goalTile)
    {
        return calcMoveRange.GetRoot(GetTileOn(character).Index, goalTile.Index, tileObjs);
    }
}
