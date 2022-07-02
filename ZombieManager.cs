using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    //レイヒットオブジェクト
    private GameObject _ReyHitObject;

    //座標
    private Vector3 _position;
    //正面角度
    private Vector3 _forword_angle;

    //ステータス
    public int _st;
    //サーチ有無
    private bool _search_st;

    //_st=1-基本形
    //_st=2-移動

    void Awake()
    {
        _position = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            RaySet();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RaySet()
    {
        _search_st = false;
        _ReyHitObject = null;

        _position = transform.position;
        _position.y += 1.5f;
        _forword_angle = transform.forward;

        Ray ray = new Ray(_position,_forword_angle);
        RaycastHit hit = new RaycastHit();

        //レイ確認用
        Debug.DrawRay(ray.origin,ray.direction*10,Color.red,3);

        if (Physics.Raycast(ray,out hit,10))
        {
            _ReyHitObject = hit.collider.gameObject;
            if (_ReyHitObject.name=="Player")
            {
                Debug.Log("OK!");
                _search_st = true;
            }
        }
    }
}
