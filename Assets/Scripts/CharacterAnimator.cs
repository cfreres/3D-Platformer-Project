using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = 0.1f;
    Animator animator;
    CharacterController controller;

    public bool isSwinging = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = controller.velocity.magnitude;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (animator.GetFloat("combatStance") == 0)
            {
                animator.SetFloat("combatStance", 1);
            }
            else
            {
                animator.SetFloat("combatStance", 0);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isSwinging = true;
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(1);
        isSwinging = false;
    }
}
