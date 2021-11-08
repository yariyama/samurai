using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ブロックプレファブ
    private GameObject _Block_pf;
    //ブロックオブジェクト
    private GameObject _Block;
    //ブロックスクリプト
    private BlockManager _BlockManager;
    //ブロックヒットオブジェクト★
    private GameObject _BlockHit;


    //ブロックコライダー★
    private BoxCollider _block_collider;
    //ブロックヒットコライダー★
    private BoxCollider _block_hit_collider;

    //オーディオソース
    private AudioSource _audio;
    //BGM
    public AudioClip _bgm;

    //ブロックカウント
    public static int _block_c=0;

    //ブロックコレクション
    public static List<bool> _block_st = new List<bool>();
    //★
    public static List<int> _block_tp = new List<int>();
    public static List<float> _block_px = new List<float>();
    public static List<float> _block_py = new List<float>();
    public static List<float> _block_pz = new List<float>();
    //★
    public static List<int> _block_angle = new List<int>();


    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _block_c = PlayerPrefs.GetInt("BlockCount", 0);
        for (int i= 1; i <= _block_c;++i)
        {
            _Block_pf = (GameObject)Resources.Load("Prefabs/Block");
            _Block = Instantiate(_Block_pf) as GameObject;
            _BlockManager = _Block.GetComponent<BlockManager>();
            //★
            _block_collider = _Block.GetComponent<BoxCollider>();
            //★
            _BlockHit = _Block.transform.Find("BlockHit").gameObject;
            _block_hit_collider = _BlockHit.GetComponent<BoxCollider>();

            _BlockManager._ver = i;
            _BlockManager._st = 1;
            //★
            _BlockManager._tp = PlayerPrefs.GetInt("BlockTp" + i, 1);
            _BlockManager._angle= PlayerPrefs.GetInt("BlockAngle" + i, 0);
            //★
            if (_BlockManager._tp == 3)
            {
                _Block.transform.localScale = new Vector3(1, 0.1f, 1);
                _block_collider.size = new Vector3(0.7f, 0.7f, 0.9f);
            }
            else if (_BlockManager._tp==2)
            {
                _Block.transform.localScale = new Vector3(1,1,0.1f);
                _Block.transform.localEulerAngles = new Vector3(0, _BlockManager._angle, 0);
                _block_collider.size = new Vector3(0.7f, 0.7f, 0.9f);
                _block_hit_collider.size = new Vector3(0.8f, 0.8f, 0.8f);
            }

            _Block.transform.position=new Vector3(PlayerPrefs.GetFloat("BlockPx"+i, 0), PlayerPrefs.GetFloat("BlockPy"+i, 0), PlayerPrefs.GetFloat("BlockPz"+i, 0));

            _block_st.Add(true);
            //★
            _block_tp.Add(PlayerPrefs.GetInt("BlockTp" + i, 1));
            _block_px.Add(PlayerPrefs.GetFloat("BlockPx"+i, 0));
            _block_py.Add(PlayerPrefs.GetFloat("BlockPy"+i, 0));
            _block_pz.Add(PlayerPrefs.GetFloat("BlockPz"+i, 0));
            //★
            _block_angle.Add(PlayerPrefs.GetInt("BlockAngle" + i, 0));
        }

        _audio.clip = _bgm;
        _audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
