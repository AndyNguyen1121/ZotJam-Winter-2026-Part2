using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject ford;
    public GameObject tes;

    private void OnEnable()
    {
        if (Globals.selectedCharacter == Globals.Character.Tes)
        {
            tes.SetActive(true);
            ford.SetActive(false);
        }
        else
        {
            tes.SetActive(false);
            ford.SetActive(true);
        }
    }
}
