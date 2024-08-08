using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.Networking;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField registrationInputField;
    public Button submitButton;
    // private PostRequest postRequest;

    void Start()
    {
        // if (postRequest != null)
        // {
        //     postRequest.StartCoroutine(postRequest.PostRequest("localhost:3000", "{ \"name\":\"Unity\" }"));
        // }
        // else
        // {
        //     Debug.LogError("PostRequest reference is not assigned.");
        // }
        submitButton.onClick.AddListener(OnSubmit);
    }

    void OnSubmit()
    {
        // Get the text from the input fields
        string playerName = nameInputField.text;
        string registrationNo = registrationInputField.text;

        // Store the data in PlayerPrefs
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetString("RegistrationNo", registrationNo);

        // Optionally, you can load the next scene or give feedback to the player
        Debug.Log("Player Name and Registration Number saved!");
        Debug.Log(PlayerPrefs.GetString("PlayerName"));
        Debug.Log(PlayerPrefs.GetString("RegistrationNo"));

        SceneManager.LoadScene("Game");
    }
    IEnumerator PostRequest(string url, string json)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {request.error}");
        }
        else
        {
            Debug.Log($"Response: {request.downloadHandler.text}");
        }
    }
}
