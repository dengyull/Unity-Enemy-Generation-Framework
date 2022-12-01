using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GEGFramework;

namespace CompleteProject {
    public class GEGPlayerHealth : MonoBehaviour, IGEGController {

        [field: SerializeField]
        public GEGCharacter Character { get; set; }

        [field: SerializeField]
        public float Scaler { get; set; }

        [field: SerializeField]
        public bool Proportional { get; set; }

        public Slider healthSlider;                                 // Reference to the UI's health bar.
        public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
        public AudioClip deathClip;                                 // The audio clip to play when the player dies.
        public int summonCost = 50;                                 // Cost of summoning a bro to help you
        public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

        [HideInInspector]
        public float currentHealth;                                 // The current health the player has.

        Animator anim;                                              // Reference to the Animator component.
        AudioSource playerAudio;                                    // Reference to the AudioSource component.
        GEGPlayerMovement playerMovement;                           // Reference to the player's movement.
        GEGPlayerShooting playerShooting;                           // Reference to the PlayerShooting script.
        bool isDead;                                                // Whether the player is dead.
        bool damaged;                                               // True when the player gets damaged.

        void Awake() {
            // Setting up the references.
            anim = GetComponent<Animator>();
            playerAudio = GetComponent<AudioSource>();
            playerMovement = GetComponent<GEGPlayerMovement>();
            playerShooting = GetComponentInChildren<GEGPlayerShooting>();
        }

        void Start() {
            currentHealth = Character["PlayerHealth"].value;
        }

        void Update() {
            // If the player has just been damaged...
            if (damaged) {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            }
            // Otherwise...
            else {
                // ... transition the colour back to clear.
                if (damageImage != null)
                    damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.C) && gameObject.name == "GEG Player"
                && ScoreManager.score >= summonCost) {
                SummonBros();
            }

            // Reset the damaged flag.
            damaged = false;
        }

        public void SummonBros() {
            Transform broPos = GameObject.Find("GEG Player").transform;
            if (broPos) {
                int randRange = Random.Range(1, 4);
                GameObject bro = Instantiate(gameObject, broPos.position, transform.rotation);
                bro.transform.position = new Vector3(transform.position.x + randRange, transform.position.y,
                    transform.position.z + randRange);
                bro.name = "GEG Player (bro)";
            }
            ScoreManager.score -= summonCost;
        }

        public void TakeDamage(int amount) {
            // Set the damaged flag so the screen will flash.
            damaged = true;

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            IGEGController.valueChanged?.Invoke(amount); // Invoke this event for intensity calculation

            // Set the health bar's value to the current health.
            if (healthSlider != null)
                healthSlider.value = currentHealth;

            // Play the hurt sound effect.
            if (playerAudio != null)
                playerAudio.Play();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0 && !isDead) {
                // ... it should die.
                Death();
            }
        }


        void Death() {
            // Set the death flag so this function won't be called again.
            isDead = true;

            // Turn off any remaining shooting effects.
            playerShooting.DisableEffects();

            // Tell the animator that the player is dead.
            anim.SetTrigger("Die");

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            playerAudio.clip = deathClip;
            playerAudio.Play();

            // Turn off the movement and shooting scripts.
            playerMovement.enabled = false;
            playerShooting.enabled = false;
        }


        public void RestartLevel() {
            // Reload the level that is currently loaded.
            SceneManager.LoadScene(0);
        }
    }
}