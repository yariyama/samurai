using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //ステータス
    private int _st;
    //移動量z
    private float _vz;
    //移動スピード
    public float _w_speed;
    //回転量
    private float _angle;
    //回転スピード
    public float _a_speed;


    //_st=1-基本形
    //_st=2-移動


    // Start is called before the first frame update
    void Start()
    {
        _st = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
