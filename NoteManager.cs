using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームメインスクリプト
    private GameMainManager _GameMainManager;

    //ステータス
    private int _st;
    //バージョン
    public int _ver;

    //ノーマルスピード
    public float _n_speed;

    //_st=1-基本移動

    void Awake()
    {
        _GameMain = GameObject.Find("GameMain");
        _GameMainManager = _GameMain.GetComponent<GameMainManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //基本移動
        if (_st==1)
        {
            transform.Translate(-_n_speed/50,0,0);

            if (transform.position.x<=-7.7f)
            {
                _GameMainManager._note_st[_ver] = false;
                Destroy(this.gameObject);
            }
        }
    }
}
