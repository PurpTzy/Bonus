 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15;
    private Rigidbody2D rb;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
       
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Obstacle")
        {
            Destroy(c.gameObject);
            Destroy(this.gameObject);
        }
        if(c.gameObject.tag == "Droid")
        {
         Destroy(this.gameObject); 
        }
    }
}
