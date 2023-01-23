using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float HP;
    float maxHP;
    public Image HPBarFill;
    
    private void Awake()
    {
        maxHP = HP; 
    }
   
    public void Health(int amount)
    {
        HP += amount;
        HPBarFill.fillAmount = HP / maxHP;
    } 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Bullet"))
        {
            Health(-1);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag.Equals("Boss Droid Bullet"))
        {
            Health(-10);
            Destroy(collision.gameObject);
        }

    }
}
