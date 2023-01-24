using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiPatrol : MonoBehaviour
{
    public float walkSpeed;
    public bool mustPatrol;
    private bool mustTurn;
    public Transform groundDetection;
    public LayerMask groundLayer;
    public Rigidbody2D rb;

    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(mustPatrol)
        {
            Patrol();
        }
    }

     private void FixedUpdate() 
     {
         if(mustPatrol)
         {
             mustTurn = !Physics2D.OverlapCircle(groundDetection.position, .1f, groundLayer);
         }
    }

    void Patrol()
    {
        if(mustTurn)
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime,rb.velocity.y);

       // RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
       // if(groundInfo.collider == false){
       //     Flip();
       // }    
    }


    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}
