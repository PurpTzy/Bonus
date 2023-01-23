using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject Bullet;
    public Transform SpawnPoint1;
    public float BulletForce;
    private AudioSource audioPlayer;
    [SerializeField] private AudioClip RobotDead;
    [SerializeField] private AudioClip ShootSound;

    public float AttackTimer;
    private float maxTimer;
    bool IsDead = false;

    Animator anim;

    private void Awake()
    {
        maxTimer = AttackTimer;
        anim = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(PlayerTransform.position.x > transform.position.x)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f, transform.localEulerAngles.z);
        }
        else 
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0 , transform.localEulerAngles.z);
        }
        if(AttackTimer > 0)
        {
            AttackTimer -= Time.deltaTime;
        }
        else 
        {
            AttackTimer = maxTimer;

            anim.SetTrigger("Shoot");
        }
        if(!IsInvoking("Shoot") && !IsDead)
        { 

            InvokeRepeating("Shoot", 1, 1);
        }
    
    }
    public void Shoot()
    {   
        GameObject bullet = Instantiate(Bullet, SpawnPoint1.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * BulletForce, ForceMode2D.Impulse);
        anim.SetTrigger("Shoot");
        audioPlayer.PlayOneShot(ShootSound);
        Destroy(bullet, 3);
    }
    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "Player Bullet")
        {   
            IsDead = true;
            CancelInvoke("Shoot");
            anim.SetTrigger("Death");
            audioPlayer.PlayOneShot(RobotDead);
            Destroy(gameObject, 1);
        }
    }
}
