using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volcanoe : MonoBehaviour
{

    public int currentHealth;
    public int initialHealth = 4;
    public Animator animator;

       void Start()
    {
        currentHealth = initialHealth ;
    }

    void Update(){
       
    }

     public void Damage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Volcano should get hurt");
         //animator.Play("hit");
        if (currentHealth < 0)
        {
            // animator.Play("GolemBossDying");
            //implodeSound.Play();
            Destroy(gameObject);
           
        }
    }
}
