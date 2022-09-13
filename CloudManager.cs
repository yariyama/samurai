using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    //リジッドボディ
    private Rigidbody2D _rbody;
    //リジッドボディ座標
    //（テキストP241）
    private Vector3 _velocity; 

    //方向
    private int _dire_x;
    //タイマー
    private float _timer;
    //スピード
    public float _s_speed;

    void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _dire_x = 1;
    }

    void FixedUpdate()
    {
        if (_dire_x == 1)
        {
            _timer += Time.deltaTime;
            //リジッドボディに渡す移動量の座標
            _velocity = new Vector3(-_s_speed / 50, -_s_speed / 50, 0);
            if (_timer >= 2)
            {
                _dire_x = 2;
                _timer = 0;
            }
        }
        else if (_dire_x == 2)
        {
            _timer += Time.deltaTime;
            _velocity = new Vector3(_s_speed/50, -_s_speed / 50, 0);
            if (_timer >= 2)
            {
                _dire_x = 1;
                _timer = 0;
            }
        }
        //リジッドボディで移動するメソッド
        //引数で今の座標位置に移動量を足しています。
        _rbody.MovePosition(transform.position + _velocity);
    }
}
