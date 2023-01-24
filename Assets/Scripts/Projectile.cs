using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public PlayerScript ps;
    bool proFlip;
    //public LayerMask whatIsSolid;
    //public GameObject destroyEffect;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        bool proFlip = true;
    }

    // Update is called once per frame
    void Update()
    {
       /* RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,transform.right, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Enemy")){
                Debug.Log("Enemy Must Take Damage!");
            }
            DestroyProjectile();
        }*/
      if(ps.facingRight == proFlip){
    transform.Translate(Vector2.right * speed * Time.deltaTime);
 }else if(ps.facingRight == !proFlip){
    transform.Translate(-Vector2.left * speed * Time.deltaTime);
 }
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyProjectile(){
       // Instantiate(destroyEffect, transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
