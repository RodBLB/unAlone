using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{


    public LifeAndDeath lifeAndDeath;
    public float damages;

    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private GameObject Player;
    public bool isDamaged;
    public Animator bushAnimator;

    // Start is called before the first frame update
    void Start()
    {
        bushAnimator.SetBool("isTriggered", false);
        isDamaged = false;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.CompareTag("Player"))
        {
            
            bushAnimator.SetBool("isTriggered", true);
            damages = 10f;
            lifeAndDeath = Col.GetComponent<LifeAndDeath>();
            lifeAndDeath.TakeDamages(damages);
            isDamaged = true;
        }
    }

    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.CompareTag("Player"))
        {
            isDamaged = false;
            bushAnimator.SetBool("isTriggered", false);
        }
    }

}
