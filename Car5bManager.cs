using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car5bManager : MonoBehaviour
{
    //リッジドボディ
    private Rigidbody2D _rbody;

    //ステータス
    private int _st;
    //スピード
    public float _speed;

    //_st=1-基本形
    //_st=2-移動

    void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void FixedUpdate()
    {
        if (_st == 2)
        {
            //リッジドボディを利用して動かす
            _rbody.velocity = new Vector2(_speed,0);
        }
    }

    void OnMouseDown()
    {
        _st = 2;
    }
}
