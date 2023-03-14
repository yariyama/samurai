using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeachTestManager : MonoBehaviour
{
    //リジッドボディ
    private Rigidbody2D _rbody;
    //表示
    private SpriteRenderer _renderer;
    //マウス座標
    private Vector3 _pos;
    //方向
    private Vector3 _dire;

    //移動スピード
    public float _speed;
    //移動量x
    private float _vx;
    //移動量y
    private float _vy;


    void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Camera.main.transform.forward);
        }
    }

    void FixedUpdate()
    {
        _dire = (_pos - this.transform.position).normalized;

        _vx = _dire.x * _speed;
        _vy = _dire.y * _speed;
        _rbody.velocity = new Vector2(_vx,_vy);

        _renderer.flipX = (_vx<0);
    }
}
