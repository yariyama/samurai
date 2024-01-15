using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : MonoBehaviour
{
    //角度
    private Vector3 _angle;
    //アニメーター
    private Animator _animator;
    //リジッドボディ
    private Rigidbody _rbody;

    //モンキーメッシュオブジェクト
    private GameObject _MonkeyMesh;
    //モンキーメッシュコライダー
    private CapsuleCollider _monkey_mesh_collider;

    //モンキーメッシュレンダラー
    private Renderer _monkey_mesh_renderer;
    //モンキーメッシュマテリアル
    private Material _monkey_mesh_material;
    //モンキーメッシュカラー
    private Color _monkey_mesh_color;

    //スピード
    public float _speed;
    //ステータス
    private int _st;
    //タイマー
    private float _timer;
    //ランダム値
    private int _ran;
    //方向
    private int _dire;
    //回転スピード
    public float _a_speed;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ターン
    //_st=4-ダメージ

    //_dire=1-右
    //_dire=2-左

    void Awake()
    {
        _angle = this.transform.localEulerAngles;
        _animator = this.GetComponent<Animator>();
        _rbody = GetComponent<Rigidbody>();

        _MonkeyMesh = transform.Find("MonkeyMesh").gameObject;
        _monkey_mesh_collider = _MonkeyMesh.GetComponent<CapsuleCollider>();
        _monkey_mesh_renderer = _MonkeyMesh.GetComponent<Renderer>();
        _monkey_mesh_material = _monkey_mesh_renderer.material;
        _monkey_mesh_color = _monkey_mesh_material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _angle.y = 270;
        transform.localEulerAngles = _angle;
        _dire = 1;
        _animator.Play("Base");
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            _timer += Time.deltaTime;
            if (_timer>=1)
            {
                _ran = Random.Range(0,10);
                if (_ran==1)
                {
                    _timer = 0;
                    _st = 2;
                    _animator.Play("Walk");
                }
            }
        }
        else if (_st==2)
        {
            transform.Translate(0,0,_speed/50);
        }
        else if (_st==3)
        {
            if (_dire==1)
            {
                transform.Rotate(0,-_a_speed/50,0);
                _angle = transform.localEulerAngles;
                if (_angle.y<=90)
                {
                    _angle.y = 90;
                    transform.localEulerAngles = _angle;
                    _st = 1;
                    _dire = 2;
                    _animator.Play("Base");
                }
            }
            else if (_dire==2)
            {
                transform.Rotate(0, _a_speed / 50, 0);
                _angle = transform.localEulerAngles;
                if (_angle.y >= 270)
                {
                    _angle.y = 270;
                    transform.localEulerAngles = _angle;
                    _st = 1;
                    _dire = 1;
                    _animator.Play("Base");
                }
            }
        }
        else if (_st==4)
        {
            _monkey_mesh_color.a -= 0.01f;
            if (_monkey_mesh_color.a<=0)
            {
                Destroy(this.gameObject);
            }
            _monkey_mesh_material.color = _monkey_mesh_color;
        }
    }

    //ターン切り換え
    public void TurnSet()
    {
        _st = 3;
    }

    //ダメージ切り換え
    public void DameSet()
    {
        _st = 4;
        _timer = 0;
        _animator.Play("Dame");
        _monkey_mesh_collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
    }
}
