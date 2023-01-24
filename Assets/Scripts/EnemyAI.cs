using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float distance;
    public bool isPatrolling;
    private bool movingRight = true;
    public Transform groundDetection;
    public Transform playerDetection;
    public Transform attackPos;
    public Transform searchPos;
    public Rigidbody2D rb;
    public Animator animator;
    public Rigidbody2D playerRB;


    public bool isFlipped = true;
    public float attackRange;
    public float distanceAttackRange;
    public float searchRange;
    public LayerMask attackMask;
    public int attackDamage = 1;
    //Attack Timing
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    // Effect
    public GameObject enemyEffect;
    public Transform enemyEffectPos;

    //Sound
    public AudioSource hurtSound;
    public AudioSource implodeSound;

    //Health
    public int currentHealth;
    public int initialHealth = 4;


    void Start()
    {
        isPatrolling = true;
        currentHealth = initialHealth ;
    }

    // Update is called once per frame
    void Update()
    {

        FindPlayer();
        if(isPatrolling){
            enemyPatrol();  
        }else
        {
            //Attack();
        }

          
    }

//Enemy Patrolling
    void enemyPatrol(){
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if(groundInfo.collider == false){
            if (movingRight == true){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                isFlipped = false;
            }else {
                transform.eulerAngles = new Vector3(0,0,0);
                movingRight = true;
                isFlipped = true;
            }
      } 
    }

   
//Enemy Find

void FindPlayer()
{
  Collider2D colPlayerInfo = Physics2D.OverlapCircle(searchPos.position, searchRange, attackMask);
    if(colPlayerInfo != null && movingRight == true ){

        isPatrolling = false;
         LookAtPlayer();
        
        Vector2 target = new Vector2(playerDetection.position.x,rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if(Vector2.Distance(playerDetection.position, rb.position) <= distanceAttackRange){
            if(timeBtwAttack <=0){
                Debug.Log("Something should happen");
            animator.SetTrigger("Attack");
            timeBtwAttack = startTimeBtwAttack;
            }else{
                timeBtwAttack -= Time.deltaTime;
            }
            
           
        }

        }else if(colPlayerInfo != null && movingRight == false ){

        isPatrolling = false;
         LookAtPlayer();
        
        Vector2 target = new Vector2(playerDetection.position.x,rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if(Vector2.Distance(playerDetection.position, rb.position) <= distanceAttackRange){
            if(timeBtwAttack <=0){
                Debug.Log("Something should happen");
            animator.SetTrigger("Attack");
            timeBtwAttack = startTimeBtwAttack;
            }else{
                timeBtwAttack -= Time.deltaTime;
            }
            
           
        }

        }else{
            isPatrolling = true;
        }
    
}

//Enemy Attack
    void Attack(){     
        Collider2D colInfo = Physics2D.OverlapCircle(attackPos.position, attackRange, attackMask);
        Debug.Log("Player hurt");

       
        if(colInfo != null){
           colInfo.GetComponent<PlayerScript>().TakeDamage(attackDamage);
           if(isFlipped==true){
               playerRB.AddForce(new Vector2(2000, 200)); 
           }else if(isFlipped==false){
               playerRB.AddForce(new Vector2(-2000, 200)); 
           }
            //colInfo.GetComponent<PlayerScript>().Knockback();
        }
    }

//Look At Player
  public void LookAtPlayer(){
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > playerDetection.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        } else if(transform.position.x < playerDetection.position.x && !isFlipped){
transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = true;
        }

    }

     private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireSphere(searchPos.position, searchRange);
        Gizmos.DrawWireSphere(searchPos.position, distanceAttackRange);
    }
    
     private void OnCollisionEnter2D(Collision2D other) {
         if(isPatrolling){
             if(other.gameObject.tag == "Enemy"){
          if (movingRight == true){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }else {
                transform.eulerAngles = new Vector3(0,0,0);
                movingRight = true;
            }
        }
         }
        
    }

//Enemy Taking Damage

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

         GameObject effect = Instantiate(enemyEffect, enemyEffectPos.position, Quaternion.identity);
         Destroy(effect, 3f);

         if(isFlipped==false){
               rb.AddForce(new Vector2(2000, 200)); 
           }else if(isFlipped==true){
               rb.AddForce(new Vector2(-2000, 200)); 
           }
         animator.Play("enemyhit");
        if (currentHealth < 0)
        {
            // animator.Play("GolemBossDying");
            //implodeSound.Play();
           
            Destroy(gameObject);
           
        }
    }
    public void HurtSound(){
        hurtSound.Play();
    }
}
