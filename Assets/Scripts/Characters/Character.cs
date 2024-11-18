using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int at;
    [SerializeField] bool isEnemy;
    [SerializeField] Vector2Int positionInt;

    public Vector2Int Position { get => positionInt; }
    public bool IsEnemy { get => isEnemy; }

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

    public void Damage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            hp = 0;
        }
    }

    public void Attack(Character target)
    {
        target.Damage(at);
    }
}
