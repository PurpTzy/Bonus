using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toby : MonoBehaviour
{   
    private Animator anim;
    float currentMovementSpeed;
    private SpriteRenderer playerRenderer;
    private AudioSource ap;
    [SerializeField] AudioClip naDead;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * currentMovementSpeed * horizontalInput * Time.deltaTime);
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Running1");
        }
        if(Mathf.Abs(horizontalInput) > 0)
        {
            anim.SetTrigger("Running");
        }
        else if (Mathf.Abs(horizontalInput) == 0)
        {
            anim.SetTrigger("Idle");
        }
        if(horizontalInput > 0)
        {
            playerRenderer.flipX = true;
        }
        else if (horizontalInput < 0)
        {
            playerRenderer.flipX = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "Restart")
        {
            ap.PlayOneShot(naDead);
        }
    }
}
