using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Vector2Int positionInt;
    
    void Start()
    {
        transform.position = (Vector2)positionInt;   
    }
}
