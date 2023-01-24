using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preEnd : MonoBehaviour
{
    
    public GameObject player;
    public GameObject notPassedYet;
    public GameObject [] EnemyZombie ;
    
     void Start() {
      EnemyZombie = GameObject.FindGameObjectsWithTag("EnemyZombie");
   }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Player"){
            if(EnemyZombie.Length - 1 >= 0 ){
                notPassedYet.SetActive(true);
                 Time.timeScale = 0f;
               // notPassedYet.SetActive(false);
            }
             if(EnemyZombie.Length - 1 <= 0 ){
                
               Destroy(gameObject);
            }
        }
    }

    public void Resume(){
         notPassedYet.SetActive(false);
          Time.timeScale = 1f;
    }
}
