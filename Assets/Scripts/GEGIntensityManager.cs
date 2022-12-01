using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GEGFramework
{
    /*public interface ICustomMessageTarget : IEventSystemHandler
    {
        void IntensityIncrease(float i);
    }*/
    public class GEGIntensityManager : MonoBehaviour
    {
        public static event Action<float> OnIntensityChanged;
        [Range(0, 100)]
        public float intensity;
        //public float IncreaseIntensitivePerSecond;
        public float decreaseIntensitivePerSecond;

        public float decreaseIntensityInterval;
        float currentDecreaseIntensityInterval;
        public float increaseIntensityInterval;
        float currentIncreaseIntensityInterval;
        float increaseIntensityTimer;
        float decreaseIntensityTimer;

        public int relaxDuration;
        int leftRelaxDuration;
        public int peakDuration;
        int leftPeakDuration;
        public float peakThreshold;

        int waveNumber;

        float mulvalue;
        public GEGGameStatus GameStatus;
        public enum GEGGameStatus
        {
            Relax,
            Medium,
            High
        }

        //public static event Action<float> intensityIncrease;

        public float mediumExpectTime;
        float mediumExpectTimer;
        bool couldChange;

        // Start is called before the first frame update
        IEnumerator Start()
        {
            decreaseIntensityTimer = 0;
            increaseIntensityTimer = 0;
            OnIntensityChanged?.Invoke(intensity);
            leftRelaxDuration = relaxDuration;
            GameStatus = GEGGameStatus.Relax;
            mulvalue = 1;
            waveNumber = 0;
            mediumExpectTimer = mediumExpectTime;
            couldChange = true;
            GEGManager.OnNewWaveStart += (int _) => {
                couldChange = true;
                leftPeakDuration--;
                leftRelaxDuration--;
            };


            /*intensityIncrease += (float n) => {
                IntensityIncrease(n);
            };*/

            yield return new WaitForSeconds(1);
            StartRelaxMode();

        }


        // Update is called once per frame
        void Update()
        {
            //Debug.Log("Intensity " + intensity);
            if (GameStatus == GEGGameStatus.High)
            {
                if ((leftPeakDuration <= 0)&& couldChange)
                {
                    GameStatus = GEGGameStatus.Relax;
                    Debug.Log("Game in Relax Mode");
                    leftRelaxDuration = relaxDuration;
                    couldChange = false;
                    StartRelaxMode();
                }
            }
            else if (GameStatus == GEGGameStatus.Relax)
            {
                if ((leftRelaxDuration <= 0)&& couldChange)
                {
                    GameStatus = GEGGameStatus.Medium;
                    Debug.Log("Game in Medium Mode");
                    //intensityIncrease?.Invoke(20);
                    couldChange = false;
                    StartMediumMode();
                } 
            }
            else if (GameStatus == GEGGameStatus.Medium)
            {
                
                mediumExpectTimer -= Time.deltaTime;
                //intensity = intensity + IncreaseIntensitivePerSecond * Time.deltaTime;
                if ((intensity >= peakThreshold) && couldChange)
                {
                    GameStatus = GEGGameStatus.High;
                    Debug.Log("Game in High Mode");
                    couldChange = false;
                    leftPeakDuration = peakDuration;
                    StartHighMode();
                }
                else
                {
                    if (mediumExpectTimer <= 0 && couldChange)
                    {
                        GameStatus = GEGGameStatus.High;
                        Debug.Log("a long time, Game in High Mode");
                        couldChange = false;
                        leftPeakDuration = peakDuration;
                        StartHighMode();
                    }
                }
            } else
            {


            }
            if (Input.GetKey(KeyCode.Q))
            {
                IntensityIncrease(5*Time.deltaTime);
            }

            if (increaseIntensityTimer > 3)
            {
                mulvalue += Mathf.Clamp(0.1f,0.5f,3f);
                //Debug.Log("mulvalue 118 " + mulvalue);
                EnemyPropertyIncrease();
                increaseIntensityTimer = 0;
                currentDecreaseIntensityInterval = 0;
                decreaseIntensityTimer = 0;
            }

            //Only Intensity Increase = 0, we will start decrease Intensity timer
            if (increaseIntensityTimer == 0)
            {
                currentDecreaseIntensityInterval += Time.deltaTime;
                if (currentDecreaseIntensityInterval >= decreaseIntensityInterval)
                {
                    decreaseIntensityTimer++;
                    currentDecreaseIntensityInterval = 0;
                }
            }
            else
            {
                currentIncreaseIntensityInterval += Time.deltaTime;
                if (currentIncreaseIntensityInterval >= increaseIntensityInterval)
                {
                    currentIncreaseIntensityInterval = 0;
                    increaseIntensityTimer = 0;
                }
            }
            //The Intensity value does not increase for a long time, increase the attributes of the enemy
            if (decreaseIntensityTimer >= 3)
            {
                //Debug.Log("mulvalue 147 " + mulvalue);
                mulvalue += Mathf.Clamp(0.1f,0.5f,3f);
                EnemyPropertyIncrease();
                decreaseIntensityTimer = 1;
                if(intensity - decreaseIntensitivePerSecond * Time.deltaTime<=0){

                } else {
                    intensity = intensity - decreaseIntensitivePerSecond * Time.deltaTime;
                    OnIntensityChanged?.Invoke(intensity);
                }
                //intensity = Mathf.Clamp(intensity - decreaseIntensitivePerSecond * Time.deltaTime, 0, 100);
                
                
            }
            else if (decreaseIntensityTimer >= 1)
            {
                if(intensity - decreaseIntensitivePerSecond * Time.deltaTime<=0){

                } else {
                    intensity = intensity - decreaseIntensitivePerSecond * Time.deltaTime;
                    OnIntensityChanged?.Invoke(intensity);
                }
            }
        }

        void StartRelaxMode()
        {
            EnemyNumberUpdate(0, 2);
            EnemyNumberUpdate(1, 0);
            EnemyNumberUpdate(2, 0);
            EnemyPropertyChange("ZomBearHealth", 0, 100);
            EnemyPropertyChange("ZomBearSpeed", 0, 4);
            EnemyPropertyChange("ZomBearAttackDamage", 0, 10);
            EnemyPropertyChange("ZomBearAttackRate", 0, 0.7f);
        }
        void StartMediumMode()
        {

            EnemyNumberUpdate(0, 2);
            EnemyNumberUpdate(1, 2);
            EnemyNumberUpdate(2, 0);
            EnemyPropertyChange("ZomBearHealth", 0, 120);
            EnemyPropertyChange("ZomBearSpeed", 0, 6);
            EnemyPropertyChange("ZomBearAttackDamage", 0, 20);
            EnemyPropertyChange("ZomBearAttackRate", 0, 0.8f);
            EnemyPropertyChange("ZomBearHealth", 1, 100);
            EnemyPropertyChange("ZomBearSpeed", 1, 8);
            EnemyPropertyChange("ZomBearAttackDamage", 1, 20);
            EnemyPropertyChange("ZomBearAttackRate", 0, 1f);
        }

        void StartHighMode()
        {
            EnemyNumberUpdate(0, 2);
            EnemyNumberUpdate(1, 2);
            EnemyNumberUpdate(2, 1);
            EnemyPropertyChange("ZomBearHealth", 0, 150);
            EnemyPropertyChange("ZomBearSpeed", 0, 8);
            EnemyPropertyChange("ZomBearAttackDamage", 0, 30);
            EnemyPropertyChange("ZomBearAttackRate", 0, 1f);
            EnemyPropertyChange("ZomBearHealth", 1, 120);
            EnemyPropertyChange("ZomBearSpeed", 1, 8);
            EnemyPropertyChange("ZomBearAttackDamage", 1, 25);
            EnemyPropertyChange("ZomBearAttackRate", 1, 1.5f);
            EnemyPropertyChange("ZomBearHealth", 2, 150);
            EnemyPropertyChange("ZomBearSpeed", 2, 15);
            EnemyPropertyChange("ZomBearAttackDamage", 2, 30);
            EnemyPropertyChange("ZomBearAttackRate", 2, 2f);
        }

        public void IntensityIncrease(float i)
        {
            if(intensity - decreaseIntensitivePerSecond * Time.deltaTime<=0){

                } else {
                    intensity = intensity - decreaseIntensitivePerSecond * Time.deltaTime;
                    OnIntensityChanged?.Invoke(intensity);
                }
        
            if (GameStatus != GEGGameStatus.Medium)
            {
                increaseIntensityTimer++;
            }
            currentDecreaseIntensityInterval = 0;
            decreaseIntensityTimer = 0;
            currentIncreaseIntensityInterval = 0;
            if (increaseIntensityTimer > 3)
            {
                mulvalue += Mathf.Clamp(0.1f,0.5f,3f);
                EnemyPropertyDecrease();
                increaseIntensityTimer = 0;
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
                    foreach (GEGCharacterProperty prop in character.properties)
                    {
                        if (prop.enabled)
                        {
                            //Debug.Log("Property Increase");
                            prop.value = prop.value + prop.value * 0.1f;
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
                        foreach (GEGCharacterProperty prop in character.properties)
                        {
                            if (prop.enabled && (prop.propertyName == propName))
                            {
                                //Debug.Log("Property Change" + propName + " index "+ index + " from "+ prop.value + " to "+ value) ;
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
        /// enemy property Decrease.
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
                    foreach (GEGCharacterProperty prop in character.properties)
                    {
                        if (prop.enabled)
                        {
                            //Debug.Log("Property Decrease");
                            prop.value = prop.value + prop.value * 0.1f;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Generate enemy number.
        /// </summary>
        /// <param name="indexs">index of enemy</param>
        /// <param name="number">Generate enemy number</param>
        /// <returns></returns>
        void EnemyNumberUpdate(int indexs, int number)
        {
            int t = 0;
            for (int i = 0; i < GEGPackedData.characters.Count; i++)
            {
                if (GEGPackedData.characters[i].type == GEGCharacterType.Enemy)
                {
                    if (t == indexs)
                    {
                        GEGPackedData.characters[i].numNextWave = number;
                    }
                    t++;
                }
            }
        }

    }
}
