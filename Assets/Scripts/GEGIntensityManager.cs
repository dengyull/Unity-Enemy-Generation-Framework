using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GEGFramework
{
    public interface ICustomMessageTarget : IEventSystemHandler
    {
        void IntensityIncrease(float i);
    }
    public class GEGIntensityManager : MonoBehaviour, ICustomMessageTarget
    {
        public float intensity;
        public float IncreaseIntensitivePerSecond;

        public float DecreaseIntensityInterval;
        float CurrentDecreaseIntensityInterval;
        public float IncreaseIntensityInterval;
        float CurrentIncreaseIntensityInterval;
        float IncreaseIntensityTimer;
        float DecreaseIntensityTimer;

        public float RelaxDuration;
        float CurrentRelaxDuration;
        public float PeakDuration;
        float CurrentPeakDuration;
        public float PeakIntensity;

        float mulvalue;
        public GEGGameStatus GameStatus;
        public enum GEGGameStatus
        {
            Relax,
            Medium,
            High
        }

        // Start is called before the first frame update
        void Start()
        {
            DecreaseIntensityTimer = 0;
            IncreaseIntensityTimer = 0;
            CurrentRelaxDuration = RelaxDuration;
            GameStatus = GEGGameStatus.Relax;
            mulvalue = 1;
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("Intensity " + intensity);
            if (GameStatus == GEGGameStatus.High)
            {
                CurrentPeakDuration -= Time.deltaTime;
                if (CurrentPeakDuration <= 0)
                {
                    GameStatus = GEGGameStatus.Relax;
                    Debug.Log("Game in Relax Mode");
                    CurrentRelaxDuration = RelaxDuration;
                    EnemyNumberUpdate(0, 2);
                    EnemyNumberUpdate(1, 0);
                    EnemyNumberUpdate(2, 0);
                    EnemyPropertyChange("ZomBearHealth", 0, 100);
                    EnemyPropertyChange("ZomBearSpeed", 0, 4);
                    EnemyPropertyChange("ZomBearAttackDamage", 0, 10);
                    EnemyPropertyChange("ZomBearAttackRate", 0, (float)0.7);
                }
            }
            else if (GameStatus == GEGGameStatus.Relax)
            {
                CurrentRelaxDuration -= Time.deltaTime;
                if (CurrentRelaxDuration <= 0)
                {
                    GameStatus = GEGGameStatus.Medium;
                    Debug.Log("Game in Medium Mode");
                    EnemyNumberUpdate(0, 2);
                    EnemyNumberUpdate(1, 2);
                    EnemyNumberUpdate(2, 0);
                    EnemyPropertyChange("ZomBearHealth", 0, 120);
                    EnemyPropertyChange("ZomBearSpeed", 0, 6);
                    EnemyPropertyChange("ZomBearAttackDamage", 0, 20);
                    EnemyPropertyChange("ZomBearAttackRate", 0, (float)0.8);
                    EnemyPropertyChange("ZomBearHealth", 1, 100);
                    EnemyPropertyChange("ZomBearSpeed", 1, 8);
                    EnemyPropertyChange("ZomBearAttackDamage", 1, 20);
                    EnemyPropertyChange("ZomBearAttackRate", 0, (float)1);
                }
            }
            else if (GameStatus == GEGGameStatus.Medium)
            {
                intensity = intensity + IncreaseIntensitivePerSecond * Time.deltaTime;
                if (intensity >= PeakIntensity)
                {
                    GameStatus = GEGGameStatus.High;
                    Debug.Log("Game in High Mode");
                    CurrentPeakDuration = PeakDuration;
                    EnemyNumberUpdate(0, 2);
                    EnemyNumberUpdate(1, 2);
                    EnemyNumberUpdate(2, 1);
                    EnemyPropertyChange("ZomBearHealth", 0, 150);
                    EnemyPropertyChange("ZomBearSpeed", 0, 8);
                    EnemyPropertyChange("ZomBearAttackDamage", 0, 30);
                    EnemyPropertyChange("ZomBearAttackRate", 0, (float)1);
                    EnemyPropertyChange("ZomBearHealth", 1, 120);
                    EnemyPropertyChange("ZomBearSpeed", 1, 8);
                    EnemyPropertyChange("ZomBearAttackDamage", 1, 25);
                    EnemyPropertyChange("ZomBearAttackRate", 1, (float)1.5);
                    EnemyPropertyChange("ZomBearHealth", 2, 150);
                    EnemyPropertyChange("ZomBearSpeed", 2, 15);
                    EnemyPropertyChange("ZomBearAttackDamage", 2, 30);
                    EnemyPropertyChange("ZomBearAttackRate", 2, (float)2);
                }
            } else
            {

                if (IncreaseIntensityTimer > 3)
                {
                    mulvalue += (float)0.1;
                    Debug.Log("mulvalue 118 " + mulvalue);
                    EnemyPropertyIncrease();
                    IncreaseIntensityTimer = 0;
                    CurrentDecreaseIntensityInterval = 0;
                    DecreaseIntensityTimer = 0;
                }

                //Only Intensity Increase = 0, we will start decrease Intensity timer
                if (IncreaseIntensityTimer == 0)
                {
                    CurrentDecreaseIntensityInterval += Time.deltaTime;
                    if (CurrentDecreaseIntensityInterval >= DecreaseIntensityInterval)
                    {
                        DecreaseIntensityTimer++;
                        CurrentDecreaseIntensityInterval = 0;
                    }
                }
                else
                {
                    CurrentIncreaseIntensityInterval += Time.deltaTime;
                    if (CurrentIncreaseIntensityInterval >= IncreaseIntensityInterval)
                    {
                        CurrentIncreaseIntensityInterval = 0;
                        IncreaseIntensityTimer = 0;
                    }
                }
                //The Intensity value does not increase for a long time, increase the attributes of the enemy
                if (DecreaseIntensityTimer >= 3)
                {
                    Debug.Log("mulvalue 147 " + mulvalue);
                    mulvalue += (float)0.1;
                    EnemyPropertyIncrease();
                    intensity -= Time.deltaTime;
                    DecreaseIntensityTimer = 0;
                }
                else if (DecreaseIntensityTimer >= 1)
                {
                    intensity -= Time.deltaTime;
                }

            }
        }


        public void IntensityIncrease(float i)
        {
            intensity += i;
            if(GameStatus != GEGGameStatus.Medium)
            {
                IncreaseIntensityTimer++;
            }
            CurrentDecreaseIntensityInterval = 0;
            DecreaseIntensityTimer = 0;
            CurrentIncreaseIntensityInterval = 0;
            if (IncreaseIntensityTimer > 3)
            {
                mulvalue -= (float)0.1;
                EnemyPropertyDecrease();
                IncreaseIntensityTimer = 0;
            }
        }

        /// <summary>
        /// Generate enemy properties.
        /// </summary>
        /// <returns></returns>
        public void EnemyPropertyIncrease()
        {
            foreach (GEGCharacter character in GEGPackedData.characters)
            {
                if (character.type == GEGCharacterType.Enemy)
                {
                    foreach (GEGCharacterProperty prop in character.propSO)
                    {
                        if (prop.diffEnabled)
                        {
                            Debug.Log("Property Increase");
                            prop.value = prop.defaultValue * mulvalue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generate enemy properties.
        /// </summary>
        /// <returns></returns>
        public void EnemyPropertyChange(string propName, int index, float value)
        {
            int i = 0;
            foreach (GEGCharacter character in GEGPackedData.characters)
            {
                if (character.type == GEGCharacterType.Enemy)
                {
                    if(i == index)
                    {
                        foreach (GEGCharacterProperty prop in character.propSO)
                        {
                            if (prop.diffEnabled && (prop.propName == propName))
                            {
                                Debug.Log("Property Change" + propName + " in"+ index +" to "+ value) ;
                                prop.value = value * mulvalue;
                            }
                        }
                    } else
                    {
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Generate enemy properties.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <param name="PropertyList">Dictionary contains all attributes with enable or not</param>
        /// <returns></returns>
        public void EnemyPropertyDecrease()
        {
            foreach (GEGCharacter character in GEGPackedData.characters)
            {
                if (character.type == GEGCharacterType.Enemy)
                {
                    foreach (GEGCharacterProperty prop in character.propSO)
                    {
                        if (prop.diffEnabled)
                        {
                            Debug.Log("Property Decrease");
                            prop.value = prop.defaultValue * mulvalue;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Generate enemy number.
        /// </summary>
        /// <param name="difflevel">Difficulty level (from 0 to 10)</param>
        /// <returns></returns>
        void EnemyNumberUpdate(int index, int number)
        {
            List<KeyValuePair<int, float>> temp = new List<KeyValuePair<int, float>>();
            for (int i = 0; i < GEGPackedData.characters.Count; i++)
            {
                if (GEGPackedData.characters[i].type == GEGCharacterType.Enemy)
                {
                    temp.Add(new KeyValuePair<int, float>(i, GEGPackedData.characters[i].diffFactor));
                }
            }
            GEGPackedData.characters[temp[index].Key].nextWaveNum = number;
        }

    }
}
