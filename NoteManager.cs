using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    //ゲームメインオブジェクト
    private GameObject _GameMain;
    //ゲームメインスクリプト
    private GameMainManager _GameMainManager;

    //座標
    private Vector2 _position;

    //ステータス
    private int _st;
    //バージョン
    public int _ver;
    //ラインNO
    private int _line_no;

    //ノーマルスピード
    public float _n_speed;

    //_st=1-基本移動

    void Awake()
    {
        _GameMain = GameObject.Find("GameMain");
        _GameMainManager = _GameMain.GetComponent<GameMainManager>();

        _position = transform.position;
    }

    // Start is called before the first frame update
    void Start()
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

    //アクティブ化
    public void ActiveSet(int _no)
    {
        Debug.Log(_no);
        _line_no = _no;
        _st = 1;

        _position.x = 7.6f;
        _position.y = 3.5f-2*(_line_no-1);
        transform.position = _position;
    }
}
