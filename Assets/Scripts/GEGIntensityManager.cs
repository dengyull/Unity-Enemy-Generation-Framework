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
        public float DecreaseIntensitivePerSecond;

        public float DecreaseIntensityInterval;
        float CurrentDecreaseIntensityInterval;
        public float IncreaseIntensityInterval;
        float CurrentIncreaseIntensityInterval;
        float IncreaseIntensityTimer;
        float DecreaseIntensityTimer;

        public int RelaxDuration;
        int LeftRelaxDuration;
        public int PeakDuration;
        int LeftPeakDuration;
        public float peakThreshold;

        int WaveNumber;

        public float mulvalue;
        public GEGGameStatus GameStatus;
        public enum GEGGameStatus
        {
            Relax,
            Medium,
            High
        }

        //public static event Action<float> intensityIncrease;

        public float MediumExpectTime;
        float MediumExpectTimer;
        public bool CouldChange;

        // Start is called before the first frame update
        IEnumerator Start()
        {
            DecreaseIntensityTimer = 0;
            IncreaseIntensityTimer = 0;
            OnIntensityChanged?.Invoke(intensity);
            LeftRelaxDuration = RelaxDuration;
            GameStatus = GEGGameStatus.Relax;
            mulvalue = 1;
            WaveNumber = 0;
            MediumExpectTimer = MediumExpectTime;
            CouldChange = true;
            GEGManager.OnNewWaveStart += (int _) => {
                CouldChange = true;
                LeftPeakDuration--;
                LeftRelaxDuration--;
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
                if ((LeftPeakDuration <= 0)&& CouldChange)
                {
                    GameStatus = GEGGameStatus.Relax;
                    Debug.Log("Game in Relax Mode");
                    LeftRelaxDuration = RelaxDuration;
                    CouldChange = false;
                    StartRelaxMode();
                }
            }
            else if (GameStatus == GEGGameStatus.Relax)
            {
                if ((LeftRelaxDuration <= 0)&& CouldChange)
                {
                    GameStatus = GEGGameStatus.Medium;
                    Debug.Log("Game in Medium Mode");
                    //intensityIncrease?.Invoke(20);
                    CouldChange = false;
                    StartMediumMode();
                } 
            }
            else if (GameStatus == GEGGameStatus.Medium)
            {
                
                MediumExpectTimer -= Time.deltaTime;
                //intensity = intensity + IncreaseIntensitivePerSecond * Time.deltaTime;
                if ((intensity >= peakThreshold) && CouldChange)
                {
                    GameStatus = GEGGameStatus.High;
                    Debug.Log("Game in High Mode");
                    CouldChange = false;
                    LeftPeakDuration = PeakDuration;
                    StartHighMode();
                }
                else
                {
                    if (MediumExpectTimer <= 0 && CouldChange)
                    {
                        GameStatus = GEGGameStatus.High;
                        Debug.Log("a long time, Game in High Mode");
                        CouldChange = false;
                        LeftPeakDuration = PeakDuration;
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

            if (IncreaseIntensityTimer > 3)
            {
                mulvalue += Mathf.Clamp(0.1f,0.5f,3f);
                //Debug.Log("mulvalue 118 " + mulvalue);
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
                //Debug.Log("mulvalue 147 " + mulvalue);
                mulvalue += Mathf.Clamp(0.1f,0.5f,3f);
                EnemyPropertyIncrease();
                DecreaseIntensityTimer = 1;
                if(intensity - DecreaseIntensitivePerSecond * Time.deltaTime<=0){

                } else {
                    intensity = intensity - DecreaseIntensitivePerSecond * Time.deltaTime;
                    OnIntensityChanged?.Invoke(intensity);
                }
                //intensity = Mathf.Clamp(intensity - DecreaseIntensitivePerSecond * Time.deltaTime, 0, 100);
                
                
            }
            else if (DecreaseIntensityTimer >= 1)
            {
                if(intensity - DecreaseIntensitivePerSecond * Time.deltaTime<=0){

                } else {
                    intensity = intensity - DecreaseIntensitivePerSecond * Time.deltaTime;
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
            if(intensity - DecreaseIntensitivePerSecond * Time.deltaTime<=0){

                } else {
                    intensity = intensity - DecreaseIntensitivePerSecond * Time.deltaTime;
                    OnIntensityChanged?.Invoke(intensity);
                }
        
            if (GameStatus != GEGGameStatus.Medium)
            {
                IncreaseIntensityTimer++;
            }
            CurrentDecreaseIntensityInterval = 0;
            DecreaseIntensityTimer = 0;
            CurrentIncreaseIntensityInterval = 0;
            if (IncreaseIntensityTimer > 3)
            {
                mulvalue += Mathf.Clamp(0.1f,0.5f,3f);
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
            List<KeyValuePair<int, float>> temp = new List<KeyValuePair<int, float>>();
            
            for (int i = 0; i < GEGPackedData.characters.Count; i++)
            {
                if (GEGPackedData.characters[i].type == GEGCharacterType.Enemy)
                {
                    temp.Add(new KeyValuePair<int, float>(i, GEGPackedData.characters[i].diffFactor));
                }
            }
            GEGPackedData.characters[temp[indexs].Key].numNextWave = number;
        }

    }
}
