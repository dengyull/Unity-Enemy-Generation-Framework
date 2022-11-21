using GEGFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GEGFramework
{
    public class Test : MonoBehaviour
    {
        public DiffTextUpdater textUpdater;
        GEGPackedData packedData;
        GEGFramework.GEGDifficultyManager diffManager;
        // Start is called before the first frame update
        void Start()
        {
            diffManager = new GEGFramework.GEGDifficultyManager(0);

            textUpdater.UpdateDiffText(0);
            InvokeRepeating("UpdateDiff", 5, 5);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void UpdateDiff()
        {
            Debug.Log("Test.UpdateDiff");
            int diff = diffManager.GetDifficulty(packedData, 2, 3, 4);
            textUpdater.UpdateDiffText(diff);
        }
    }
}
