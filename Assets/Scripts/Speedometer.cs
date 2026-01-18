using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{

    public float maxHealth = 100f;
    // The min and max angle for the arrow
    private float minArrowAngle = 183f;
    private float maxArrowAngle = 7;

    [Header("UI")]
    // The text indicating the health of the player
    public TMP_Text healthLabel;
    // The arrow indicating the health of the player
    public RectTransform arrow;

    private float health = 100f; // This should be from the Car's health

    void Update(){
        if (Input.GetKeyDown(KeyCode.W)){
            TakeDamage(20f);
        }
        if (arrow){
            arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minArrowAngle, maxArrowAngle, health / maxHealth));
        }
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
    }
}
