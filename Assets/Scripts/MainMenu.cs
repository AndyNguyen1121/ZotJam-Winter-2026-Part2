using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button StartButton;
    public Animator CameraAnimator;
    public GameObject FadeToBlack;
    public AudioSource AccelerationSound;

    
    public string nextLevel = "SelectionScene";

    void Start()
    {
        StartButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        CameraAnimator.Play("MenuCameraMoveUp");
        FadeToBlack.SetActive(true);
        AccelerationSound.Play();
        StartCoroutine(LoadLevelSequence());
    }

    IEnumerator LoadLevelSequence(){
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(nextLevel);
    }

}
