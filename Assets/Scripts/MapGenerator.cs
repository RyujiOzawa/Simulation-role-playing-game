using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// マップの生成
// タイルを生成して、配置する
// ・prefab生成
// ・大きさを決める
public class MapGenerator : MonoBehaviour
{
    [SerializeField] TileObj tileObjPrefab;
    [SerializeField] Transform tileParent;

    const int WiDTH = 15;
    const int HEIGHT = 9;

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
                Vector2 pos = new Vector2(x, y);
                Instantiate(tileObjPrefab, pos, Quaternion.identity, tileParent);
            }
        }
    }
}
