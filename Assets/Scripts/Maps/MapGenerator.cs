using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マップの生成
// タイルを生成して、配置する
// ・prefab生成
// ・大きさを決める
public class MapGenerator : MonoBehaviour
{
    [SerializeField] TileObj grassPrefab;
    [SerializeField] TileObj forestPrefab;
    [SerializeField] TileObj waterPrefab;
    [SerializeField] Transform tileParent;

    // Prefabの多様化:バリアント
    // 割合に応じたPrefabの生成

    public const int WIDTH = 15;
    public const int HEIGHT = 9;
    const int WATER_RATE = 10;
    const int FOREST_RATE = 30;
    
    // Map生成
    public TileObj[,] Generate()
    {
        TileObj[,] tileObjs = new TileObj[WIDTH, HEIGHT];

        Vector2 offset = new Vector2(-WIDTH / 2, -HEIGHT / 2);
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                // 移動コスト
                // 平原:-1
                // 森:-2
                // 水:-99
                Vector2 pos = new Vector2(x, y) + offset;
                int rate = Random.Range(0, 100); // 0-99迄の数字がランダムで1つ出る
                TileObj tileObj = null;
                if (rate < WATER_RATE)
                {
                    tileObj = Instantiate(waterPrefab, pos, Quaternion.identity, tileParent);
                    tileObj.SetCost(-99);
                }
                else if (rate < FOREST_RATE)
                {
                    tileObj = Instantiate(forestPrefab, pos, Quaternion.identity, tileParent);
                    tileObj.SetCost(-2);
                }
                else
                {
                    tileObj = Instantiate(grassPrefab, pos, Quaternion.identity, tileParent);
                    tileObj.SetCost(-1);
                }
                tileObj.positionInt = new Vector2Int((int) pos.x, (int) pos.y);
                tileObjs[x,y] = tileObj;
                tileObj.SetIndex(x, y);
            }
        }
        return tileObjs;
    }
}
