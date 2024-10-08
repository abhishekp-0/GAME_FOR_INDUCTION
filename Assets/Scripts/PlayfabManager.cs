//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using PlayFab;
//using PlayFab.ClientModels;
//using static UnityEngine.Networking.UnityWebRequest;

//public class PlayfabManager : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        Login();
//    }

//    void Login()
//    {
//        var request = new LoginWithCustomIDRequest
//        {
//            CustomId = SystemInfo.deviceUniqueIdentifier,
//            CreateAccount = true
//        };
//        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
//    }

//    void OnSuccess(LoginResult result)
//    {
//        Debug.Log("Successful login/account create!");
//    }

//    void OnError(PlayFabError error)
//    {
//        Debug.Log("Error while logging in/creating account!");
//        Debug.Log(error.GenerateErrorReport());
//    }

//    public void SendLeaderboard(int Score)
//    {
//        var request = new UpdatePlayerStatisticsRequest
//        {
//            Statistics = new List<StatisticUpdate>
//            {
//                new StatisticUpdate
//                {
//                    StatisticName = "Score",
//                    Value = Score
//                }
//            }
//        };
//        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
//    }

//    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
//    {
//        Debug.Log("Successfull leaderboard sent");
//    }
//}

    