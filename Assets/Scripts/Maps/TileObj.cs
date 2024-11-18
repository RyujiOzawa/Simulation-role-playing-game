using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObj : MonoBehaviour
{
    public Vector2Int positionInt;
    [SerializeField ]int cost;// 移動コスト

    [SerializeField] GameObject movablePanel;

    public int Cost { get => cost; }

    public void ShowMovablePanel(bool isActive)
    {
        movablePanel.SetActive(isActive);
    }

    public void SetCost(int cost)
    {
        this.cost = cost;
    }
}

// TODO:地形に依存した移動範囲の表示
// ・地形の移動コストを其々のタイルに設定する
// ・移動コストに基づいて、移動範囲を計算する