using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObj : MonoBehaviour
{
    public Vector2Int positionInt;

    Vector2Int index;  //  何番目のタイルなのか？

    [SerializeField ]int cost;// 移動コスト

    [SerializeField] GameObject movablePanel;

    public int Cost { get => cost; }
    public Vector2Int Index { get => index; }

    public void ShowMovablePanel(bool isActive)
    {
        movablePanel.SetActive(isActive);
    }

    public void SetCost(int cost)
    {
        this.cost = cost;
    }
    public void SetIndex(int x, int y)
    {
        this.index = new Vector2Int(x, y);
    }
}

// TODO:地形に依存した移動範囲の表示
// ・地形の移動コストを其々のタイルに設定する
// ・移動コストに基づいて、移動範囲を計算する