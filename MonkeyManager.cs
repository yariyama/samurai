﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{

    //アニメーター
    private Animator _animator;
    //角度
    private Vector3 _angle;
    //リジッドボディ
    private Rigidbody _rbody;
    //コライダー
    private CapsuleCollider _collider;

    //モンキーメッシュオブジェクト■
    private GameObject _MonkeyMesh;
    //モンキーメッシュレンダラー■
    private Renderer _monkey_mesh_renderer;
    //モンキーメッシュマテリアル■
    private Material _monkey_mesh_material;
    //モンキーメッシュカラー■
    private Color _monkey_mesh_color;

    //ステータス
    public int _st;
    //移動スピード
    public float _w_speed;
    //回転スピード
    public float _a_speed;
    //方向
    private int _dire;
    //タイマー
    private float _timer;
    //ランダム値
    private int _ran;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-振り向き
    //_st=4-ダメージ

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        //■
        _MonkeyMesh = transform.Find("MonkeyMesh").gameObject;
        _monkey_mesh_renderer = _MonkeyMesh.GetComponent<Renderer>();
        _monkey_mesh_material = _monkey_mesh_renderer.material;
        _monkey_mesh_color = _monkey_mesh_material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _dire = 1;
        _timer = 0;
        _angle.y = 270;
        transform.localEulerAngles = _angle;
        _animator.Play("Base");
    }

    void FixedUpdate()
    {
        if (_st == 1)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1)
            {
                _ran = Random.Range(0, 5);
                if (_ran == 1)
                {
                    _timer = 0;
                    _st = 2;
                    _animator.Play("Walk");
                }
            }
        }
        else if (_st == 2)
        {
            transform.Translate(0, 0, _w_speed / 50);
        }
        else if (_st == 3)
        {
            transform.Rotate(0, -_a_speed / 50, 0);
            _angle = transform.localEulerAngles;
            if (_angle.y >= 360)
            {
                _angle.y -= 360;
            }
            if (_dire == 1 && _angle.y <= 90)
            {
                _st = 1;
                _angle.y = 90;
                _dire = 2;
                _animator.Play("Base");
                transform.localEulerAngles = _angle;
            }
            else if (_dire == 2 && _angle.y <= 270 && _angle.y >= 260)
            {
                _st = 1;
                _angle.y = 270;
                _dire = 1;
                _animator.Play("Base");
                transform.localEulerAngles = _angle;
            }
        }
        //■
        else if (_st == 4)
        {
            _monkey_mesh_color.a -= 0.01f;
            if (_monkey_mesh_color.a <= 0)
            {
                _monkey_mesh_color.a = 0;
                Destroy(this.gameObject);
            }
            _monkey_mesh_material.color = _monkey_mesh_color;
        }
    }

    public void TurnSet()
    {
        _st = 3;
    }

    public void DameSet()
    {
        _st = 4;
        _timer = 0;
        _animator.Play("Dame");
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
        _collider.enabled = false;
    }
}
