using UnityEngine;

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

    //ステータス
    public int _st;
    //カウント
    private int _count;
    //タイマー
    private float _timer;
    //ランダム値
    private int _ran;
    //サーチ有無
    private bool _search_st;
    //角度ベース
    private float _angle_b;
    //角度ターゲット
    private float _angle_target1;
    private float _angle_target2;

    //_st=1-基本形
    //_st=2-移動

    void Awake()
    {
        _position = transform.position;
        _angle = transform.localEulerAngles;

        _animator = GetComponent<Animator>();
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
                _animator.Play("Walk");

                _Target = GameObject.Find("Player");
                _target_position = _Target.transform.position;
            }
        }
        else if (_st==2)
        {

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
