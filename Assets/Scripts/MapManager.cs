using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] Cursor cursor;

    // クリックしたオブジェクトを取得したい
    // クリック判定　=> Update関数の中でInputを使う
    // クリックしたオブジェクトを取得したい　=> クリックした場所にRayを飛ばしてオブジェクトを取得する

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(
                clickPosition,
                Vector2.down
                );
            if (hit2D && hit2D.collider)
            {
                cursor.SetPosition(hit2D.transform);
                TileObj tileObj = hit2D.collider.GetComponent<TileObj>();
                Debug.Log(tileObj.positionInt);
            }
        }
    }

}
