using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutDetection : MonoBehaviour
{
    private NutBehaviour nutBehaviour;
    // Start is called before the first frame update
    private void Awake()
    {
        nutBehaviour = GetComponentInParent<NutBehaviour>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            nutBehaviour.nutDetection = true;
        }
    }
}
