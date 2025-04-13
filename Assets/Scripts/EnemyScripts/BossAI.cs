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

    public AudioSource AudioSource;

    // god audio
    public AudioClip godAmbience;
    public AudioClip godSpawnRoar;
    public AudioClip godRoar;
    public AudioClip godRoar2;
    public AudioClip godSwing;
    public AudioClip godSlash1;
    public AudioClip godSlash2;
    public AudioClip godBeam;
    public AudioClip godExplosion;
    public AudioClip godSmash;

    // knight audio
    public AudioClip knightRoar;
    public AudioClip knightSlash1;
    public AudioClip knightSlash2;

    public AudioClip walkSound;

    // raph/mich audio

    public BossType bossType;

    public enum BossType {
        God,
        Michael
    }

    void Awake() { 
        Player = GameObject.Find("Player").transform;
        Enemy = GetComponent<NavMeshAgent>();
    }

    void Start() {
        AudioSource = GetComponentInChildren<AudioSource>();
        AudioSource.spatialBlend = 1f;
        AudioSource.minDistance = 1f;
        AudioSource.maxDistance = 50f;
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

        if (!AudioSource.isPlaying && bossType == BossType.Michael)  {
            AudioSource.clip = walkSound;
            AudioSource.loop = true;
            AudioSource.volume = 0.8f;
            AudioSource.Play();
        }
    }

    private void Running() {
        Enemy.speed = 7f;
        Enemy.acceleration = 12f;
        Enemy.SetDestination(Player.position);

        if (!AudioSource.isPlaying && bossType == BossType.Michael) {
            AudioSource.clip = walkSound;
            AudioSource.loop = true;
            AudioSource.volume = 1f;
            AudioSource.Play();
        }
    }

    private void AttackPlayer() {
        Enemy.SetDestination(transform.position);
        transform.LookAt(Player);

        if (AudioSource.isPlaying && AudioSource.clip == walkSound) {
            AudioSource.Stop();
        }

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
            if (bossType == BossType.God) {
                AudioSource.PlayOneShot(godSlash1, 1.4f);
                AudioSource.PlayOneShot(godSwing, 1.4f);
            }
            if (bossType == BossType.Michael) {
                AudioSource.PlayOneShot(knightSlash1, 1.3f);
                AudioSource.PlayOneShot(knightRoar, 1.3f); 
            }
            DodgeAttackOne.SetActive(true);

            yield return new WaitForSeconds(AttackDuration);
            DodgeAttackOne.SetActive(false);
        }
        if (AttackType == 1) {
            ParryIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            ParryIndicator.SetActive(false);

            if (bossType == BossType.God) {
                AudioSource.PlayOneShot(godSmash, 1.5f);
                AudioSource.PlayOneShot(godRoar2, 1.6f);
            }

            if (bossType == BossType.Michael) {
                AudioSource.PlayOneShot(knightSlash2, 1.4f);
                AudioSource.PlayOneShot(knightRoar, 1.4f); 
            }

            ParryAttackOne.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            ParryAttackOne.SetActive(false);
        }
        if (AttackType == 2) {
            DodgeIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            DodgeIndicator.SetActive(false);

            if (bossType == BossType.God) {
                AudioSource.PlayOneShot(godSlash2, 1.4f);
                AudioSource.PlayOneShot(godSwing, 1.4f);
                AudioSource.PlayOneShot(godRoar, 1.4f);
            }

            DodgeAttackTwo.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            DodgeAttackTwo.SetActive(false);
        }
        if (AttackType == 3) {
            RunIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            RunIndicator.SetActive(false);

            if (bossType == BossType.God) {
                AudioSource.PlayOneShot(godExplosion, 1.8f);
                AudioSource.PlayOneShot(godRoar2, 1.6f);
            }

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