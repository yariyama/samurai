using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SimplePun : MonoBehaviourPunCallbacks
{
    //プレイヤーオブジェクト
    private GameObject _Player;
    //プレイヤースクリプト
    private PlayerManager _PlayerManager;
    //回転スクリプト
    private RotateManager _RotateManager;
    //サブカメラオブジェクト
    private GameObject _SubCamera;
    //FPSキャラクターオブジェクト
    private GameObject _FPS_Character;
    //FPSキャラクタースクリプト
    private FPS_CharacterManager _FPS_CharacterManager;
    //マイNOオブジェクト
    private GameObject _MyNo;
    //ゾンビオブジェクト
    private GameObject _Zombie;
    //ゾンビスクリプト
    private ZombieManager _ZombieManager;

    //サブカメラカメラ
    private Camera _sub_camera_camera;
    //マイNOテキスト
    private Text _my_no_text;

    void Awake()
    {
        _MyNo = GameObject.Find("MyNo");
        _my_no_text = _MyNo.GetComponent<Text>();
    }

    // Use this for initialization
    void Start()
    {
        //旧バージョンでは引数必須でしたが、PUN2では不要です。
        PhotonNetwork.ConnectUsingSettings();
    }

    void OnGUI()
    {
        //ログインの状態を画面上に出力
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }


    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom()
    {
        //ナンバーリング
        int _s = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            ++_s;
        }
        _my_no_text.text = _s.ToString("00");

        //キャラクターを生成
        _Player = PhotonNetwork.Instantiate("Prefabs/Player", Vector3.zero, Quaternion.identity, 0);
        _Player.name = "Player" + _s;

        _SubCamera = _Player.transform.Find("SubCamera").gameObject;
        _FPS_Character = _Player.transform.Find("FPS_Character").gameObject;
        
        _Zombie= PhotonNetwork.Instantiate("Prefabs/Zombie", Vector3.zero, Quaternion.identity, 0);
        _Zombie.name = "Zombie" + _s;

        //自分だけが操作できるようにスクリプトを有効にする
        _PlayerManager = _Player.GetComponent<PlayerManager>();
        _RotateManager = _Player.GetComponent<RotateManager>();
        _PlayerManager.enabled = true;
        _RotateManager.enabled = true;
        _sub_camera_camera = _SubCamera.GetComponent<Camera>();
        _sub_camera_camera.enabled = true;
        _FPS_CharacterManager = _FPS_Character.GetComponent<FPS_CharacterManager>();
        _FPS_CharacterManager.enabled = true;
        
        _ZombieManager = _Zombie.GetComponent<ZombieManager>();
        _ZombieManager.enabled = true;
        _ZombieManager.ActiveSet(_s);
        
        
        //初期化
        _PlayerManager.ActiveSet(_s);
    }
}

