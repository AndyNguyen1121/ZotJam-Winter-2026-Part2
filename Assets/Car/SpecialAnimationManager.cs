using UnityEngine;
using UnityEngine.UI;

public class SpecialAnimationManager : MonoBehaviour
{
    public Image portraitImage;
    public GameObject specialAnimation;

    private void Start()
    {
        portraitImage = GetComponent<Image>();
        specialAnimation.SetActive(false);

    }

    private void ActivateSpecialAnimation()
    {
        portraitImage.enabled = false;
        specialAnimation.SetActive(true);
    }


}
