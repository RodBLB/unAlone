using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarBehaviour : MonoBehaviour
{


    public LifeAndDeath lifeAndDeath;
    public float damages;

    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private GameObject Player;
    public bool ennemyActivation = false;
    public bool isDamagedBoar;

    void Start()
    {
    }

    void Update()
    {

        if (ennemyActivation == true)
        {
            transform.Translate(Vector3.left * 8 * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.CompareTag("Player"))
        {
            damages = 15f;
            lifeAndDeath = Col.GetComponent<LifeAndDeath>();
            lifeAndDeath.TakeDamages(damages);
            isDamagedBoar = true;
            Destroy(GetComponent<BoxCollider>());
        }

    }

    void OnTriggerExit2D(Collider2D Col)
    {
        if (Col.CompareTag("Player"))
        {
            isDamagedBoar = false;
        }
    }

}
