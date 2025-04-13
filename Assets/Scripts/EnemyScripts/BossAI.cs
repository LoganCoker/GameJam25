using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour {
    public Blocker ExitBlocker;
    public HealthSystem BossHealth;
    public NavMeshAgent Enemy;
    public Transform Player;
    public LayerMask WhatIsPlayer;
    public float TimeBetweenAttacks, AttackDuration, IndicatorTimer, WalkRange, AttackRange;
    public bool AlreadyAttacked, PlayerInWalkRange, PlayerInAttackRange;
    public GameObject ParryIndicator, DodgeIndicator, RunIndicator, Boss;
    // attacks
    public int NumberOfAttacks;
    public GameObject DodgeAttackOne, ParryAttackOne, DodgeAttackTwo, ParryAttackTwo, RunAttack;
    public float Timer = 15f;
    void Awake() { 
        Player = GameObject.Find("Player").transform;
        Enemy = GetComponent<NavMeshAgent>();
    }

    void Update() {
        PlayerInWalkRange = Physics.CheckSphere(transform.position, WalkRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);
        Timer -= Time.deltaTime;
        
        if (BossHealth.GetHealth() <= 0) { 
            Boss.SetActive(false);
            ExitBlocker.IncreaseCounter();

        }
        if (!PlayerInAttackRange && Timer <= 0) { RangedAttack(); }
        if (PlayerInAttackRange) { AttackPlayer(); } 
        else if (PlayerInWalkRange) { Walking(); } 
        else { Running(); }
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

    private void RangedAttack() {
        Enemy.SetDestination(transform.position);
        transform.LookAt(Player);

        if (!AlreadyAttacked && ParryAttackTwo != null) {
            StartCoroutine(RangedMove());
        }
    }

    public void TakeDamage(int Damage) {
        BossHealth.LoseHealth(Damage);
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
            RunIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            RunIndicator.SetActive(false);
            RunAttack.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            RunAttack.SetActive(false);
        }
        yield return new WaitForSeconds(TimeBetweenAttacks);
        AlreadyAttacked = false;
    }
    IEnumerator RangedMove() {
        AlreadyAttacked = true;
        ParryIndicator.SetActive(true);
        yield return new WaitForSeconds(IndicatorTimer);
        ParryIndicator.SetActive(false);
        ParryAttackTwo.SetActive(true);
        yield return new WaitForSeconds(AttackDuration);
        ParryAttackTwo.SetActive(false);
        yield return new WaitForSeconds(TimeBetweenAttacks);
        AlreadyAttacked = false;
        Timer = 15f;
    }
}