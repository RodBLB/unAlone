using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LifeAndDeath : MonoBehaviour
{
    [SerializeField] private float stressLevel;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        stressLevel += Time.deltaTime * 1 / 3f;
        if (stressLevel >= 100f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamages(float damages)
    {

        stressLevel += damages;
    }
}
