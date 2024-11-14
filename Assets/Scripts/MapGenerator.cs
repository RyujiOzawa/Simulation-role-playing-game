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
    
    private void Start()
    {
        Generate();
    }

    void Generate()
    {
        Vector2 offset = new Vector2(-WiDTH / 2, -HEIGHT / 2);
        for (int x = 0; x < WiDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                Vector2 pos = new Vector2(x, y) + offset;
                int rate = Random.Range(0, 100); // 0-99迄の数字がランダムで1つ出る
                if (rate < 10)
                {
                    Instantiate(waterPrefab, pos, Quaternion.identity, tileParent);
                }
                else if (rate < 30)
                {
                    Instantiate(forestPrefab, pos, Quaternion.identity, tileParent);
                }
                else
                {
                    Instantiate(grassPrefab, pos, Quaternion.identity, tileParent);
                }
            }
        }
    }
}
