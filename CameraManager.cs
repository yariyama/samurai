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
    //左右移動タイプ★
    public int _tp2;
    //ターゲットx角度
    private float _t_x_angle;
    //ターゲットy角度★
    private float _t_y_angle;
    //ターゲットx座標★
    private float _t_x_pos;
    //ターゲットy座標
    private float _t_y_pos;
    //ターゲットz座標★
    private float _t_z_pos;

    //_st=1-基本形
    //_st=2-上下移動
    //_st=3-左右移動★

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

        //★
        _tp2 = 1;
        transform.position = new Vector3(0, 5, -10);
        transform.position = _position;
        transform.localEulerAngles = new Vector3(20, 0, 0);
        transform.localEulerAngles = _angle;
    }

    // Update is called once per frame
    void Update()
    {
        //上下移動
        if (Input.GetKeyDown("z") && _st == 1)
        {
            RoteSet();
            _st = 2;
        }
        //左右移動
        if (Input.GetKeyDown("x") && _st == 1)
        {
            SlideSet();
            _st = 3;
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
        //★
        else if (_st == 3)
        {
            if (_tp2 == 1)
            {
                if (_position.x < _t_x_pos)
                {
                    _position.x += 0.2f;
                    if (_position.x >= _t_x_pos)
                    {
                        _position.x = _t_x_pos;
                    }
                }
                if (_position.z < _t_z_pos)
                {
                    _position.z += 0.2f;
                    if (_position.z >= _t_z_pos)
                    {
                        _position.z = _t_z_pos;
                    }
                }
                if (_angle.y > _t_y_angle)
                {
                    _angle.y -= 1.6f;
                    if (_angle.y <= _t_y_angle)
                    {
                        _angle.y = _t_y_angle;
                    }
                }

                if (_position.x >= _t_x_pos && _position.z >= _t_z_pos && _angle.y <= _t_y_angle)
                {
                    _tp2 = 2;
                    _st = 1;
                }


            }
            else if (_tp2 == 2)
            {
                if (_position.x > _t_x_pos)
                {
                    _position.x -= 0.2f;
                    if (_position.x <= _t_x_pos)
                    {
                        _position.x = _t_x_pos;
                    }
                }
                if (_position.z < _t_z_pos)
                {
                    _position.z += 0.2f;
                    if (_position.z >= _t_z_pos)
                    {
                        _position.z = _t_z_pos;
                    }
                }
                if (_angle.y > _t_y_angle)
                {
                    _angle.y -= 1.6f;
                    if (_angle.y <= _t_y_angle)
                    {
                        _angle.y = _t_y_angle;
                    }
                }

                if (_position.x <= _t_x_pos && _position.z >= _t_z_pos && _angle.y <= _t_y_angle)
                {
                    _tp2 = 3;
                    _st = 1;
                }
            }
            else if (_tp2 == 3)
            {
                if (_position.x > _t_x_pos)
                {
                    _position.x -= 0.2f;
                    if (_position.x <= _t_x_pos)
                    {
                        _position.x = _t_x_pos;
                    }
                }
                if (_position.z > _t_z_pos)
                {
                    _position.z -= 0.2f;
                    if (_position.z <= _t_z_pos)
                    {
                        _position.z = _t_z_pos;
                    }
                }
                if (_angle.y > _t_y_angle)
                {
                    _angle.y -= 1.6f;
                    if (_angle.y <= _t_y_angle)
                    {
                        _angle.y = _t_y_angle;
                    }
                }

                if (_position.x <= _t_x_pos && _position.z <= _t_z_pos && _angle.y <= _t_y_angle)
                {
                    _tp2 = 4;
                    _st = 1;
                }
            }
            else if (_tp2 == 4)
            {
                if (_position.x < _t_x_pos)
                {
                    _position.x += 0.2f;
                    if (_position.x >= _t_x_pos)
                    {
                        _position.x = _t_x_pos;
                    }
                }
                if (_position.z > _t_z_pos)
                {
                    _position.z -= 0.2f;
                    if (_position.z <= _t_z_pos)
                    {
                        _position.z = _t_z_pos;
                    }
                }
                if (_angle.y > _t_y_angle)
                {
                    _angle.y -= 1.6f;
                    if (_angle.y <= _t_y_angle)
                    {
                        _angle.y = _t_y_angle;
                    }
                }

                if (_position.x >= _t_x_pos && _position.z >= _t_z_pos && _angle.y <= _t_y_angle)
                {
                    _angle.y = 0;
                    _tp2 = 1;
                    _st = 1;
                }
            }

            transform.position = _position;
            transform.localEulerAngles = _angle;

            _BoyManager.BillSet();
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

    //左右移動セット★
    void SlideSet()
    {
        if (_tp2 == 1)
        {
            _t_x_pos = _Boy.transform.position.x + 10;
            _t_z_pos = _Boy.transform.position.z;
            _t_y_angle = -90;
        }
        else if (_tp2 == 2)
        {
            _t_x_pos = _Boy.transform.position.x;
            _t_z_pos = _Boy.transform.position.z + 10;
            _t_y_angle = -180;
        }
        else if (_tp2 == 3)
        {
            _t_x_pos = _Boy.transform.position.x - 10;
            _t_z_pos = _Boy.transform.position.z;
            _t_y_angle = -270;
        }
        else if (_tp2 == 4)
        {
            _t_x_pos = _Boy.transform.position.x;
            _t_z_pos = _Boy.transform.position.z - 10;
            _t_y_angle = -360;
        }
        _BoyManager.StopSet();
    }
}
