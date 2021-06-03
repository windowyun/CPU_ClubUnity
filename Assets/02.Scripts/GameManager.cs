using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float rollCoolTime = 0f;
    public float attackCoolTime = 0f;

    public float rollTime;
    public float attackTime;

    void Start()
    {
        rollTime = 0f;
        attackTime = 0f;
    }

    void Update()
    {
        rollTime += Time.deltaTime;
        attackTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z) && attackTime >= attackCoolTime)
            attackTime = 0f;

        if (Input.GetKeyDown(KeyCode.LeftShift) && rollTime >= rollCoolTime)
            rollTime = 0f;

        if (rollTime > 100)
            rollTime = rollCoolTime;

        if (attackTime > 100)
            attackTime = attackCoolTime;

    }
}
