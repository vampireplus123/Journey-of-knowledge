using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 0.01f;
    Animator anim;

    public Transform enemy;

    public float attackRange = 1f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if(Vector3.Distance(transform.position,enemy.position)>attackRange)
        {
            transform.LookAt(enemy.position);
            transform.position = Vector3.Lerp(this.transform.position, enemy.position,speed*Time.deltaTime);
            anim.SetBool("Run",true);
        }
        else 
        {
            GetComponent<PlayerController>().isAttacked = true;
            GetComponent<Movement>().enabled = false;
        }
        
        
    }
}
