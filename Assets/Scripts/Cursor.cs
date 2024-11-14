using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    // ・カーソルを移動させたい
    public void SetPosition(Transform target)
    {
        transform.position = target.position;
    }
}
