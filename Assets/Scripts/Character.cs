using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Vector2Int positionInt;

    public Vector2Int Position { get => positionInt; }

    void Start()
    {
        transform.position = (Vector2)positionInt;   
    }
}

// 選択タイルの取得
// キャラの選択
// ・選択タイルの座標とキャラの座標を比較する
//   ・全てのキャラを管理するクラスを作る
// キャラの移動