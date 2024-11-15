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

    const int WiDTH = 15;
    const int HEIGHT = 9;
    const int WATER_RATE = 10;
    const int FOREST_RATE = 30;
    
    // Map生成
    public List<TileObj> Generate()
    {
        List<TileObj> tileObjs = new List<TileObj>();

        Vector2 offset = new Vector2(-WiDTH / 2, -HEIGHT / 2);
        for (int x = 0; x < WiDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                Vector2 pos = new Vector2(x, y) + offset;
                int rate = Random.Range(0, 100); // 0-99迄の数字がランダムで1つ出る
                TileObj tileObj = null;
                if (rate < WATER_RATE)
                {
                    tileObj = Instantiate(waterPrefab, pos, Quaternion.identity, tileParent);
                }
                else if (rate < FOREST_RATE)
                {
                    tileObj = Instantiate(forestPrefab, pos, Quaternion.identity, tileParent);
                }
                else
                {
                    tileObj = Instantiate(grassPrefab, pos, Quaternion.identity, tileParent);
                }
                tileObj.positionInt = new Vector2Int((int) pos.x, (int) pos.y);
                tileObjs.Add(tileObj);
            }
        }
        return tileObjs;
    }
}
