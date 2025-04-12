using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnightAI : MonoBehaviour {
    public NavMeshAgent Enemy;
    public Transform Player;
    public LayerMask WhatIsGround, WhatIsPlayer;
    public float TimeBetweenAttacks;
    public float AttackDuration;
    public bool AlreadyAttacked;
    public GameObject SideSlash;
    public GameObject OverheadSlash;
    public float WalkRange, AttackRange;
    public bool PlayerInWalkRange, PlayerInAttackRange;
    void Awake() { 
        Player = GameObject.Find("Player").transform;
        Enemy = GetComponent<NavMeshAgent>();
    }

    void Update() {
        PlayerInWalkRange = Physics.CheckSphere(transform.position, WalkRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);
        
        if (PlayerInWalkRange) { Walking(); }
        if (!PlayerInWalkRange) { Running(); }
        if (PlayerInAttackRange) { AttackPlayer(); }
    }

    private void Walking() {
        Enemy.speed = 3.5f;
        Enemy.acceleration = 8f;
        Enemy.SetDestination(Player.position);
    }

    private void Running() {
        Enemy.speed = 7f;
        Enemy.acceleration = 12f;
        Enemy.SetDestination(Player.position);
    }

    private void AttackPlayer() {
        Enemy.SetDestination(transform.position);
        transform.LookAt(Player);

        if (!AlreadyAttacked) {
            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking() {
        int AttackType = Random.Range(0, 2);
        AlreadyAttacked = true;
        
        if (AttackType == 0) {
            SideSlash.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            SideSlash.SetActive(false);
        }
        else {
            OverheadSlash.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            OverheadSlash.SetActive(false);
        }
        yield return new WaitForSeconds(TimeBetweenAttacks);
        AlreadyAttacked = false;
    }
}