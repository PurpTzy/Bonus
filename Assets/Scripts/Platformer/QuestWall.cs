using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestWall : MonoBehaviour
{
    public int ShroomRequirement;
    public bool isButtonRequired;

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(isButtonRequired == false)
        {
            if(collision.gameObject.tag == "Player")
            {
            CheckRequirements(collision.gameObject);
            }
        }
    
    }
    public void CheckRequirements(GameObject obj)
    {
        if(obj.gameObject.GetComponent<PlayerStats>().Score >= ShroomRequirement) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Need More Shrooms");
        }
    }
     
}
