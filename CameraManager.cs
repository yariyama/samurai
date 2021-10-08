using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //ボーイオブジェクト
    private GameObject _Boy;
    //ボーイスクリプト
    private BoyManager _BoyManager;

    //座標
    private Vector3 _position;
    //x角度
    private Vector3 _angle;

    //ステータス
    public int _st;
    //上下移動タイプ
    public int _tp1;
    //ターゲットx角度
    private float _t_x_angle;
    //ターゲットy座標
    private float _t_y_pos;

    //_st=1-基本形
    //_st=2-上下移動

    void Awake()
    {
        _Boy = GameObject.Find("Boy");
        _BoyManager = _Boy.GetComponent<BoyManager>();

        _position = transform.position;
        _angle = transform.localEulerAngles;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _tp1 = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z") && _st == 1)
        {
            RoteSet();
            _st = 2;
        }
    }

    void FixedUpdate()
    {
        if (_st == 2)
        {
            if (_tp1 == 1)
            {
                if (_position.y > _t_y_pos)
                {
                    _position.y -= 0.2f;
                    if (_position.y <= _t_y_pos)
                    {
                        _position.y = _t_y_pos;
                    }
                }
                if (_angle.x > _t_x_angle)
                {
                    _angle.x -= 1.6f;
                    if (_angle.x <= _t_x_angle)
                    {
                        _angle.x = _t_x_angle;
                    }
                }

                if (_position.y <= _t_y_pos && _angle.x <= _t_x_angle)
                {
                    _tp1 = 2;
                    _st = 1;
                }

                transform.position = _position;
                transform.localEulerAngles = _angle;

                _BoyManager.BillSet();
            }
            else if (_tp1 == 2)
            {
                if (_position.y < _t_y_pos)
                {
                    _position.y += 0.2f;
                    if (_position.y >= _t_y_pos)
                    {
                        _position.y = _t_y_pos;
                    }
                }
                if (_angle.x < _t_x_angle)
                {
                    _angle.x += 1.6f;
                    if (_angle.x >= _t_x_angle)
                    {
                        _angle.x = _t_x_angle;
                    }
                }

                if (_position.y >= _t_y_pos && _angle.x >= _t_x_angle)
                {
                    _tp1 = 1;
                    _st = 1;
                }

                transform.position = _position;
                transform.localEulerAngles = _angle;

                _BoyManager.BillSet();
            }
        }
    }

    //上下移動セット
    void RoteSet()
    {
        if (_tp1 == 1)
        {
            _t_y_pos = 0.1f;
            _t_x_angle = -20;
        }
        else
        {
            _t_y_pos = 5f;
            _t_x_angle = 20;
        }
    }
}
