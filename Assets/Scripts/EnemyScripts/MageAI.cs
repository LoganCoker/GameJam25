using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class MageAI : MonoBehaviour {
    public Blocker ExitBlocker;
    public HealthSystem BossHealth;
    public NavMeshAgent Enemy;
    public Transform Player;
    public LayerMask WhatIsPlayer;
    public float TimeBetweenAttacks, AttackDuration, IndicatorTimer, WalkRange, AttackRange;
    public bool AlreadyAttacked, PlayerInWalkRange, PlayerInAttackRange;
    public GameObject ParryIndicator, DodgeIndicator, Boss, Beam, FireBall, FireBallSpawn, Mage;
    public Animator contr;   

    void Awake() { 
        Player = GameObject.Find("Player").transform;
        Enemy = GetComponent<NavMeshAgent>();
    }

    void Update() {
        PlayerInWalkRange = Physics.CheckSphere(transform.position, WalkRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);
        contr.SetBool("walking", true);
        contr.SetBool("attack", false);


        if (BossHealth.GetHealth() <= 0) { 
            Boss.SetActive(false);
            ExitBlocker.IncreaseCounter();
        }
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
        contr.SetBool("walking", false);
        contr.SetBool("attack", true);
        Enemy.SetDestination(transform.position);
        transform.LookAt(Player);

        if (!AlreadyAttacked) {
            StartCoroutine(Attacking());
        }
    }

    public void TakeDamage(int Damage) {
        BossHealth.LoseHealth(Damage);
    }

    IEnumerator Attacking() {
        int AttackType = Random.Range(0, 2);
        AlreadyAttacked = true;

        if (AttackType == 0) {
            DodgeIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            DodgeIndicator.SetActive(false);
            Beam.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            Beam.SetActive(false);
        }
        if (AttackType == 1) {
            ParryIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            ParryIndicator.SetActive(false);
            GameObject FB = Instantiate(FireBall, FireBallSpawn.transform.position, FireBallSpawn.transform.rotation);
            FireBall fbScript = FB.GetComponent<FireBall>();
            if (fbScript != null) {
                fbScript.Boss = this;
            }
            Destroy(FB, 10);
        }
        yield return new WaitForSeconds(TimeBetweenAttacks);
        AlreadyAttacked = false;
    }
}   