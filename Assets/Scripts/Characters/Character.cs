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

// キャラクターの選択：前回
// TODO：コードの整理
// キャラの移動