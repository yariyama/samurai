using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaManager : MonoBehaviour
{
    //アニメーター
    private Animator _animtor;
    //リジッドボディ
    private Rigidbody _rbody;

    //パンダメッシュオブジェクト
    private GameObject _PandaMesh;
    //パンダメッシュコライダー
    private CapsuleCollider _panda_mesh_collider;

    //パンダメッシュレンダラー
    private Renderer _panda_mesh_renderer;
    //パンダメッシュマテリアル
    private Material _panda_mesh_material;
    //パンダメッシュカラー
    private Color _panda_mesh_color;

    //角度
    private Vector3 _angle2;

    //メインカメラオブジェクト
    private GameObject _MainCamera;

    //フェイスオブジェクト
    private GameObject[] _Face = new GameObject[2];

    //ポイントオブジェクト
    private GameObject[] _Point = new GameObject[4];

    //ゲームオーバーオブジェクト
    private GameObject _GameOver;
    //ゲームオーバースクリプト
    private GameOverManager _GameOverManager;

    //スピード
    public float _speed;
    //移動量x
    //private float _vx;
    //移動量z
    private float _vz;
    //回転量
    private float _angle;
    //回転スピード
    public float _a_speed;
    //ジャンプパワー
    public float _jump_p;

    //ステータス
    public int _st;
    //カウント
    private int _count;
    //タイマー
    private float _timer;

    //プッシュ有無
    private bool _push_st;
    //接地有無
    private bool _ground_st;

    //モンキーオブジェクト
    private GameObject _Monkey;
    //モンキースクリプト
    private MonkeyManager _MonkeyManager;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプ前
    //_st=4-ジャンプ
        //_count=0-上昇
        //_count=1-トップ
        //_count=2-下降
    //_st=5-ジャンプ後
    //_st=6-攻撃
    //_st=7-攻撃後
    //_st=8-ダメージ
    //_st=9-落下ダメージ
    //_st=10-クリア
    //_st=11-スタート

    void Awake()
    {
        //定義
        _animtor = this.GetComponent<Animator>();
        _rbody = this.GetComponent<Rigidbody>();
        _angle2 = transform.localEulerAngles;

        _PandaMesh = transform.Find("PandaMesh").gameObject;
        _panda_mesh_collider = _PandaMesh.GetComponent<CapsuleCollider>();
        _panda_mesh_renderer = _PandaMesh.GetComponent<Renderer>();
        _panda_mesh_material = _panda_mesh_renderer.material;
        _panda_mesh_color = _panda_mesh_material.color;

        _MainCamera = GameObject.Find("Main Camera");

        for (int i=0; i<2; i++)
        {
            _Face[i] = GameObject.Find("Face"+(i+1));
        }

        for (int i = 0; i < 4; i++)
        {
            _Point[i] = GameObject.Find("Point" + (i + 1));
        }

        _GameOver = GameObject.Find("GameOver");
        _GameOverManager = _GameOver.GetComponent<GameOverManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 11;
        _count = 0;
        _ground_st = true;
        _animtor.Play("Base");

        _angle2.y = 180;
        transform.localEulerAngles = _angle2;

        _GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //_vx = 0;
        _vz = 0;
        _angle = 0;

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            _vz = _speed;
        }
        else if (Input.GetKey("down") || Input.GetKey("s"))
        {
            _vz = -_speed;
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            //_vx = _speed;
            _angle = _a_speed;
        }
        else if (Input.GetKey("left") || Input.GetKey("a"))
        {
            //_vx = -_speed;
            _angle = -_a_speed;
        }

        if (Input.GetKey("space"))
        {
            if ((_st==1||_st==2)&&_push_st==false && _ground_st==true)
            {
                _push_st = true;
                _st = 3;
                _timer =0;
                _animtor.Play("JumpSet");
            }
        }
        else
        {
            _push_st = false;
        }
    }

    void FixedUpdate()
    {
        if (_st==1)
        {
            if (_angle!=0 || _vz!=0)
            {
                _st = 2;
                _animtor.Play("Walk");
            }
        }
        else if (_st==2)
        {
            if (_angle == 0 && _vz == 0)
            {
                _st = 1;
                _animtor.Play("Base");
            }
        }
        //ジャンプ前
        else if (_st==3)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.2f)
            {
                _timer = 0;
                _st = 4;
                _count = 0;
                _rbody.AddForce(new Vector3(0,_jump_p,0),ForceMode.Impulse);
                _animtor.Play("JumpUp");
            }
        }
        //ジャンプ
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_count==0 && _timer>=0.2f)
            {
                _timer = 0;
                _count = 1;
                _animtor.Play("JumpTop");
            }
            else if (_count==1 && _timer>=0.2f)
            {
                _timer = 0;
                _count = 2;
                _animtor.Play("JumpDown");
            }
        }
        else if (_st == 5)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.3f)
            {
                _timer = 0;
                _st = 1;
                _animtor.Play("Base");
            }
        }
        else if (_st==6)
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                _timer = 0;
                _st = 7;
                _animtor.Play("JumpDown");
                _rbody.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
        else if (_st==8)
        {
            _panda_mesh_color.a -= 0.01f;
            if (_panda_mesh_color.a<=0)
            {
                _panda_mesh_color.a = 0;
                if (GameManager._zan_c>0)
                {
                    --GameManager._zan_c;
                    _Face[GameManager._zan_c].SetActive(false);
                    FallSet();
                }
                else
                {
                    _MainCamera.transform.parent = null;
                    _GameOver.SetActive(true);
                    _GameOverManager.ActiveSet();
                    this.gameObject.SetActive(false);
                }
            }
            _panda_mesh_material.color = _panda_mesh_color;
        }
        else if (_st==9)
        {
            if (transform.position.y<=-10)
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
        else if (_st==11)
        {
            if (_count==0)
            {
                _timer += Time.deltaTime;
                if (_timer>=1)
                {
                    _timer =0;
                    _count = 1;
                    _animtor.Play("StartSet");
                }
            }
            else if (_count==2)
            {
                _timer += Time.deltaTime;
                if (_timer>=1)
                {
                    _timer =0;
                    _count = 3;
                    _animtor.Play("StartAfter");
                }
            }
            else if (_count==4)
            {
                _angle2.y -= 10;
                if (_angle2.y<=0)
                {
                    _angle2.y = 0;
                    _count = 5;
                    _animtor.Play("TurnAfter");
                }
                transform.localEulerAngles = _angle2;
            }
        }

        if (_st==2 || _st==4)
        {
            this.transform.Translate(0, 0, _vz / 50);
            this.transform.Rotate(0,_angle/50,0);
        }

        if (_st!=9 && transform.position.y<=-1)
        {
            _st = 9;
            _animtor.Play("FallDame");
            _MainCamera.transform.parent = null;
        }
    }

    public void DameSet()
    {
        _st = 8;
        _animtor.Play("StrengthDame");
        _panda_mesh_collider.enabled = false;
        _rbody.constraints = RigidbodyConstraints.FreezePosition;
    }

    private void FallSet()
    {
        _MainCamera.transform.parent = this.gameObject.transform;
        _MainCamera.transform.localPosition = new Vector3(0,2,-3);
        _MainCamera.transform.localEulerAngles = new Vector3(10,0,0);

        _rbody.constraints = RigidbodyConstraints.FreezeRotation;
        _panda_mesh_collider.enabled = true;

        _panda_mesh_color.a = 1;
        _panda_mesh_material.color = _panda_mesh_color;

        for (int i=3; i>=0; i--)
        {
            if (transform.position.z>=_Point[i].transform.position.z || i==0)
            {
                transform.position = _Point[i].transform.position;
                _st = 7;
                _animtor.Play("JumpDown");
                break;
            }
        }
    }

    public void ClearSet()
    {
        _st = 10;
        _animtor.Play("Base");
    }

    public void StartSet(int _no)
    {
        if (_no==1)
        {
            _count = 2;
            _animtor.Play("WaveHand");
        }
        else if (_no==2)
        {
            _count = 4;
            _animtor.Play("TurnSet");
        }
        else if (_no==3)
        {
            _st = 1;
            _animtor.Play("Base");

            _MainCamera.transform.parent = this.transform;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Monkey" && _st==4)
        {
            _Monkey = collision.gameObject;
            _MonkeyManager = _Monkey.GetComponent<MonkeyManager>();

            _st = 6;
            _timer = 0;
            _animtor.Play("JumpAttack");
            _rbody.constraints = RigidbodyConstraints.FreezePosition;

            _MonkeyManager.DameSet();
        }
        else if ((collision.gameObject.tag == "Cube"||collision.gameObject.tag == "Cloud") && (_st == 4||_st==7))
        {
            _st = 5;
            _timer = 0;
            _animtor.Play("JumpArrive");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Cube"|| other.gameObject.tag == "Cloud")
        {
            _ground_st = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cube" || other.gameObject.tag == "Cloud")
        {
            _ground_st = false;
        }
    }
}
