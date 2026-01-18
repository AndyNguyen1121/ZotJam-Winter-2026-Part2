using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{

    public Button TesButton;
    public Button FordButton;
    public MainMenu Camera;
    
    public string nextLevel = "TerrainGeneration";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TesButton.onClick.AddListener(() => SetCharacter(Globals.Character.Tes));
        FordButton.onClick.AddListener(() => SetCharacter(Globals.Character.Ford));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCharacter(Globals.Character character){
        Debug.Log("AHAHAJHA");
        Globals.selectedCharacter = character;
        Camera.StartGame();
    }
}
