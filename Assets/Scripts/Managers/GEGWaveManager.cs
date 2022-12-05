using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GEGFramework
{

    public class GEGWaveManager : MonoBehaviour
    {

        [System.Serializable]
        public class GEGWaveEnemy
        {
            public GEGCharacter Character;
            public GEGCharacter getCharacter()
            {
                return Character;
            }

            public int number;
            public int getnumber()
            {
                return number;
            }
        }
        public List<GEGWaveEnemy> easyModeEnemy;
        public List<GEGWaveEnemy> normalModeEnemy;
        public List<GEGWaveEnemy> hardModeEnemy;

        // Start is called before the first frame update
        void Awake()
        {
            IntensityManager.OnModeChanged += (GameMode mode) => {
                apply(mode);
            };
        }

        // Update is called once per frame
        void Update()
        {

        }
        void apply(GameMode mode)
        {
            switch (mode)
            {
                case GameMode.Easy:
                    InEasyMode();
                    break;
                case GameMode.Normal:
                    InNormalMode();
                    break;
                case GameMode.Hard:
                    InHardMode();
                    break;
            }
        }

        void InEasyMode()
        {
            if (easyModeEnemy != null)
            {
                Debug.Log("InEasyMode");
                foreach (GEGCharacter c in PackedData.Instance.characters)
                {
                    c.numNextWave = 0;
                }
                foreach (GEGWaveEnemy c in easyModeEnemy)
                {
                    if (c.Character.type == CharacterType.Enemy)
                    {
                        c.Character.numNextWave = c.getnumber();
                    }
                }
            }
        }

        void InNormalMode()
        {
            if (normalModeEnemy != null)
            {
                Debug.Log("In Normal Mode");
                foreach (GEGCharacter c in PackedData.Instance.characters)
                {
                    c.numNextWave = 0;
                }
                foreach (GEGWaveEnemy c in normalModeEnemy)
                {
                    if (c.Character.type== CharacterType.Enemy)
                    {
                        c.Character.numNextWave = c.getnumber();
                    }
                }
            }
        }

        void InHardMode()
        {
            if (hardModeEnemy != null)
            {
                Debug.Log("In Hard Mode");
                foreach (GEGCharacter c in PackedData.Instance.characters)
                {
                    c.numNextWave = 0;
                }
                foreach (GEGWaveEnemy c in hardModeEnemy)
                {
                    if (c.Character.type == CharacterType.Enemy)
                    {
                        c.Character.numNextWave = c.getnumber();
                    }
                }
            }
        }
    }

}