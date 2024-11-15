using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    // キャラ全てを管理する
    public List<Character> characters = new List<Character>();

    void Start()
    {
        // データ型が一致する子要素を全て取得する
        GetComponentsInChildren(characters);
    }

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
}
