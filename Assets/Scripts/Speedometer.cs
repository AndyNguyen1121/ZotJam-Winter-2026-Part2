using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{

    public float maxHealth = 100f;
    // The min and max angle for the arrow
    private float minArrowAngle = -18f;
    private float maxArrowAngle = -163f;

    [Header("UI")]
    // The text indicating the health of the player
    public TMP_Text healthLabel;
    // The arrow indicating the health of the player
    public RectTransform arrow;

    private float health = 50f; // This should be from the Car's health

    void Update(){
        if (Input.GetKey(KeyCode.W)){
            health += 1f;
        }
        healthLabel.text = ((int)health) + " HP";
        arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minArrowAngle, maxArrowAngle, health / maxHealth));
    }
}
