using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks, IPunObservable//�ٸ� ���� ���� �޾Ƶ��̱� 
{
    [SerializeField] TMP_Text text;
    public TMP_Dropdown charselect;
    Player player;//���� ����Ÿ���� Player�� ���� �� �� �ְ� ���ش�.
    public GameObject dropbox;
    public int photon_index;

    void Start()
    {
        charselect.value = GameManager.char_idx;
    }
    public void OnDropdownEvent(int index)
    {
        GameManager.char_idx = index;
    }

    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;//�÷��̾� �̸� �޾Ƽ� �׻�� �̸��� ��Ͽ� �߰� ������ش�. 

        if (_player.NickName != PhotonNetwork.NickName)
        {
            TMP_Dropdown tMP_Dropdown = dropbox.GetComponent<TMP_Dropdown>();
            tMP_Dropdown.enabled = false;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)//�÷��̾ �涰������ ȣ��
    {
        if (player == otherPlayer)//���� �÷��̾ ����?
        {
            Destroy(gameObject);//�̸�ǥ ����
        }
    }

    public override void OnLeftRoom()//�� ������ ȣ��
    {
        Destroy(gameObject);//�̸�ǥ ȣ��
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(charselect.options[photon_index].text);
            // stream.SendNext(charselect.options[photon_index].image);
        }
        if (stream.IsReading)
        {
            charselect.captionText.text = (string)stream.ReceiveNext();
            //charselect.captionImage = (Image)stream.ReceiveNext();
        }
    }

}