using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
   public int currentHealth;
    public int initialHealth = 4;
    public Animator animator;
    public ParticleSystem effect;

       void Start()
    {
       
    }

    void Update(){
       
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            animator.Play("end");
            Destroy(effect);
          //  GetComponent<Interactable>().TakeDamage(attackDamage);
        }
    }
}
