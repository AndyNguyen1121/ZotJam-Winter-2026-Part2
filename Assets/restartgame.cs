using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restartgame : MonoBehaviour
{
    public Button StartButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartButton.onClick.AddListener(Bye);
    }

    void Bye()
    {
        Debug.Log("bye");
        SceneManager.LoadScene("MainMenu");
    }
}
