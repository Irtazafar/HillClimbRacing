using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEditor.VersionControl;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{

    public Text messageText;
    void Start()
    {
        Login();
    }

   void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request,onSucess,onError);

       
    }

    void onSucess(LoginResult result)
    {
        Debug.Log("Successfull Login");
        GetTitleData();
    }

    void onError(PlayFabError error)
    {
        Debug.Log("Error while logging");
        Debug.Log(error.GenerateErrorReport());
    }

    public void OnLeaderboardUpdate(UpdatePlayerStatisticsRequest result)
    {
        
    }

    public void SendLeaderBoard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                    new StatisticUpdate {
                        StatisticName="Score",
                        Value = score
                    }
            }

        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, onError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull Updated in Leaderboard");
    }

    public void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest {
            StatisticName = "Score",
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, onError);

    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

    void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), onTitleDataRecieved, onError);
    }

    private void onTitleDataRecieved(GetTitleDataResult result)
    {
        if(result.Data==null || result.Data.ContainsKey("Message") == false)
        {
            Debug.Log("No Message");
            return;
        }

        messageText.text = result.Data["Message"];
    }
}
