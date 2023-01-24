using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    public Animator CharacterAnimator;
    public float moveSpeed = 5;
    public float jumpHeight;
    public Rigidbody2D myRB;
    public bool facingRight;
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    //public GameObject arrow;
    public bool isAttacking = false;
    public static PlayerScript instance;

    //Player Effect
    public GameObject playerEffect;
    public Transform playerEffectPos;

   //Attacking
    public float attackRange;
    public Transform attackPos;
    public LayerMask attackMask;
    public LayerMask volcanoeMask;
    public int attackDamage = 1;

    //Game Sound

    public AudioSource grassWalk;
    public AudioSource slashSound;
    public AudioSource thud;
    public AudioSource hurtSound;
    
    //HealthBar Code
     public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    //GameOver
    public GameObject gameOverUI;
    public float timeLeftForUI = 2.0f;
    public bool isDead = false;


//

    
    void Awake(){
         instance = this;
    }

    void Start()
    {
        facingRight = true;
         //playerHealth = initialHealth;
          health = numOfHearts;
          
    }

    void Update(){
        
        if(isDead == true){
            Dead();
        }
        Attack();
        if(grounded && Input.GetAxis("Vertical")>0){
            grounded = false;
            CharacterAnimator.SetBool("isGrounded",grounded);
            myRB.AddForce(new Vector2(0, jumpHeight));
        }

        /*if(Input.GetAxis("Fire1")>0){
            CharacterAnimator.Play("attack1");
        }
        
        if(Input.GetAxis("Fire2")>0){
            CharacterAnimator.Play("attack2");
        }*/

        
    }

    void Attack(){
        if(Input.GetKeyDown(KeyCode.Space) && !isAttacking ){
           
            
            isAttacking = true;

            Collider2D colInfo = Physics2D.OverlapCircle(attackPos.position,attackRange,attackMask);
           // Collider2D volcanoeInfo = Physics2D.OverlapCircle(attackPos.position,attackRange,volcanoeMask);
                if(colInfo !=null){
                    colInfo.GetComponent<EnemyAI>().TakeDamage(attackDamage);
                    
                }
             
        }
    } 

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);
        CharacterAnimator.SetBool("isGrounded", grounded);
        CharacterAnimator.SetFloat("verticalSpeed",myRB.velocity.y);
        
        float move = Input.GetAxis("Horizontal");
        CharacterAnimator.SetFloat("speed",Mathf.Abs(move));
        myRB.velocity = new Vector2(move*moveSpeed, myRB.velocity.y);
       
        if(move>0 && !facingRight){
            flip();
        }else if(move<0 && facingRight){
            flip();
        }
    }
    void flip(){
        facingRight =! facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

     public void TakeDamage(int damage)
    {
        CharacterAnimator.Play("hit");
        //Knockback();
         GameObject effect = Instantiate(playerEffect, playerEffectPos.position, Quaternion.identity);
         Destroy(effect, 3f);
        health -= damage;

         if(health > numOfHearts){
              health = numOfHearts;
          }
        for(int i = 0; i < hearts.Length; i++){
            if(i < health){
                hearts[i].sprite = fullHeart;
            }else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i< numOfHearts){
                hearts[i].enabled = true;
            }else{
                hearts[i].enabled = false;
            }
        }

        //healthbar.SetHealth(playerHealth);
 
        if (health <= 0)
        {
            Debug.Log("Player Dead");
           Dead();
           isDead=true;
          
           // GameObject effect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
     // Destroy(effect, 3f);
         //   Destroy(gameObject);
           // SceneManager.LoadScene("GameOverScene");
        }
    }

    public void Dead(){
         CharacterAnimator.Play("die");

            timeLeftForUI -= Time.deltaTime;

            if(timeLeftForUI<0){
                Time.timeScale = 0f;
                gameOverUI.SetActive(true);
            }
    }

    //Knockback

    public void Knockback(){
        if(facingRight==true){
            myRB.AddForce(new Vector2(2000, 200)); 
        }else{
             myRB.AddForce(new Vector2(-2000, 200)); 
        }
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
   
    }

    public void SoundPlay(){
        slashSound.Play();
    }

    public void FootStep(){
        grassWalk.Play();
    }

     public void Land(){
        thud.Play();
    }

    public void HurtSOund(){
        hurtSound.Play();
    }

   
}
