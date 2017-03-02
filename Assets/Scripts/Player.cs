using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public float speed;
    public GameObject weaponObject;

    Animator animator;
    Rigidbody2D rigidBody;
    Collider2D weaponCollider;
    int direction;
    bool facingRight;
    bool grounded;

    // Use this for initialization
    void Start()
    {
        direction = 0;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        weaponCollider = weaponObject.GetComponent<Collider2D>();
        weaponCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            direction = -1;
            facingRight = false;

            animator.SetBool("walkingLeft", true);
            animator.SetBool("walkingRight", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = 1;
            facingRight = true;

            animator.SetBool("walkingRight", true);
            animator.SetBool("walkingLeft", false);
        }
        else
        {
            direction = 0;

            animator.SetBool("walkingRight", false);
            animator.SetBool("walkingLeft", false);
        }

        if (!IsAnimating())
        {
            if (Input.GetKeyDown(KeyCode.E) && !facingRight)
            {
                weaponCollider.transform.localPosition = new Vector2(-.35f, -.045f);
                StartCoroutine(HandleWeaponHitbox());
                animator.SetTrigger("attackLeft");
            }
            else if (Input.GetKeyDown(KeyCode.E) && facingRight)
            {
                weaponCollider.transform.localPosition = new Vector2(.75f, -.045f);
                StartCoroutine(HandleWeaponHitbox());
                animator.SetTrigger("attackRight");
            }
            else if (Input.GetKeyDown(KeyCode.R) && !facingRight)
            {
                animator.SetTrigger("shootLeft");
            }
            else if (Input.GetKeyDown(KeyCode.R) && facingRight)
            {
                animator.SetTrigger("shootRight");
            }
        }
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(new Vector2(direction * (speed / 100), 0), ForceMode2D.Impulse);
    }

    IEnumerator HandleWeaponHitbox()
    {
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(.45f);
        weaponCollider.enabled = false;
    }

    bool IsAnimating()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackRight")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttackLeft")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerShootRight")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerShootLeft"))
            return true;

        return false;
    }
}
