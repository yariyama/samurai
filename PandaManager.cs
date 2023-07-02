using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //パンダメッシュオブジェクト
    private GameObject _PandaMesh;
    //パンダメッシュレンダラー
    private Renderer _panda_mesh_render;
    //パンダメッシュマテリアル
    private Material _panda_mesh_material;
    //パンダメッシュカラー
    private Color _panda_mesh_color;

    //モンキーオブジェクト
    private GameObject _Monkey;
    //モンキースクリプト
    private MonkeyManager _MonkeyManager;

    //メインカメラオブジェクト
    private GameObject _MainCamera;

    //フェイスオブジェクト
    private GameObject[] _Face = new GameObject[2];
    //ポイントオブジェクト
    private GameObject[] _Point = new GameObject[3];

    //ゲームオーバーオブジェクト
    private GameObject _GameOver;
    //ゲームオーバースクリプト
    private GameOverManager _GameOverManager;

    //リジッドボディ
    private Rigidbody _rbody;
    //アニメーター
    private Animator _animator;
    //コライダー
    private CapsuleCollider _collider;

    //ステータス
    public int _st;
    //カウント
    private int _count;
    //タイマー
    private float _timer;
    //移動量z
    private float _vz;
    //移動スピード
    public float _w_speed;
    //回転量
    private float _angle;
    //回転スピード
    public float _a_speed;
    //ジャンプ力
    public float _jump_p;
    //プッシュ有無
    private bool _push_st;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプセット
    //_st=4-ジャンプ
    //_st=5-ジャンプ着地
    //_st=6-攻撃
    //_st=7-攻撃後
    //_st=8-ダメージ
    //_st=9-クリア

    void Awake()
    {
        _rbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();

        _PandaMesh = transform.Find("PandaMesh").gameObject;
        _panda_mesh_render = _PandaMesh.GetComponent<Renderer>();
        _panda_mesh_material = _panda_mesh_render.material;
        _panda_mesh_color = _panda_mesh_material.color;

        _MainCamera = transform.Find("Main Camera").gameObject;

        for (int i=0; i<2;i++)
        {
            _Face[i] = GameObject.Find("Face"+(i+1));
        }

        for (int i=0;i<3; i++)
        {
            _Point[i] = GameObject.Find("Point"+(i+1));
        }

        _GameOver = GameObject.Find("GameOver");
        _GameOverManager = _GameOver.GetComponent<GameOverManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _count = 0;
        _timer = 0;
        _animator.Play("Base");

        _GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _vz = 0;
        _angle = 0;
        if (Input.GetKey("up"))
        {
            _vz = _w_speed;
        }
        else if (Input.GetKey("down"))
        {
            _vz = -_w_speed;
        }
        if (Input.GetKey("right"))
        {
            _angle = _a_speed;
        }
        else if (Input.GetKey("left"))
        {
            _angle = -_a_speed;
        }

        if (Input.GetKey("space"))
        {
            if (!_push_st)
            {
                _push_st = true;
                _st = 3;
                _timer = 0;
                _animator.Play("JumpSet");
            }
        }
        else
        {
            _push_st = false;
        }
    }

    void FixedUpdate()
    {
        if (_st == 1)
        {
            if (_vz != 0 || _angle != 0)
            {
                _st = 2;
                _animator.Play("Walk");
            }
        }
        else if (_st == 2)
        {
            if (_vz == 0 && _angle == 0)
            {
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st == 3)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.2f)
            {
                _timer = 0;
                _st = 4;
                _count = 0;
                _rbody.AddForce(new Vector3(0, _jump_p, 0), ForceMode.Impulse);
                _animator.Play("JumpUp");
            }
        }
        else if (_st == 4)
        {
            _timer += Time.deltaTime;
            if (_count == 0 && _timer >= 0.3f)
            {
                _timer = 0;
                _count = 1;
                _animator.Play("JumpTop");
            }
            else if (_count == 1 && _timer >= 0.2f)
            {
                _timer = 0;
                _count = 2;
                _animator.Play("JumpDown");
            }
        }
        else if (_st == 5)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.3f)
            {
                _timer = 0;
                _st = 1;
                _animator.Play("Base");
            }
        }
        else if (_st == 6)
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.5f)
            {
                _timer = 0;
                _st = 7;
                _animator.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
        else if (_st==8)
        {
            _panda_mesh_color.a -= 0.01f;
            if (_panda_mesh_color.a<=0)
            {
                _panda_mesh_color.a = 0;
                _MainCamera.transform.parent = null;

                if (GameManager._zan_c>0)
                {
                    --GameManager._zan_c;
                    _Face[GameManager._zan_c].SetActive(false);
                    FallSet();
                }
                else
                {
                    _GameOver.SetActive(true);
                    _GameOverManager.ActiveSet();
                    this.gameObject.SetActive(false);
                }
            }
            _panda_mesh_material.color = _panda_mesh_color;
        }

        if (_st==2 || _st==4) {
            transform.Translate(0, 0, _vz / 50);
            transform.Rotate(0, _angle / 50, 0);
        }

        if (_MainCamera.transform.parent && transform.position.y<=-6)
        {
            _MainCamera.transform.parent = null;
        }
        else if (!_MainCamera.transform.parent && transform.position.y <= -10)
        {
            if (GameManager._zan_c > 0)
            {
                --GameManager._zan_c;
                _Face[GameManager._zan_c].SetActive(false);
                FallSet();
            }
            else
            {
                _GameOver.SetActive(true);
                _GameOverManager.ActiveSet();
                this.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((_st==4||_st==7) && other.gameObject.tag=="Cloud")
        {
            _st = 5;
            _timer = 0;
            _animator.Play("JumpArrive");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Monkey" && _st==4 && _count==2)
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();
            if (_MonkeyManager._st<=3)
            {
                _st = 6;
                _timer =0;
                _animator.Play("JumpAttack");
                _rbody.constraints = RigidbodyConstraints.FreezePosition;

                _MonkeyManager.DameSet();
            }
        }
    }

    public void DameSet()
    {
        _st = 8;
        _animator.Play("StrengthDame");
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
        _collider.enabled = false;
    }

    public void FallSet()
    {
        _MainCamera.transform.parent = this.gameObject.transform;
        _MainCamera.transform.localPosition = new Vector3(0,2.5f,-2.5f);
        _MainCamera.transform.localEulerAngles = new Vector3(25,0,0);
        _rbody.constraints = RigidbodyConstraints.FreezeRotation;
        _collider.enabled = true;
        _panda_mesh_color.a = 1;
        _panda_mesh_material.color = _panda_mesh_color;

        for (int i=2; i>=0; i--)
        {
            if (transform.position.z>=_Point[i].transform.position.z || i==0)
            {
                transform.position = _Point[i].transform.position;
                break;
            }
        }

        _st = 7;
        _animator.Play("JumpDown");
    }

    public void ClearSet()
    {
        _st = 9;
        _animator.Play("Base");
    }
}
