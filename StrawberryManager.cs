using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrawberryManager : MonoBehaviour
{
    //コライダー
    private BoxCollider _collider;
    //アニメーター
    private Animator _animator;

    //スコアオブジェクト
    private GameObject _Score;
    //スコアテキスト
    private Text _score_text;

    //ステータス
    public int _st;

    //_st=1-基本形
    //_st=2-ゲット

    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();

        _Score = GameObject.Find("Score");
        _score_text = _Score.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _animator.Play("Base");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DelSet()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name=="Panda" && _st==1)
        {
            _st = 2;
            _collider.enabled = false;
            _animator.Play("Get");
            GameManager._score += 100;
            _score_text.text = GameManager._score.ToString("0000");
        }
    }
}
