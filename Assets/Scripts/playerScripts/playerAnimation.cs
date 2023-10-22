using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    public Animator animator;
    public playerController playerController;
    int horizontalParameter;
    int verticalParameter;
     
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<playerController>();
    }
    public void Awake()
    {
        horizontalParameter = Animator.StringToHash("horizontal");
        verticalParameter = Animator.StringToHash("vertical");
    }
    // Update is called once per frame
    public void updateAnimValue(float horMove, float verMove)
    {
        if (playerController.SprintAction)
        {
            horMove = 1.5f;
            verMove = 1.5f;
        }

        animator.SetFloat(horizontalParameter, horMove);
        animator.SetFloat(verticalParameter, verMove);
    }

    public void startShootAnimation()
    {
        animator.SetBool("shooting", true);
    }
    public void stopShootAnimation()
    {
        animator.SetBool("shooting", false);
    }
}
