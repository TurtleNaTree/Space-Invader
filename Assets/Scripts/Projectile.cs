using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed; // works like a event research System.Action later
    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.destroyed != null){ //needed so it only runs for the player to destroy its projectile
            this.destroyed.Invoke();
        }
       
        Destroy(this.gameObject);
    }
    
}
