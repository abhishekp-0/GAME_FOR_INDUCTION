using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField registrationInputField;
    public Button submitButton;
    private PostRequest postRequest;

    void Start()
    {
        if (postRequest != null)
        {
            postRequest.StartCoroutine(postRequest.PostRequest("localhost:3000", "{ \"name\":\"Unity\" }"));
        }
        else
        {
            Debug.LogError("PostRequest reference is not assigned.");
        }
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
}
