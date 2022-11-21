using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffTextUpdater : MonoBehaviour
{
    public TMPro.TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Default";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDiffText(int currentDiff)
    {
        text.text = "Diff: "+currentDiff;
    }
}
