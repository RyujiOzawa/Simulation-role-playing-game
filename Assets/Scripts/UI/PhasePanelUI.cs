using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhasePanelUI : MonoBehaviour
{

    private void Start()
    {
        // ・rotationを90にする
        transform.rotation = Quaternion.Euler(90, 0, 0);
        StartCoroutine(PanelAnim());
    }

    public IEnumerator PanelAnim()
    {
        // 1フレーム待機
        yield return new WaitForSeconds(0.5f);
        // ・(0, 0, 0)に近付ける（DOTween）WaitForCompletion:アニメーション終了迄待ってくれる
        yield return transform.DORotate(new Vector3(0, 0, 0), 0.7f).WaitForCompletion();
        yield return new WaitForSeconds(1f);
        // ・90に戻す（DOTween）
        yield return transform.DORotate(new Vector3(90, 0, 0), 0.7f).WaitForCompletion();

        // パネル表示が終わってやりたいこと
    }
}
