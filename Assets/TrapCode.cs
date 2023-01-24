using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCode : MonoBehaviour
{
    public int attackDamage = 4;
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
           player.GetComponent<PlayerScript>().TakeDamage(attackDamage);
        }
    }
}
