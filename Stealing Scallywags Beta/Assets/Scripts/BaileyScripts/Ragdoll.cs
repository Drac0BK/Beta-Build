using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ragdoll : MonoBehaviour
{

    private Animator animator = null;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    void Start()
    {
        animator = GetComponent<Animator>();

        foreach (Rigidbody r in rigidbodies)
            r.isKinematic = true;
    }

    public bool RagdollOn
    {
        get { return !animator.enabled; }
        set
        {
            animator.enabled = !value;
            foreach (Rigidbody r in rigidbodies)
                r.isKinematic = !value;
        }
    }
}