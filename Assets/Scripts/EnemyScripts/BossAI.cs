using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour {
    public NavMeshAgent Enemy;
    public Transform Player;
    public LayerMask WhatIsPlayer;
    public float TimeBetweenAttacks, AttackDuration, IndicatorTimer, WalkRange, AttackRange;
    public bool AlreadyAttacked, PlayerInWalkRange, PlayerInAttackRange;
    public GameObject ParryIndicator, DodgeIndicator;
    // attacks
    public GameObject DodgeAttackOne, ParryAttackOne, DodgeAttackTwo, ParryAttackTwo, AttackFive;
    public int NumberOfAttacks;
  

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
        int AttackType = Random.Range(0, NumberOfAttacks);
        AlreadyAttacked = true;

        if (AttackType == 0) {
            DodgeIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            DodgeIndicator.SetActive(false);
            DodgeAttackOne.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            DodgeAttackOne.SetActive(false);
        }
        if (AttackType == 1) {
            ParryIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            ParryIndicator.SetActive(false);
            ParryAttackOne.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            ParryAttackOne.SetActive(false);
        }
        if (AttackType == 2) {
            DodgeIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            DodgeIndicator.SetActive(false);
            DodgeAttackTwo.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            DodgeAttackTwo.SetActive(false);
        }
        if (AttackType == 3) {
            ParryIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            ParryIndicator.SetActive(false);
            ParryAttackTwo.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            ParryAttackTwo.SetActive(false);
        }
        if (AttackType == 4) {
            ParryIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            ParryIndicator.SetActive(false);
            AttackFive.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            AttackFive.SetActive(false);
        }
        yield return new WaitForSeconds(TimeBetweenAttacks);
        AlreadyAttacked = false;
    }
}