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

    // キャラを移動
    public void Move(Vector2Int pos)
    {
        positionInt = pos;
        transform.position = (Vector2)positionInt;
    }
}

// キャラクターの選択：前回
// コードの整理：前回
// TODO:キャラクターの移動
// ・選択したキャラを保持
// ・キャラ以外の場所をクリックすると移動