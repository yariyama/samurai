using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SimplePun : MonoBehaviourPunCallbacks
{
    //�v���C���[�I�u�W�F�N�g
    private GameObject _Player;
    //�v���C���[�X�N���v�g
    private PlayerManager _PlayerManager;
    //��]�X�N���v�g
    private RotateManager _RotateManager;
    //�T�u�J�����I�u�W�F�N�g
    private GameObject _SubCamera;
    //FPS�L�����N�^�[�I�u�W�F�N�g
    private GameObject _FPS_Character;
    //FPS�L�����N�^�[�X�N���v�g
    private FPS_CharacterManager _FPS_CharacterManager;
    //�}�CNO�I�u�W�F�N�g
    private GameObject _MyNo;

    //�T�u�J�����J����
    private Camera _sub_camera_camera;
    //�}�CNO�e�L�X�g
    private Text _my_no_text;

    void Awake()
    {
        _MyNo = GameObject.Find("MyNo");
        _my_no_text = _MyNo.GetComponent<Text>();
    }

    // Use this for initialization
    void Start()
    {
        //���o�[�W�����ł͈����K�{�ł������APUN2�ł͕s�v�ł��B
        PhotonNetwork.ConnectUsingSettings();
    }

    void OnGUI()
    {
        //���O�C���̏�Ԃ���ʏ�ɏo��
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }


    //���[���ɓ����O�ɌĂяo�����
    public override void OnConnectedToMaster()
    {
        // "room"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    //���[���ɓ�����ɌĂяo�����
    public override void OnJoinedRoom()
    {
        //�i���o�[�����O
        int _s = 0;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            ++_s;
        }
        _my_no_text.text = _s.ToString("00");

        //�L�����N�^�[�𐶐�
        _Player = PhotonNetwork.Instantiate("Prefabs/Player", Vector3.zero, Quaternion.identity, 0);
        _Player.name = "Player" + _s;

        _SubCamera = _Player.transform.Find("SubCamera").gameObject;
        _FPS_Character = _Player.transform.Find("FPS_Character").gameObject;

        //��������������ł���悤�ɃX�N���v�g��L���ɂ���
        _PlayerManager = _Player.GetComponent<PlayerManager>();
        _RotateManager = _Player.GetComponent<RotateManager>();
        _PlayerManager.enabled = true;
        _RotateManager.enabled = true;
        _sub_camera_camera = _SubCamera.GetComponent<Camera>();
        _sub_camera_camera.enabled = true;
        _FPS_CharacterManager = _FPS_Character.GetComponent<FPS_CharacterManager>();
        _FPS_CharacterManager.enabled = true;

        //������
        _PlayerManager.ActiveSet(_s);
    }
}

