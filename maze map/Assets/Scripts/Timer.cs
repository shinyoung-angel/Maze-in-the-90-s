using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;//���� ��� ���

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public bool timeActive = false;
    public float CountTime;
    public Text text_Timer;
    private PhotonView pv;
    private int playercnt = 0;
    public GameObject TimeRecord;
    [SerializeField] Transform recordListContent;
    [SerializeField] GameObject recordListItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        timeActive = true;
        pv.RPC("StartTime", RpcTarget.All);
    }

    // Update is called once per frame
    private void Update()
    {
        if (timeActive)
        {
            CountTime += Time.deltaTime;
            text_Timer.text = "Time : " + CountTime.ToString("F2");
            if (CountTime > 200) //�ð� �ʰ� �� ������ �¸�
            {
                timeActive = false; //Ÿ�̸� ����
                foreach (int i in PhotonNetwork.CurrentRoom.Players.Keys)//�����ϴ� ��� roomListContent
                {
                    if (!GameManager.Tagged[i]) // ������ ���� ���
                    {
                        if (i== (int)PhotonNetwork.CurrentRoom.CustomProperties["Tagger"])
                        {
                            GameManager.records.Add(PhotonNetwork.CurrentRoom.Players[i].ToString().Substring(4), "Loser");
                        }
                        else
                        {
                            GameManager.records.Add(PhotonNetwork.CurrentRoom.Players[i].ToString().Substring(4), "Winner" );
                        }
                    }
                }
                foreach (KeyValuePair<string, string> record in GameManager.records)//�����ϴ� ��� roomListContent
                {
                    playercnt += 1;
                    Instantiate(recordListItemPrefab, recordListContent).GetComponent<RecordListItem>().SetUp(playercnt, record);
                }
                TimeRecord.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }


    public void crush(int i)
    {
        timeActive = false; //Ÿ�̸� ����
        GameManager.records.Add(PhotonNetwork.CurrentRoom.Players[i].ToString().Substring(4), "Winner");
        foreach (KeyValuePair<string, string> record in GameManager.records)//��� ����� ��
        {
            playercnt += 1;
            Instantiate(recordListItemPrefab, recordListContent).GetComponent<RecordListItem>().SetUp(playercnt, record);
        }
        TimeRecord.SetActive(true);
    }

    [PunRPC]
    void StartTime(PhotonMessageInfo info)
    {
        GameManager.startTime = info.SentServerTime;
    }

}
