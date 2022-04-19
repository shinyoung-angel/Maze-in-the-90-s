using UnityEngine;
using UnityEngine.UI;
using FirebaseWebGL.Scripts.FirebaseBridge;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MiniJSON;


namespace FirebaseWebGL.Examples.Auth
{
    public class MyPageHandler : MonoBehaviour
    {
        public static MyPageHandler instance;
        //�ؿ� ������ ���� ������Ʈ(����)
        [SerializeField] Transform recordListContent;
        //������
        [SerializeField] GameObject gameRecordPrefab;
        //�ؿ� ������ ���� ������Ʈ(��ŷ)
        [SerializeField] Transform rankListContent;
        //������
        [SerializeField] GameObject gameRankPrefab;



        void Start()
        {
            GetRecords(FirebaseWebGL.Examples.Auth.LobbyHandler.userName);
            GetRanks(FirebaseWebGL.Examples.Auth.LobbyHandler.userName);
        }


        //���� ������ ����(���������� ��ȯ�� ��)
        public void SetUp(string record)
        {
            var text = "";

            //JSON ���ڿ� ���¿��� �ٽ� Deserialize
            Dictionary<string, object> response = Json.Deserialize(record) as Dictionary<string, object>;

            //�ε����� ����Ͽ� �ð� ���� ����
            string time = response["time"].ToString();

            //�ð��� 12:11 �̷� �������� ��ȯ
            //string time1 = time.Substring(0, 4);

            if (time.Length >= 6)
            {
                for (int i = 0; i < time.Length; i++)
                {
                    if (time[i] == '.')
                    {
                        text += ':';

                        for (int j = i + 1; j < i + 3; j++)
                        {
                            text += time[j];
                        }
                        break;
                    }
                    else
                    {
                        text += time[i];
                    }
                }

                response["time"] = text;
            }

            else
            {
                for (int i = 0; i < time.Length; i++)
                {
                    if (time[i] == '.')
                    {
                        text += ':';
                    }
                    else
                    {
                        text += time[i];
                    }
                }

                response["time"] = text;
            }


            //Debug
            Debug.Log(response["mode"]);
            Debug.Log(response["map"]);
            Debug.Log(response["players"]);
            Debug.Log(response["rank"]);
            Debug.Log(response["time"]);

            Debug.Log(response["mode"].GetType().Name);
            Debug.Log(response["map"].GetType().Name);
            Debug.Log(response["players"].GetType().Name); // int�̹Ƿ� string���� ��ȯ�ؾ� ��
            Debug.Log(response["rank"].GetType().Name); //�굵
            Debug.Log(response["time"].GetType().Name);

            string textPlayers = response["players"].ToString();
            string textRank = response["rank"].ToString();

            response["players"] = textPlayers;
            response["rank"] = textRank;

            //MyPageItem.cs�� ������
            Instantiate(gameRecordPrefab, recordListContent).GetComponent<MyPageItem>().SetUp(response);
        }

        public void GetRecords(string username) =>
               FirebaseDatabase.GetRecords(username);

        public void GetRanks(string username) =>
               FirebaseDatabase.GetRanks(username);
    }
}