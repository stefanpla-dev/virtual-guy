using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnimation : MonoBehaviour
{
    private Animator anim;
    private bool waving = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Activated");
            StartCoroutine(WavingFlag());
        }
    }
    IEnumerator WavingFlag()
    {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Waving", waving);
    }

}
