using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField] new string name;
    [SerializeField] int hp;
    [SerializeField] int maxHp;
    [SerializeField] int at;
    [SerializeField] int df;
    [SerializeField] bool isEnemy;
    [SerializeField] Vector2Int positionInt;
    [SerializeField] int moveRange;

    public Vector2Int Position { get => positionInt; }
    public bool IsEnemy { get => isEnemy; }
    public string Name { get => name; }
    public int Hp { get => hp; }
    public int At { get => at; }
    public int Df { get => df; }
    public int MaxHp { get => maxHp; }
    public int MoveRange { get => moveRange; }

    void Start()
    {
        transform.position = (Vector2)positionInt;   
    }

    // キャラを移動
    public void Move(Vector2Int pos)
    {
        positionInt = pos;
        // transform.position = (Vector2)positionInt;
        transform.DOMove((Vector2)positionInt, 0.3f).SetEase(Ease.Linear);
    }

    public int Damage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            hp = 0;
        }
        return value;
    }

    public int Attack(Character target)
    {
        return target.Damage(at);
    }
}
