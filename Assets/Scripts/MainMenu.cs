using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using PostRequest;
public class MainMenuManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField registrationInputField;
    public Button submitButton;

    void Start()
    {
        PostRequest postRequest = new PostRequest();
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
