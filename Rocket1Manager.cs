using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket1Manager : MonoBehaviour
{
    //ミサイル1オブジェクト
    private GameObject _Missile1;
    //ミサイル1スクリプト
    private Missile1Manager _Missile1Manager;

    //回転スピード
    public float _speed;


    void Awake()
    {
        _Missile1 = transform.Find("Missile1").gameObject;
        _Missile1Manager = _Missile1.GetComponent<Missile1Manager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(0,0,_speed/50);
    }

    void OnMouseDown()
    {
        _Missile1Manager.MoveSet();
    }
}
