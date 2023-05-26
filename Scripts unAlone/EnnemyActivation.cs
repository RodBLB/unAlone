using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyActivation : MonoBehaviour
{
    private WildBoarBehaviour boarActivation;
    // Start is called before the first frame update
    private void Awake()
    {
        boarActivation = GetComponentInParent<WildBoarBehaviour>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            boarActivation.ennemyActivation = true;
        }
    }
}
