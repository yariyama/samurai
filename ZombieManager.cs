using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    //ターゲットオブジェクト
    private GameObject _Target;
    //レイヒットオブジェクト
    private GameObject _ReyHitObject;

    //座標
    private Vector3 _position;
    //正面角度
    private Vector3 _forword_angle;
    //角度
    private Vector3 _angle;
    //ターゲット座標
    private Vector3 _target_position;
    //アニメーター
    private Animator _animator;
    //ナビメッシュエージェント
    private NavMeshAgent _agent;

    //ステータス
    public int _st;
    //カウント
    private int _count;
    //タイマー
    private float _timer;
    private float _timer2;
    //ラジアン
    private float _rad;
    //角度
    private float _rot;
    //距離
    private float _dis_b = 1.3f;
    //x移動量
    private float _vx;
    //z移動量
    private float _vz;
    //ランダム値
    private int _ran;
    //サーチ有無
    private bool _search_st;
    //角度ベース
    private float _angle_b;
    //角度ターゲット
    private float _angle_target1;
    private float _angle_target2;

    //ターゲット角度
    private float _target_rot;
    //ターゲット角度タイプ
    private int _target_rot_tp;

    //ターゲットz
    private float _target_z;
    //ターゲットzタイプ
    private int _target_z_tp;

    //_st=1-基本形
    //_st=2-移動
    //_st=3-攻撃
    //_st=4-攻撃後

    void Awake()
    {
        _position = transform.position;
        _angle = transform.localEulerAngles;

        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _st = 1;
        _count = 0;
        _timer = 0;
    }

    void FixedUpdate()
    {
        if (_st == 1)
        {
            if (_count==0)
            {
                _angle_b = transform.localEulerAngles.y;
                _angle_target1 = _angle_b - 30;
                _angle_target2 = _angle_b + 30;
                _count = 1;
            }
            else if (_count==1)
            {
                _angle.y -= 0.1f;
                if (_angle.y<=_angle_target1)
                {
                    _angle.y = _angle_target1;
                    _count = 2;
                    _timer = 0;
                }
                transform.localEulerAngles = _angle;
            }
            else if (_count==2||_count==4)
            {
                _timer += Time.deltaTime;
                if (_timer>=1)
                {
                    _ran = Random.Range(0,10);
                    if (_ran==1)
                    {
                        if (_count==2) {
                            _count = 3;
                        }
                        else if (_count==4)
                        {
                            _count = 1;
                        }
                    }
                }
            }
            else if (_count == 3)
            {
                _angle.y += 0.1f;
                if (_angle.y >= _angle_target2)
                {
                    _angle.y = _angle_target2;
                    _count = 4;
                    _timer = 0;
                }
                transform.localEulerAngles = _angle;
            }

            RaySet();

            if (_search_st)
            {
                _st = 2;
                _count = 0;
                _animator.Play("Walk");

                _Target = GameObject.Find("Player");
                _target_position = _Target.transform.position;
            }
        }
        else if (_st==2)
        {
            _agent.destination = _Target.transform.position;
        }
        /*
        else if (_st==2)
        {
            if (_count==0)
            {
                _position = transform.position;
                _target_position = _Target.transform.position;

                //2点間の角度
                _rad = Mathf.Atan2(_target_position.z - _position.z, _target_position.x - _position.x);
                _rot = _rad * 180 / Mathf.PI;
                if (_rot < 0)
                {
                    _rot += 360;
                }
                _target_rot = -_rot + 90;

                if (_target_rot < 0)
                {
                    _target_rot += 360;
                }
                if (_angle.y < 0)
                {
                    _angle.y += 360;
                }

                if (_target_rot>=_angle.y)
                {
                    if (_target_rot-_angle.y>_angle.y-_target_rot)
                    {
                        _target_rot_tp = 1;
                    }
                    else
                    {
                        _target_rot_tp = 2;
                    }
                }
                else
                {
                    if (_angle.y-_target_rot_tp > _target_rot-_angle.y)
                    {
                        _target_rot_tp = 2;
                    }
                    else
                    {
                        _target_rot_tp = 1;
                    }
                }

                if (_target_position.z>_position.z)
                {
                    _target_z_tp = 1;
                }
                else
                {
                    _target_z_tp = 2;
                }
                _target_z = _target_position.z;

                _count = 1;
                _animator.Play("Walk");
            }
            else if (_count==1)
            {
                _vx = Mathf.Cos(_rot * Mathf.PI / 180) * _dis_b;
                _vz = Mathf.Sin(_rot * Mathf.PI / 180) * _dis_b;
                _position.x += _vx / 50;
                _position.z += _vz / 50;

                if (_target_z_tp==1 && _position.z>=_target_z)
                {
                    _count = 0;
                }
                else if (_target_z_tp == 2 && _position.z <= _target_z)
                {
                    _count = 0;
                }

                transform.position = _position;

                _angle = transform.localEulerAngles;
                if (_angle.y < 0)
                {
                    _angle.y += 360;
                }

                if (_target_rot_tp==1 && _angle.y<=_target_rot)
                {
                    _angle.y += 5f;
                }
                else if (_target_rot_tp == 2 && _angle.y >= _target_rot)
                {
                    _angle.y -= 5f;
                }
                transform.localEulerAngles = _angle;

                _timer2 += Time.deltaTime;
                if (_timer2 >= 3)
                {
                    _ran = Random.Range(0, 1);
                    if (_ran == 1)
                    {
                        _timer = 0;
                        _count = 0;
                    }
                }
            }

            _timer += Time.deltaTime;
            if (_timer>=10)
            {
                _ran = Random.Range(0,20);
                if (_ran==1)
                {
                    _timer = 0;
                    _st = 1;
                    _count = 0;
                    _animator.Play("Idle");
                }
            }

        }
        */
        /*
        else if (_st == 2)
        {
            _position = transform.position;
            _target_position = _Target.transform.position;

            //2点間の角度
            _rad = Mathf.Atan2(_target_position.z - _position.z, _target_position.x - _position.x);
            _rot = _rad * 180 / Mathf.PI;
            if (_rot < 0)
            {
                _rot += 360;
            }
            _angle.y = -_rot + 90;
            transform.localEulerAngles = _angle;

            _vx = Mathf.Cos(_rot * Mathf.PI / 180) * _dis_b;
            _vz = Mathf.Sin(_rot * Mathf.PI / 180) * _dis_b;

            _position.x += _vx / 50;
            _position.z += _vz / 50;
            transform.position = _position;

            _timer += Time.deltaTime;
            if (_timer >= 10)
            {
                _ran = Random.Range(0, 20);
                if (_ran == 1)
                {
                    _timer = 0;
                    _st = 1;
                    _count = 0;
                    _animator.Play("Idle");
                }
            }
        }
        */
        else if (_st==4)
        {
            _timer += Time.deltaTime;
            if (_timer>=0.5f)
            {
                _st = 1;
                _timer = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackSet()
    {
        _st = 3;
        _animator.Play("Attack");
    }

    public void AttackEnd()
    {
        _st = 4;
        _timer = 0;
        _animator.Play("Idle");
    }

    private void RaySet()
    {
        _search_st = false;
        _ReyHitObject = null;

        _position = transform.position;
        _position.y += 1.5f;
        _forword_angle = transform.forward;

        Ray ray = new Ray(_position, _forword_angle);
        RaycastHit hit = new RaycastHit();

        //レイ確認用
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 3);

        if (Physics.Raycast(ray, out hit, 10))
        {
            _ReyHitObject = hit.collider.gameObject;
            if (_ReyHitObject.name == "Player")
            {
                _search_st = true;
            }
        }
    }
}
