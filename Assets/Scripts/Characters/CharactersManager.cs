using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharactersManager : MonoBehaviour
{
    // キャラ全てを管理する
    public List<Character> characters = new List<Character>();

    //void Start()
    //{
    //   // データ型が一致する子要素を全て取得する
    //   GetComponentsInChildren(characters);
    //}

    // 座標が一致するキャラを渡す
    public Character GetCharacter(Vector2Int pos)
    {
        foreach (var character in characters)
        {
            if (character.Position == pos)
            {
                return character;
            }
        }
        return null;
    }
    // 座標が一致するキャラを渡す
    public Character GetCharacter(Vector2 pos)
    {
        foreach (var character in characters)
        {
            if (character.Position == pos)
            {
                return character;
            }
        }
        return null;
    }

    // 敵キャラをランダムに1体取得する
    public Character GetRandomEnemy()
    {
        // 全ての敵を集める
        List<Character> enemies = characters.FindAll(chara => chara.IsEnemy);
        // ランダムに1つ渡す
        int r = Random.Range(0, enemies.Count);
        return enemies[r];
    }

    // 自分に最も近いキャラ(敵キャラ)探す
    // 敵キャラ：PlayerからするとEnemy, EnemyからするとPlayer

    public Character GetClosestCharacter(Character self)
    {
        return characters
            .Where(chara => chara.IsEnemy != self.IsEnemy) //  敵を探す
            .OrderBy(chara => Vector2.Distance(self.Position, chara.Position)) //  selfから距離が近い順に並べる
            .FirstOrDefault(); // 最も近いキャラを渡す
    }
}
