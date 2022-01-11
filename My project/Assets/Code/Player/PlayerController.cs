using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public bool isAttacked = false;

    public float heal = 100f;
    public bool getHit = false;

    GameObject lost;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lost = GameObject.FindGameObjectWithTag("Lost");
        lost.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttacked)
        {
            isAttack(true);
        }
        
    }
    void isAttack(bool CanAttack)
    {
        anim.SetBool("Attack",CanAttack);
    }
    void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.name == "EnemyHand") 
         {
             heal -=100;
         }
         if(heal <= 0)
         {
            anim.SetBool("Die",true);
            StartCoroutine(waitfor());
         }
    }
     public IEnumerator waitfor()
    {
        yield return new WaitForSeconds(5);
        lost.SetActive(true);
    }
}

