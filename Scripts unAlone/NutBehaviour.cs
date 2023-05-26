using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutBehaviour : MonoBehaviour
{
    public LifeAndDeath lifeAndDeath;
    public float damages;
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private GameObject Player;
    public bool nutDetection = false;
    public bool isDamagedNut;

    void Update()
    {

        if (nutDetection == true)
        {
            transform.Translate(Vector3.down * 10 * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.CompareTag("Player"))
        {
            damages = 5f;
            lifeAndDeath = Col.GetComponent<LifeAndDeath>();
            lifeAndDeath.TakeDamages(damages);
            isDamagedNut = true;
        }

    }

    void OnTriggerExit2D(Collider2D Col)
    {
        isDamagedNut = false;
    }

}