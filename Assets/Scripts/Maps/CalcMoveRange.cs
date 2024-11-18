using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcMoveRange : MonoBehaviour
{
    // 移動コストのマップデータ
    int[,] _originalMapList = new int[MapGenerator.WIDTH, MapGenerator.HEIGHT];
    // 移動計算結果のデータ格納用
    int[,] _resultMoveRangeList = new int[MapGenerator.WIDTH, MapGenerator.HEIGHT];

    // マップ上のx,z位置
    int _x;
    int _z;
    // 移動力
    int _m;

    // マップの大きさ
    int _xLength = MapGenerator.WIDTH;
    int _zLength = MapGenerator.HEIGHT;

    public void SetMovecost(TileObj[,] tileObjs)
    {
        // 移動コストのマップデータを(_originalMapList)作成 (本来はここでは作成せず外部から渡す)
        for (int i = 0; i < _zLength; i++)
        {
            for (int j = 0; j < _xLength; j++)
            {
                _originalMapList[i,j] = tileObjs[i,j].Cost;
            }
        }
    }

    /// <summary>
    /// 探索開始
    /// 計算結果のマップデータを返す
    /// </summary>
    public List<List<int>> StartSearch(int currentX, int currentZ, int movePower)
    {
        // _originalMapListのコピー作成
        _resultMoveRangeList = new List<List<int>>(_originalMapList);

        _xLength = _resultMoveRangeList[0].Count;
        _zLength = _resultMoveRangeList.Count;

        _x = currentX;
        _z = currentZ;
        _m = movePower;

        // 現在位置に現在の移動力を代入
        _resultMoveRangeList[_z][_x] = _m;
        Search4(_x, _z, _m);

        return _resultMoveRangeList;
    }

    /// <summary>
    /// 移動可能な範囲の4方向を調べる
    /// </summary>
    void Search4(int x, int z, int m)
    {

        if (0 < x && x < _xLength && 0 < z && z < _zLength)
        {
            // 上方向
            Search(x, z - 1, m);
            // 下方向
            Search(x, z + 1, m);
            // 左方向
            Search(x - 1, z, m);
            // 右方向
            Search(x + 1, z, m);
        }
    }

    /// <summary>
    /// 移動先のセルの調査
    /// </summary>
    void Search(int x, int z, int m)
    {
        // 探索方向のCellがマップエリア領域内かチェック
        if (x < 0 || _xLength <= x) return;
        if (z < 0 || _zLength <= z) return;

        // すでに計算済みのCellかチェック
        if ((m - 1) <= _resultMoveRangeList[z][x]) return;

        m = m + _originalMapList[z][x];

        if (m > 0)
        {
            // 進んだ位置に現在の移動力を代入
            _resultMoveRangeList[z][x] = m;
            // 移動量があるのでSearch4を再帰呼びだし
            Search4(x, z, m);
        }
        else
        {
            m = 0;
        }
    }

}
