using UnityEngine;
using UnityEngine.UI;

public class MainMenuCamera : MonoBehaviour
{
    public Button StartButton;
    public Animator CameraAnimator;
    public GameObject FadeToBlack;
    public AudioSource AccelerationSound;

    void Start()
    {
        StartButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        CameraAnimator.Play("MenuCameraMoveUp");
        FadeToBlack.SetActive(true);
        AccelerationSound.Play();
    }
}
