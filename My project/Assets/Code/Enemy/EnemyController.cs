using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator anim;
    public bool isAttacked = false;

    public float health = 100f;

    GameObject win;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        win = GameObject.FindGameObjectWithTag("Win");
        win.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttacked)
        {
            isAttack(true);
        }
    }
    public void isAttack(bool CanAttack)
    {
        anim.SetBool("Attack",CanAttack);
    }
    void OnTriggerEnter(Collider other)
    {
    
        if(other.gameObject.name == "PlayerHand")
        {
            health -= 100;
        }
        if(health <= 0)
        {
            anim.SetBool("Die",true);
            StartCoroutine(waitfor());
        }
        
    }
     public IEnumerator waitfor()
    {
        yield return new WaitForSeconds(5);
        win.SetActive(true);
    }
}
