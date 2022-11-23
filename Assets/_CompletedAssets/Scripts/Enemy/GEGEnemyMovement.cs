using UnityEngine;
using System.Collections;
using GEGFramework;

namespace CompleteProject {
    public class GEGEnemyMovement : MonoBehaviour, IGEGController {
        public GEGCharacter _character;
        public GEGCharacter Character {
            get => _character;
            set => _character = value;
        }

        Transform player;                  // Reference to the player's position.
        GEGPlayerHealth playerHealth;      // Reference to the player's health.
        GEGEnemyHealth enemyHealth;        // Reference to this enemy's health.
        UnityEngine.AI.NavMeshAgent nav;   // Reference to the nav mesh agent.


        void Awake() {
            // Set up the references.
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerHealth = player.GetComponent<GEGPlayerHealth>();
            enemyHealth = GetComponent<GEGEnemyHealth>();
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        void OnEnable() {
            nav.speed = _character["ZomBearSpeed"].value;
        }

        void Update() {
            // If the enemy and the player have health left...
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
                // ... set the destination of the nav mesh agent to the player.
                nav.SetDestination(player.position);
            }
            // Otherwise...
            else {
                // ... disable the nav mesh agent.
                nav.enabled = false;
            }
        }
    }
}