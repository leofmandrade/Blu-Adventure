using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI livesText; // Texto para exibir o número de vidas

    public GameObject winTextObject;
    public GameObject loseTextObject;
    public MusicController musicController;
    public AudioClip victorySoundEffect;
    public AudioClip loseSoundEffect;
    public AudioClip hitSoundEffect;
    public AudioClip pickupSoundEffect;
    

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int Second { get; set; }
    private int Minute { get; set; }
    private float timer;
    private int lives = 3; // Alterado para 3 vidas
    private AudioSource audioSource;

    private int pickUpCount = 0;
    private bool gameWon = false;

    public LayerMask groundMask;

    public static Action OnSecondChanged;
    public static Action OnMinuteChanged;
    private bool canTakeDamage = true;
    private float invincibilityTime = 1f;
    private float invincibilityTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);

        Second = 0;
        Minute = 0;
        timer = 0;
        UpdateTimeText();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        UpdateLivesText(); // Atualiza o texto das vidas ao iniciar o jogo
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }


    void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (Second > 19){
            LoseGame();
            return;
        }

        else if (timer >= 1)
        {
            timer = 0;
            Second++;
            if (Second >= 60)
            {
                Second = 0;
                Minute++;
                OnMinuteChanged?.Invoke();
            }
            OnSecondChanged?.Invoke();
            UpdateTimeText();

        }

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (transform.position.y < -5)
        {
            // respawn in the middle
            transform.position = Vector3.zero;
        }

        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else
        {
            canTakeDamage = true;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (!gameWon){
            if (other.gameObject.CompareTag("PickUp"))
            {
                pickUpCount ++;
                other.gameObject.SetActive(false);
                audioSource.PlayOneShot(pickupSoundEffect);

                if (pickUpCount == 9)
                {
                    audioSource.PlayOneShot(victorySoundEffect);
                    gameWon = true;
                    winTextObject.SetActive(true);
                    Invoke("LoadMainMenu", 5f);
                }


                if (lives < 3) // Se o jogador tiver menos de 3 vidas
                {
                    lives++;
                    UpdateLivesText();
                }
                Second -= 1;
                if (Second < 0)
                {
                    Second = 0;
                }
            }

            if (other.gameObject.CompareTag("Hazard")) // Se o jogador colidir com um objeto perigoso
            {
                if (canTakeDamage)
                {
                    TakeDamage();
                }
            }
        }
       
    }

    private void TakeDamage()
    {
        audioSource.PlayOneShot(hitSoundEffect);
        lives--;
        UpdateLivesText();
        canTakeDamage = false;
        invincibilityTimer = invincibilityTime;
        if (lives <= 0)
        {
            invincibilityTime = 5f;
            LoseGame();
        }
    }

    private void LoseGame()
    {
        if (!gameWon)
        {
            // desativa os pickups
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("PickUp");
            foreach (GameObject pickup in pickups)
            {
                pickup.SetActive(false);
            }

            // desativa a colisão com os hazards
            GameObject[] hazards = GameObject.FindGameObjectsWithTag("Hazard");
            foreach (GameObject hazard in hazards)
            {
                hazard.GetComponent<Collider>().enabled = false;
            }

            
            loseTextObject.SetActive(true);
            audioSource.PlayOneShot(loseSoundEffect);
            Invoke("LoadMainMenu", 5f);

        }
    }

    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = string.Format("{0:00}:{1:00}", Minute, Second);
        }
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives.ToString();
        }
    }
}
