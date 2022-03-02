using System.Collections;
using System.Collections.Generic;
using Cainos.PixelArtTopDown_Basic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance = null;

    public string characterName;
    public Slider healthBar;
    public Slider staminaBar;

    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text nameText;

    // Scene Management Variables
    public string areaTransitionName;

    private float speed;
    private int health;
    private int maxHealth = 10;
    private float stamina;
    private float maxStamina = 10f;
    private float staminaLossRate = 2f;
    private float staminaRegenRate = 1f;

    private Animator animator;

    // Characters within chat range
    GameObject chattableCharacter = null;

    // flags
    private bool isSprinting;
    private bool canChat;
    private bool isChatting;
    private bool isTransitioning;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;
        speed = GetComponent<TopDownCharacterController>().speed;
        isSprinting = false;
        canChat = false;
        isChatting = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for chatting (Start Conversation)
        if(canChat && Input.GetKeyDown(KeyCode.Z))
        {
            dialoguePanel.SetActive(true);
            isChatting = true;
            if (!chattableCharacter.GetComponent<NPCChat>().Chat(dialogueText, nameText, characterName))
            {
                isChatting = false;
                dialoguePanel.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger("IsAttacking");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isSprinting && GetComponent<Rigidbody2D>().velocity.magnitude > 0 && stamina > 0)
        {
            StartSprint();
        }
        if (isSprinting && (Input.GetKeyUp(KeyCode.LeftShift) || stamina <= 0 || GetComponent<Rigidbody2D>().velocity.magnitude <= 0))
        {
            StopSprint();
        }

        if(isSprinting)
        {
            stamina = stamina - staminaLossRate * Time.deltaTime < 0 ? 0 : stamina - staminaLossRate * Time.deltaTime;
            UpdateStaminaDisplay();
            animator.SetBool("IsSprinting", true);
        } else
        {
            stamina = stamina + staminaRegenRate * Time.deltaTime > maxStamina ? maxStamina : stamina + staminaRegenRate * Time.deltaTime;
            UpdateStaminaDisplay();
            animator.SetBool("IsSprinting", false);
        }
    }

    private void StartSprint()
    {
        isSprinting = true;
        speed *= 2;
        UpdateSpeed();
    }

    private void StopSprint()
    {
        isSprinting = false;
        speed /= 2;
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        GetComponent<TopDownCharacterController>().speed = speed;
    }

    private void TakeDamage(int damage)
    {
        health = health - damage < 0 ? 0 : health - damage;
        UpdateHealthDisplay();
        if (health <= 0)
            Destroy(gameObject);
    }

    private void UpdateHealthDisplay()
    {
        healthBar.value = (float)health / maxHealth;
    }

    private void UpdateStaminaDisplay()
    {
        staminaBar.value = stamina / maxStamina;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Talkable")
        {
            canChat = true;
            chattableCharacter = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Talkable")
        {
            canChat = false;
            chattableCharacter = null;
        }
    }

    public bool CanMove()
    {
        return !isChatting && !isTransitioning;
    }

    public void SetTransitioning(bool transition)
    {
        isTransitioning = transition;
    }
}
