using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    [SerializeField] private float movementSpeed = 5f;
    float currentMovementSpeed;
    [SerializeField] private float jumpHeight = 3f;
    float currentJumpHeight;

    private SpriteRenderer playerRenderer;
    public Transform bulletSpawnLocation;
    public GameObject bulletPrefab;
    public PlayerHealth _playerhealth;
    
    private Animator playerAnimator;
    public Animator TransitionAnimation;
    WaitForSeconds delay = new WaitForSeconds(1);

    private AudioSource audioPlayer;
    [SerializeField] public AudioClip jump;
    [SerializeField] private AudioClip Landing;
    [SerializeField] private AudioClip PowerUp;
    [SerializeField] private AudioClip Shrooms;
    [SerializeField] private AudioClip Ending;
    [SerializeField] private AudioClip Shoot;
    [SerializeField] private AudioClip naDead;
    
    public float powerUpTimer = 0f;
    public float powerUp1Timer = 0f;
    public bool isGrounded = false;

    void Start()
    {
    playerRigidbody = GetComponent<Rigidbody2D>();
    playerAnimator = GetComponent<Animator>();
    playerRenderer = GetComponent<SpriteRenderer>();
    audioPlayer = GetComponent<AudioSource>();
    _playerhealth = GetComponent<PlayerHealth>();

    

    currentJumpHeight = jumpHeight;
    currentMovementSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {  
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            playerRigidbody.AddForce(Vector2.up * currentJumpHeight, ForceMode2D.Impulse);
            playerAnimator.SetTrigger("Jumping");
            audioPlayer.PlayOneShot(jump);
        }
         
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * currentMovementSpeed * horizontalInput * Time.deltaTime);

        if(Mathf.Abs(horizontalInput) > 0)
        {
            playerAnimator.SetTrigger("Running");
        }
        else if (Mathf.Abs(horizontalInput) == 0)
        {
            playerAnimator.SetTrigger("Idle");
        }
        if(horizontalInput > 0)
        {
            playerRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
             playerRenderer.flipX = true;
        }
        if(powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
            if(powerUpTimer <= 0)
            {
                powerUpTimer = 0;
                currentJumpHeight = jumpHeight;
                currentMovementSpeed = movementSpeed;
            }
        }
        if(powerUp1Timer > 0)
        {
            powerUp1Timer -= Time.deltaTime;
            if(powerUp1Timer <= 0)
            {
                powerUp1Timer = 0;
                currentMovementSpeed = movementSpeed;
            }
        }
        ShootBullets();
    
    }
    void ShootBullets()
    {   
        float direction = 0;
        
        if(Input.GetAxis("Horizontal") >= 0)
        {
            bulletSpawnLocation.position = transform.position + new Vector3(0.50f, 0, 0);
            direction = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            bulletSpawnLocation.position = transform.position - new Vector3(0.50f, 0, 0);
             direction = -1;
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            GameObject g = Instantiate(bulletPrefab, bulletSpawnLocation.position, Quaternion.identity);
            playerAnimator.SetTrigger("Shoot");
            audioPlayer.PlayOneShot(Shoot);
            g.GetComponent<Bullet>().speed *= direction;
            Destroy(g, 1);
        }
        
    }
    private void OnCollisionStay2D(Collision2D c)
    {
        if(c.gameObject.tag == "Ground")
        {
         isGrounded = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    { 
         if(c.gameObject.tag == "Ground")
        {
         audioPlayer.PlayOneShot(Landing);
         
        }
       if(c.gameObject.tag == "PowerUp")
       {
            currentJumpHeight = jumpHeight * 2;
            powerUpTimer = 10;
            Destroy(c.gameObject);
            audioPlayer.PlayOneShot(PowerUp);
       }
       if(c.gameObject.tag == "Sprint")
       {
            currentMovementSpeed = movementSpeed * 2;
            powerUp1Timer = 10;
            Destroy(c.gameObject);
            audioPlayer.PlayOneShot(PowerUp);
       }
       if(c.gameObject.tag == "Super ups")
       {
            currentJumpHeight = jumpHeight * 3;
            currentMovementSpeed = movementSpeed * 2;
            powerUpTimer = 10;
            Destroy(c.gameObject);
            audioPlayer.PlayOneShot(PowerUp);
       }
       
       
        if(c.gameObject.tag.Equals("CheckPoint"))
        {
            audioPlayer.PlayOneShot(PowerUp);
        }
       
       
    }
    private void OnCollisionExit2D(Collision2D c)
    {
        if(c.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Shrooms"))
        {
            GetComponent<PlayerStats>().EditScore(collision.gameObject.GetComponent<Shrooms>().ShroomValue);
            Destroy(collision.gameObject);
            audioPlayer.PlayOneShot(Shrooms);
        }
        if(collision.gameObject.tag.Equals("Restart"))
        {   
            StartCoroutine(RestartScene());
            Destroy(collision.gameObject);
            audioPlayer.PlayOneShot(naDead);
        }
        if(collision.gameObject.tag.Equals("End"))
        {   
            StartCoroutine(EndScene());
        }
        if(_playerhealth.HP <= 0)
        {
            StartCoroutine(RestartScene());
            audioPlayer.PlayOneShot(naDead);
        }
        
    }
    IEnumerator RestartScene()
    {   
        TransitionAnimation.Play("FadeOut");
        yield return delay;
        SceneManager.LoadScene("Level 1");
    }
     IEnumerator EndScene()
    {   
        TransitionAnimation.Play("FadeOut");
        yield return delay;
        SceneManager.LoadScene("End Point"); 
    }
}

