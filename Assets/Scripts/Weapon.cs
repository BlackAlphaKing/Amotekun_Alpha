using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    public Transform arrow;
    public Transform firepoint;
    public PlayerScript ps;
   // public GameObject arrow;
   public bool isFlipped = true;
   public GameObject projectile;
   public Vector2 aimDirection;
    private float timeBtwShots;
    public float startTimeBtwShots;
    
    
    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) ;
         aimDirection= difference - arrow.position;
  

 if(ps.facingRight == true){
     arrow.right = aimDirection;
 }else if(ps.facingRight == false){
     arrow.right = -aimDirection;
 }
    if(timeBtwShots <= 0 ){
        if(Input.GetMouseButtonDown(0)){
        Instantiate(projectile,arrow.position,transform.rotation);
        timeBtwShots = startTimeBtwShots;
    }
    }else{
        timeBtwShots -= Time.deltaTime;
    }
        
    }
  
}

