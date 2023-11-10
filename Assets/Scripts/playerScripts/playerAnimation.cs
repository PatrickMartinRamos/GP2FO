using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    public Animator animator;
    public playerController playerController;
    buttonManager buttonManager;
    int horizontalParameter;
    int verticalParameter;

     
    void Start()
    {
        buttonManager = FindAnyObjectByType<buttonManager>();
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
        if(playerController.walkAction)
        {
            horMove = .5f;
            verMove = .5f;
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
    public void reloadAnimation()
    {
        //reload animation here
        Debug.Log("is reloading");
    }
}
