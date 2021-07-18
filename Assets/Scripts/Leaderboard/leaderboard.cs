using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using TMPro;


public class leaderboard : MonoBehaviour
{
    private string Username;
    private int score;
    public  TMP_Text leaderBoardText;

    public AuthenticationManager _authenticationManager;

    [Obsolete]
    void Start()
    {
        //Username = "Alex";
        
        GetScoresForLeaderBoard();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            Username = (_authenticationManager.GetUsersId());
        }
        catch { }
    }

    [Serializable]
    public class EventData
    {
        public int id;
        public string Username;
        public int Clicks;
    }

    public EventData[] eventLeaderBoard;

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }

    public string DNS 
    {
        get 
        {
            return "http://ec2-18-236-231-98.us-west-2.compute.amazonaws.com:1337/tutorials";
        }
    }

    [Obsolete]
    public void GetScoresForLeaderBoard() 
    {
        StartCoroutine(GetScores());
    }

    [Obsolete]
    public void PostToLeaderBoard()
    {
        if (Username != null)
        {
            EventData leaderboardData = new EventData();
            leaderboardData.Clicks = score;
            string json = JsonUtility.ToJson(leaderboardData);
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);

            UnityWebRequest request;

            if (Array.Exists(eventLeaderBoard, x => x.Username == Username))
                for (int i = 0; i < eventLeaderBoard.Count(); i++)
                    if (eventLeaderBoard[i].Username == Username)
                    {
                        request = UnityWebRequest.Delete(DNS + "/" + eventLeaderBoard[i].id);
                        request.SendWebRequest();
                    }

            leaderboardData.Username = Username;
            json = JsonUtility.ToJson(leaderboardData);
            bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
            request = new UnityWebRequest(DNS, UnityWebRequest.kHttpVerbPOST);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.chunkedTransfer = false;
            request.SetRequestHeader("Content-type", "application/json");
            request.SendWebRequest();



            GetScoresForLeaderBoard();
        }
    }

    [Obsolete]
    public IEnumerator GetScores() 
    {
        UnityWebRequest www = UnityWebRequest.Get(DNS);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }

        else 
        {
            string json = "{ \"array\": " + www.downloadHandler.text + "}";
            Debug.Log(json);
            Wrapper<EventData> wrapper = JsonUtility.FromJson<Wrapper<EventData>>(json);
            eventLeaderBoard = wrapper.array.OrderByDescending(go => go.Clicks).ToArray();
        }

        leaderBoardText.text = "";
        int eventDataCounter = 1;
        foreach (EventData data in eventLeaderBoard)
        {
            leaderBoardText.text += eventDataCounter + ". " + data.Username + ": " + data.Clicks + "\n";
            eventDataCounter++;
        }
    }
}
