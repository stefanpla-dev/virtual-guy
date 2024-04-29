using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwap : MonoBehaviour
{
    private Animator anim;
    public bool upsideDown;
    [SerializeField] private AudioClip gravitySwap;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SoundManager.instance.PlaySound(gravitySwap);
            Physics2D.gravity = new Vector2(0f,9.81f);
            transform.eulerAngles = new Vector3(180,0,0);
            anim.SetTrigger("jump");
            upsideDown = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.instance.PlaySound(gravitySwap);
            Physics2D.gravity = new Vector2(0f,-9.81f);
            transform.eulerAngles = new Vector3(0,0,0);
            anim.SetTrigger("jump");
            upsideDown = false;
        }
    }
}
