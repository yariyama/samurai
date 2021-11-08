using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoyManager : MonoBehaviour
{
    //ブロックプレファブ
    private GameObject _Block_pf;
    //ブロックオブジェクト
    private GameObject _Block;
    //ブロックスクリプト
    private BlockManager _BlockManager;
    //ブロックヒットオブジェクト★
    private GameObject _BlockHit;

    //ポールオブジェクト
    private GameObject _Pole;
    //ポールスクリプト
    private PoleManager _PoleManager;
    //ボーイスプライトオブジェクト
    private GameObject _BoySprite;
    //ボーイスプライトスクリプト
    private BoySpriteManager _BoySpriteManager;
    //メインカメラオブジェクト
    private GameObject _MainCamera;
    //カメラスクリプト
    private CameraManager _CameraManager;
    //ブロックタイプオブジェクト
    private GameObject _BlockType;

    //座標
    private Vector3 _position;
    //角度
    private Vector3 _angle;
    //カメラ座標
    private Vector3 _camera_position;
    //リジッドボディ
    private Rigidbody _rbody;

    //オーディオソース
    private AudioSource _audio;
    //効果音
    public AudioClip _se1;
    public AudioClip _se2;
    public AudioClip _se3;
    public AudioClip _se4;

    //ボーイスプライトアニメーター
    private Animator _boy_sprite_animator;

    //ブロックタイプテキスト
    private Text _block_type_text;

    //ブロックコライダー★
    private BoxCollider _block_collider;
    //ブロックヒットコライダー★
    private BoxCollider _block_hit_collider;

    //ステータス
    private int _st;
    //移動スピード
    public float _speed;
    //移動量X
    private float _vx;
    //移動量Z
    private float _vz;
    //ジャンプパワー
    public float _jump_p;
    //接地
    private bool _ground_st;

    //カウント
    private int _count;

    //向いてる方向
    private int _dire;
    //向いてるx方向
    private int _dire_x;
    //向いてるｙ方向
    private int _dire_z;
    //向いてる前のx方向
    private int _dire_x_b;
    //向いてる前のy方向
    private int _dire_z_b;
    //移動方向
    private int _dire_v;
    //方向角度
    private float _dire_angle;
    //ブロックセット
    private bool _block_set_st;
    //ブロックセットタイプ★
    private int _block_set_tp;

    //_dire=1-下
    //_dire=2-左下
    //_dire=3-左
    //_dire=4-左上
    //_dire=5-上
    //_dire=6-右上
    //_dire=7-右
    //_dire=8-右下

    //_st=1-基本形
    //_st=2-移動
    //_st=3-ジャンプセット
    //_st=4-ジャンプ
    //_st=5-攻撃
    //_st=6-ジャンプ攻撃

    void Awake()
    {
        _Pole = transform.Find("Pole").gameObject;
        _PoleManager = _Pole.GetComponent<PoleManager>();

        _MainCamera = GameObject.Find("Main Camera");
        _CameraManager = _MainCamera.GetComponent<CameraManager>();

        _BoySprite = transform.Find("BoySprite").gameObject;
        _BoySpriteManager = _BoySprite.GetComponent<BoySpriteManager>();
        _boy_sprite_animator = _BoySprite.GetComponent<Animator>();

        _position = transform.position;
        _angle = transform.localEulerAngles;
        _rbody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();

        _BlockType = GameObject.Find("BlockType");
        _block_type_text = _BlockType.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _vx = 0;
        _vz = 0;

        _dire_x = 0;
        _dire_z = 0;
        _dire_x_b = 0;
        _dire_z_b = 0;

        _ground_st = true;
        _block_set_st = false;
        _dire = 1;
        _dire_angle = 0;

        _block_set_tp = 1;

        AnimeSet(1);

        BillSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (_CameraManager._st==1) {
            _vx = 0;
            _vz = 0;
            _dire_x = 0;
            _dire_z = 0;

            //4方向のキー操作受付
            if (Input.GetKey("right") && _st != 5)
            {
                _dire_x = 1;
                if (_CameraManager._tp2 == 1)
                {
                    _vx = _speed;
                }
                else if (_CameraManager._tp2 == 2)
                {
                    _vz = _speed;
                }
                else if (_CameraManager._tp2 == 3)
                {
                    _vx = -_speed;
                }
                else if (_CameraManager._tp2 == 4)
                {
                    _vz = -_speed;
                }
            }
            else if (Input.GetKey("left") && _st != 5)
            {
                _dire_x = 2;
                if (_CameraManager._tp2 == 1)
                {
                    _vx = -_speed;
                }
                else if (_CameraManager._tp2 == 2)
                {
                    _vz = -_speed;
                }
                else if (_CameraManager._tp2 == 3)
                {
                    _vx = _speed;
                }
                else if (_CameraManager._tp2 == 4)
                {
                    _vz = _speed;
                }
            }
            if (Input.GetKey("up") && _st != 5)
            {
                _dire_z = 1;
                if (_CameraManager._tp2 == 1)
                {
                    _vz = _speed;
                }
                else if (_CameraManager._tp2 == 2)
                {
                    _vx = -_speed;
                }
                else if (_CameraManager._tp2 == 3)
                {
                    _vz = -_speed;
                }
                else if (_CameraManager._tp2 == 4)
                {
                    _vx = _speed;
                }
            }
            else if (Input.GetKey("down") && _st != 5)
            {
                _dire_z = 2;
                if (_CameraManager._tp2 == 1)
                {
                    _vz = -_speed;
                }
                else if (_CameraManager._tp2 == 2)
                {
                    _vx = _speed;
                }
                else if (_CameraManager._tp2 == 3)
                {
                    _vz = _speed;
                }
                else if (_CameraManager._tp2 == 4)
                {
                    _vx = -_speed;
                }
            }

            //スペースキー操作受付
            if (Input.GetKeyDown("space") && _ground_st && (_st == 1 || _st == 2 ))
            {
                _st = 3;
                if (_block_set_st)
                {

                    _block_set_st = false;
                    _BlockManager.DesSet();
                }
            }

            //sキー操作受付
            if (Input.GetKeyDown("s") && _st == 1 && _ground_st)
            {
                _block_set_st = true;
                BlockSet(1);
            }
            else if (Input.GetKeyUp("s") && _block_set_st)
            {
                _block_set_st = false;
                if (_st == 1)
                {
                    _BlockManager.ActiveSet(2);
                }
            }

            //攻撃
            if (Input.GetKeyDown("a") && (_st == 1||_st==2||_st==4))
            {
                if (_st == 4)
                {
                    _st = 6;
                }
                else
                {
                    _st = 5;
                }

                AnimeSet(3);

                //ポールセット
                _count = 0;
                if (_dire == 1 || _dire == 2 || _dire == 8)
                {
                    _PoleManager.ActiveSet(1, 1);
                }
                else if (_dire == 3)
                {
                    _PoleManager.ActiveSet(1, 2);
                }
                else if (_dire == 7)
                {
                    _PoleManager.ActiveSet(1, 3);
                }
                else if (_dire == 4 || _dire == 5 || _dire == 6)
                {
                    _PoleManager.ActiveSet(1, 4);
                }

            }

            //qキー受付
            if (Input.GetKeyDown("q"))
            {
                if (_block_set_tp==1)
                {
                    _block_set_tp = 2;
                    _block_type_text.text = "PLATE1";
                }
                else if(_block_set_tp == 2)
                {
                    _block_set_tp = 3;
                    _block_type_text.text = "PLATE2";
                }
                else
                {
                    _block_set_tp = 1;
                    _block_type_text.text = "BLOCK";
                }

                _audio.clip = _se4;
                _audio.Play();
            }
        }
    }

    void FixedUpdate()
    {
        //向きがある場合
        if (_dire_x != 0 || _dire_z != 0)
        {
            //前と向きが違う場合
            if (_dire_x != _dire_x_b || _dire_z != _dire_z_b)
            {
                _dire_x_b = _dire_x;
                _dire_z_b = _dire_z;

                //今の方向の設定
                //z方向なし
                if (_dire_z == 0)
                {
                    //x方向右
                    if (_dire_x == 1)
                    {
                        _dire = 7;
                    }
                    //x方向左
                    else if (_dire_x == 2)
                    {
                         _dire = 3;
                    }
                }
                //z方向上
                else if (_dire_z == 1)
                {
                    //x方向右
                    if (_dire_x == 1)
                    {
                        _dire = 6;
                    }
                    //x方向左
                    else if (_dire_x == 2)
                    {
                        _dire = 4;
                    }
                    //x方向なし
                    else
                    {
                        _dire = 5;
                    }
                }
                //z方向下
                else if (_dire_z == 2)
                {
                    //x方向右
                    if (_dire_x == 1)
                    {
                        _dire = 8;
                    }
                    //x方向左
                    else if (_dire_x == 2)
                    {
                        _dire = 2;
                    }
                    //x方向なし
                    else
                    {
                        _dire = 1;
                    }
                }

                //移動
                if (_st==2) {
                    AnimeSet(2);
                }
                //ジャンプ
                else if (_st==4)
                {
                    AnimeSet(1);
                }

                //移動方向
                //z方向なし
                if (_vz == 0)
                {
                    //z方向ストップ
                    if (_vx > 0)
                    {
                        //x方向右
                        _dire_v = 7;
                    }
                    else if (_vx < 0)
                    {
                        //x方向左
                        _dire_v = 3;
                    }
                }
                //z方向上
                else if (_vz > 0)
                {
                    if (_vx > 0)
                    {
                        //x方向右
                        _dire_v = 6;
                    }
                    else if (_vx < 0)
                    {
                        //x方向左
                        _dire_v = 4;
                    }
                    else
                    {
                        //x方向ストップ
                        _dire_v = 5;
                    }
                }
                //z方向下
                else if (_vz < 0)
                {
                    if (_vx > 0)
                    {
                        //x方向右
                        _dire_v = 8;
                    }
                    else if (_vx < 0)
                    {
                        //x方向左

                        _dire_v = 2;
                    }
                    else
                    {
                        //x方向ストップ
                        _dire_v = 1;
                    }
                }
                _dire_angle = (_dire_v - 1) * 45;
            }

            //基本形
            if (_st == 1)
            {
                _st = 2;

                if (_block_set_st)
                {
                    _BlockManager.DesSet();
                }

                AnimeSet(2);
            }

            //移動
            transform.Translate(_vx / 50, 0, _vz / 50, Space.World);

            _CameraManager.PosSet();

            BillSet();
        }
        else
        {
            if (_st == 2)
            {
                _st = 1;
                _dire_x = 0;
                _dire_z = 0;
                _dire_x_b = 0;
                _dire_z_b = 0;

                AnimeSet(1);

                if (_block_set_st)
                {
                    BlockSet(1);
                }
            }
        }

        //ジャンプ
        if (_st == 3)
        {
            _st = 4;
            _rbody.AddForce(new Vector3(0, _jump_p, 0), ForceMode.Impulse);
            AnimeSet(1);
            _audio.clip = _se1;
            _audio.Play();
        }
        //攻撃
        else if (_st == 5||_st==6)
        {
            if (_BoySpriteManager._attack_st==2)
            {
                if (_count==0) {
                    _count = 1;
                    //ポールセット
                    if (_dire == 1 || _dire == 2 || _dire == 8)
                    {
                        _PoleManager.ActiveSet(2, 1);
                    }
                    else if (_dire == 3)
                    {
                        _PoleManager.ActiveSet(2, 2);
                    }
                    else if (_dire == 7)
                    {
                        _PoleManager.ActiveSet(2, 3);
                    }
                    else if (_dire == 4 || _dire == 5 || _dire == 6)
                    {
                        _PoleManager.ActiveSet(2, 4);
                    }
                }
            }
            else if (_BoySpriteManager._attack_st == 0)
            {
                _st = 1;
                 AnimeSet(1);
                //ポール消滅
                _PoleManager.DelSet();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_st == 4)
        {
            _ground_st = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_st == 4||_st==6||_st==1||_st==2)
        {
            if (_st==6)
            {
                //ポール消滅
                _PoleManager.DelSet();
            }
            if (_st == 4 || _st == 6)
            {
                _audio.clip = _se2;
                _audio.Play();
            }
            _st = 1;
            AnimeSet(1);
            _ground_st = true;
        }
    }

    //ブロックセット
    void BlockSet(int _no)
    {
        //★
        float _b_angle;

        float _sx;
        float _sy;
        float _sz;
        int _bx;
        int _by;
        int _bz;

        if (_no == 1)
        {
            //プレハブからゲームオブジェクトを作成
            _Block_pf = (GameObject)Resources.Load("Prefabs/Block");
            _Block = Instantiate(_Block_pf) as GameObject;
            ++GameManager._block_c;
            _BlockManager = _Block.GetComponent<BlockManager>();
            _block_collider=_Block.GetComponent<BoxCollider>();
            _BlockManager._ver = GameManager._block_c;
            //★
            _BlockHit = _Block.transform.Find("BlockHit").gameObject;
            _block_hit_collider = _BlockHit.GetComponent<BoxCollider>();
        }

        //★
        if (_block_set_tp==2) {
            _BlockManager._tp = 2;
            _Block.transform.localScale = new Vector3(1,1,0.1f);
            _block_collider.size = new Vector3(0.7f,0.7f,0.9f);
            _block_hit_collider.size = new Vector3(0.8f,0.8f,0.8f);

            //プレートの角度
            if (_dire == 3 || _dire == 7)
            {
                if (_CameraManager._tp2 == 1 || _CameraManager._tp2 == 3)
                {
                    _Block.transform.eulerAngles = new Vector3(0, 90, 0);
                    _BlockManager._angle = 90;
                }
                else
                {
                    _Block.transform.eulerAngles = new Vector3(0, 0, 0);
                    _BlockManager._angle = 0;
                }
            }
            else
            {
                if (_CameraManager._tp2 == 1 || _CameraManager._tp2 == 3)
                {
                    _Block.transform.eulerAngles = new Vector3(0, 0, 0);
                    _BlockManager._angle = 0;
                }
                else
                {
                    _Block.transform.eulerAngles = new Vector3(0, 90, 0);
                    _BlockManager._angle = 90;
                }
            }
        }
        else if (_block_set_tp == 3)
        {
            _BlockManager._tp = 3;
            _Block.transform.localScale = new Vector3(1, 1, 0.1f);
            _block_collider.size = new Vector3(0.7f, 0.7f, 0.9f);
            _Block.transform.eulerAngles = new Vector3(90, 0, 0);
            _BlockManager._angle = 0;
        }
        else
        {
            _BlockManager._tp = 1;
            _BlockManager._angle = 0;
        }
        

        //現在の座標取得
        _position = transform.position;
        //ダミー角度を正規化し、キャラの正面角度に補正
        _b_angle = 360 - _dire_angle + 270;
        if (_b_angle >= 360)
        {
            _b_angle -= 360;
        }


        //角度、距離からセット位置を設定
        _sx = _position.x + Mathf.Cos(_b_angle * Mathf.PI / 180) * 1f;
        _sz = _position.z + Mathf.Sin(_b_angle * Mathf.PI / 180) * 1f;

        if (_CameraManager._tp1 == 2)
        {
            _sy = _position.y + 2f;
        }
        else
        {
            _sy = _position.y + 1f;
        }

        //一定間隔でセット出来るよう、位置を丸める
        _bx = (int)Mathf.Round(_sx / 1f);
        _sx = _bx * 1f;
        _bz = (int)Mathf.Round(_sz / 1f);
        _sz = _bz * 1f;
        _by = (int)Mathf.Round(_sy / 1f);
        _sy = (_by * 1f) - 0.5f;

        //★
        if (_block_set_tp==2) {
            //プレートの位置
            if (_CameraManager._tp2 == 1)
            {
                if (_dire == 1 || _dire == 2 || _dire == 8)
                {
                    _sz += 0.45f;
                }
                else if (_dire == 3)
                {
                    _sx += 0.45f;
                }
                else if (_dire == 7)
                {
                    _sx -= 0.45f;
                }
                else if (_dire == 4 || _dire == 5 || _dire == 6)
                {
                    _sz -= 0.45f;
                }
            }
            else if (_CameraManager._tp2 == 2)
            {
                if (_dire == 1 || _dire == 2 || _dire == 8)
                {
                    _sx -= 0.45f;
                }
                else if (_dire == 3)
                {
                    _sz += 0.45f;
                }
                else if (_dire == 7)
                {
                    _sz -= 0.45f;
                }
                else if (_dire == 4 || _dire == 5 || _dire == 6)
                {
                    _sx += 0.45f;
                }
            }
            else if (_CameraManager._tp2 == 3)
            {
                if (_dire == 1 || _dire == 2 || _dire == 8)
                {
                    _sz -= 0.45f;
                }
                else if (_dire == 3)
                {
                    _sx -= 0.45f;
                }
                else if (_dire == 7)
                {
                    _sx += 0.45f;
                }
                else if (_dire == 4 || _dire == 5 || _dire == 6)
                {
                    _sz += 0.45f;
                }
            }
            else if (_CameraManager._tp2 == 4)
            {
                if (_dire == 1 || _dire == 2 || _dire == 8)
                {
                    _sx += 0.45f;
                }
                else if (_dire == 3)
                {
                    _sz -= 0.45f;
                }
                else if (_dire == 7)
                {
                    _sz += 0.45f;
                }
                else if (_dire == 4 || _dire == 5 || _dire == 6)
                {
                    _sx -= 0.45f;
                }
            }
        }
        else if (_block_set_tp==3)
        {
            _sy += 0.45f;
        }

        _Block.transform.position = new Vector3(_sx, _sy, _sz);
        _BlockManager.ActiveSet(1);
        _audio.clip = _se3;
        _audio.Play();
    }

    //アニメーションのセット
    void AnimeSet(int _no)
    {
        //基本形
        if (_no == 1)
        {
            if (_dire == 1 || _dire == 2 || _dire == 8)
            {
                _boy_sprite_animator.Play("front_base");
            }
            else if (_dire == 3)
            {
                _boy_sprite_animator.Play("left_base");
            }
            else if (_dire == 7)
            {
                _boy_sprite_animator.Play("right_base");
            }
            else if (_dire == 4 || _dire == 5 || _dire == 6)
            {
                _boy_sprite_animator.Play("back_base");
            }
        }
        //歩き
        else if (_no == 2)
        {
            if (_dire == 1 || _dire == 2 || _dire == 8)
            {
                _boy_sprite_animator.Play("front_walk");
            }
            else if (_dire == 3)
            {
                _boy_sprite_animator.Play("left_walk");
            }
            else if (_dire == 7)
            {
                _boy_sprite_animator.Play("right_walk");
            }
            else if (_dire == 4 || _dire == 5 || _dire == 6)
            {
                _boy_sprite_animator.Play("back_walk");
            }
        }
        //攻撃
        else if (_no == 3)
        {
            if (_dire == 1 || _dire == 2 || _dire == 8)
            {
                _boy_sprite_animator.Play("front_attack");
            }
            else if (_dire == 3)
            {
                _boy_sprite_animator.Play("left_attack");
            }
            else if (_dire == 7)
            {
                _boy_sprite_animator.Play("right_attack");
            }
            else if (_dire == 4 || _dire == 5 || _dire == 6)
            {
                _boy_sprite_animator.Play("back_attack");
            }
        }
    }

    //ビルボードセット
    public void BillSet()
    {
        _camera_position = Camera.main.transform.position;
        _camera_position.y = transform.position.y;
        transform.LookAt(_camera_position);
        _angle = transform.localEulerAngles;
        _angle.y += 180;
        transform.localEulerAngles = _angle;
    }

    //移動停止
    public void StopSet()
    {
        if (_st==2) {
            _st = 1;

            _vx = 0;
            _vz = 0;

            _dire_x = 0;
            _dire_z = 0;
            _dire_x_b = 0;
            _dire_z_b = 0;

            AnimeSet(1);
        }
        else if (_st==4)
        {
            _vx = 0;
            _vz = 0;

            _dire_x = 0;
            _dire_z = 0;
            _dire_x_b = 0;
            _dire_z_b = 0;
        }
    }
}
