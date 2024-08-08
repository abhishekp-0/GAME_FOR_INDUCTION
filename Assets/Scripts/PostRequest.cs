//using System.Collections;
//using UnityEngine;
//using UnityEngine.Networking;
//using System.Text;

//public class PostRequest : MonoBehaviour
//{
//    void Start()
//    {
//        StartCoroutine(PostRequest("localhost:3000", "{ \"name\":\"Unity\" }"));
//    }
//    IEnumerator PostRequest(string url, string json)
//    {
//        var request = new UnityWebRequest(url, "POST");
//        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
//        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
//        request.downloadHandler = new DownloadHandlerBuffer();
//        request.SetRequestHeader("Content-Type", "application/json");
//        yield return request.SendWebRequest();

//        if (request.result == UnityWebRequest.Result.ConnectionError || 
//            request.result == UnityWebRequest.Result.ProtocolError)
//        {
//            Debug.LogError($"Error: {request.error}");
//        }
//        else
//        {
//            Debug.Log($"Response: {request.downloadHandler.text}");
//        }
//    }
//}
