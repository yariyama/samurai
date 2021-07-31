using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    //クリックオブジェクト
    private GameObject _ClickedGameObject;
    //回転スクリプト
    private RotationManager _RotationManager;

    //タッチ処理用
    private Vector2 touchPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //マウスの画面位置の取得
            touchPosition = Input.mousePosition;
            TouchAct(touchPosition);
        }
    }

    /// タッチされていたときの処理
    private void TouchAct(Vector2 pos)
    {
        //マウスの画面位置をゲーム中の位置に変換
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(pos);

        //クリックしたオブジェクトを全て取得する
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(worldPoint, Vector2.zero))
        {
            //オブジェクトが見つかったときの処理
            if (hit)
            {
                _ClickedGameObject = hit.transform.gameObject;
                if (_ClickedGameObject.tag == "Player")
                {
                    //サーチしたオブジェクトのスクリプトを取得
                    _RotationManager = _ClickedGameObject.GetComponent<RotationManager>();
                    _RotationManager.RoteSet();
                }
            }
        }
    }
}
