using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{

    public static PlayFabManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        Loging();   
    }


    // Update is called once per frame
    void Loging()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnSuccessLogin, OnError);
    }

    void OnSuccessLogin(LoginResult result)
    {
        Debug.Log("successful login");
        GetLeaderboard();
    }

    void OnError(PlayFabError error)
    {
        Debug.LogWarning("Did not log in " + error.ToString() );
    }


    public void UpdateLeaderboard(ulong Score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "shooterGameLeaderboard",
                    Value = (int)Score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("leaderoard updated");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "shooterGameLeaderboard",
            StartPosition = 0,
            MaxResultsCount = 20
        };

        PlayFabClientAPI.GetLeaderboard(request, onLeaderboardGet, OnError);
    }

    void onLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            Debug.Log(item.Position +" ID: "+ item.PlayFabId +" Score: "+ item.StatValue);
        }
    }
}
