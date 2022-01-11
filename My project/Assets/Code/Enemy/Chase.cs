using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public float speed = 0.01f;
    Animator anim;

    public Transform player;

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
        if(Vector3.Distance(transform.position,player.position)>attackRange)
        {
            transform.LookAt(player.position);
            transform.position = Vector3.Lerp(this.transform.position, player.position,speed*Time.deltaTime);
            anim.SetBool("Run",true);
        }
        else 
        {
            GetComponent<EnemyController>().isAttacked = true;
            GetComponent<Chase>().enabled = false;
        }
        
        
    }
}
