using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyManager01 : MonoBehaviour
{
    //角度
    private Vector3 _angle;
    //カメラ座標
    private Vector3 _camera_position;

    //ステータス
    private int _st;
    //移動スピード
    public float _speed;
    //移動量X
    private float _vx;
    //移動量Z
    private float _vz;

    //_st=1-基本形
    //_st=2-移動

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _vx = 0;
        _vz = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _vx = 0;
        _vz = 0;

        //4方向のキー操作受付
        if (Input.GetKey("right"))
        {
            _vx = _speed;
        }
        else if (Input.GetKey("left"))
        {
            _vx = -_speed;
        }
        if (Input.GetKey("up"))
        {
            _vz = _speed;

        }
        else if (Input.GetKey("down"))
        {
            _vz = -_speed;
        }
    }

    void FixedUpdate()
    {
        //移動量がある場合
        if (_vx != 0 || _vz != 0)
        {
            if (_st == 1)
            {
                _st = 2;
            }
            //移動
            transform.Translate(_vx / 50, 0, _vz / 50, Space.World);

            //ビルボード用
            _camera_position = Camera.main.transform.position;
            _camera_position.y = transform.position.y;
            transform.LookAt(_camera_position);
            _angle = transform.localEulerAngles;
            _angle.y += 180;
            transform.localEulerAngles = _angle;
        }
        else
        {
            if (_st == 2)
            {
                _st = 1;
            }
        }
    }
}
