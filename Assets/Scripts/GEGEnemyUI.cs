using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEGEnemyUI : MonoBehaviour
{
    // To make the UI stay the same angle
    Quaternion angle;

    // UI components that need to be updated
    TMPro.TMP_Text hpText;
    TMPro.TMP_Text damageText;
    TMPro.TMP_Text rateText;

    // The health Script. Because it is used in update() so we store it as a class variable.
    CompleteProject.EnemyHealth healthScript;

    // Start is called before the first frame update
    void Start()
    {
        //record the initial angle 
        angle = transform.rotation;

        // Find UI components
        hpText = transform.Find("HP").GetComponent<TMPro.TMP_Text>();
        damageText = transform.Find("Damage").GetComponent<TMPro.TMP_Text>();
        rateText = transform.Find("Rate").GetComponent<TMPro.TMP_Text>();

        // Find health Script
        healthScript = transform.parent.GetComponent<CompleteProject.EnemyHealth>();

        // Update UI
        hpText.text = ""+ healthScript.currentHealth;
        damageText.text = "" + transform.parent.GetComponent<CompleteProject.EnemyAttack>().attackDamage;
        rateText.text = "" + transform.parent.GetComponent<CompleteProject.EnemyAttack>().timeBetweenAttacks;
    }
    void Update()
    {
        // Only health changes during gameplay
        hpText.text = ""+ healthScript.currentHealth;
    }

    // remain the angle
    void LateUpdate()
    {
        transform.rotation = angle;
    }
}
