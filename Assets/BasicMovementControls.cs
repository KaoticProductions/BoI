using UnityEngine;
using System.Collections;

public class BasicMovementControls : MonoBehaviour {

    public float maxSpeed = 5f;
    public float facing = 0.0f;
    
    private Rigidbody2D rbody;
    private Animator anim;
    private Animator[] child_anims;

    private bool attacking = false;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        child_anims = GetComponentsInChildren<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        foreach (Animator child_anim in child_anims)
        {
            child_anim.SetFloat("Input_X", 0f);
            child_anim.SetFloat("Input_Y", -1f);
        }
    }

    public void AttackAnimationEnded()
    {
        attacking = false;
        foreach (Animator child_anim in child_anims)
        {
            child_anim.SetBool("Attacking", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        bool queueAttack = Input.GetAxisRaw("Fire1") != 0;
        bool moving = movement != Vector2.zero;

        if (!attacking && queueAttack)
        {
            attacking = true;
        }

        if (moving)
        {
            // Set Anim State in children
            foreach (Animator child_anim in child_anims)
            {
                child_anim.SetFloat("Input_X", movement.x);
                child_anim.SetFloat("Input_Y", movement.y);
            }

            rbody.MovePosition(rbody.position + (movement * maxSpeed * Time.deltaTime));
        }

        // Set Anim Flags in children
        anim.SetBool("Moving", moving);
        anim.SetBool("Attacking", attacking);
        foreach (Animator child_anim in child_anims)
        {
            child_anim.SetBool("Moving", moving);
            child_anim.SetBool("Attacking", attacking);
        }
    }
}
