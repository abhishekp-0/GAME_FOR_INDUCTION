using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class PostRequest : MonoBehaviour
{
    [System.Serializable]
    public class ScoreData
    {
        public string name;
        public string regno;
        public int score;
    }

    void Start()
    {
        // Example usage
        PostRequest postRequest = GetComponent<PostRequest>();
        if (postRequest != null)
        {
            postRequest.SendScoreData("JohnDoe", "REG12345", 95);
            Debug.Log("SendScoreData called with example data.");
        }
        else
        {
            Debug.LogError("PostRequest component is missing.");
        }
    }

    // Function to call the API
    public void SendScoreData(string name, string regno, int score)
    {
        Debug.Log($"Preparing to send score data: Name = {name}, RegNo = {regno}, Score = {score}");
        StartCoroutine(PostScoreData(name, regno, score));
    }

    // Coroutine to handle the API call
    private IEnumerator PostScoreData(string name, string regno, int score)
    {
        // Create a new ScoreData object
        ScoreData scoreData = new ScoreData
        {
            name = name,
            regno = regno,
            score = score
        };

        // Convert the object to a JSON string
        string json = JsonUtility.ToJson(scoreData);
        Debug.Log("JSON data to send: " + json);

        // Create a UnityWebRequest for a POST request
        UnityWebRequest request = new UnityWebRequest("https://induction-leaderboard.vercel.app", "POST");

        // Convert the JSON string to a byte array
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        // Attach the body to the request
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Set the content type to JSON
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending data: " + request.error);
        }
        else
        {
            Debug.Log("Server Response: " + request.downloadHandler.text);
        }
    }
}
