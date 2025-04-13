using System.Collections;
using System.Collections.Generic;
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

    public AudioSource AudioSource;

    public AudioClip beam;
    public AudioClip fireball;

    public AudioClip walkSound;
    private Animator animator;

    void Awake() {
        Player = GameObject.Find("Player").transform;
        Enemy = GetComponent<NavMeshAgent>();
    }
    void Start() {
        AudioSource = GetComponentInChildren<AudioSource>();
        animator = GetComponent<Animator>();
        AudioSource.spatialBlend = 1f;
        AudioSource.minDistance = 1f;
        AudioSource.maxDistance = 50f;
    }

    void Update() {
        PlayerInWalkRange = Physics.CheckSphere(transform.position, WalkRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);


        if (BossHealth.GetHealth() <= 0) {
            Boss.SetActive(false);
            ExitBlocker.IncreaseCounter();
        }
        if (PlayerInAttackRange) { AttackPlayer(); } else if (PlayerInWalkRange) { Walking(); } else { Running(); }
    }

    private void Walking() {
        Enemy.speed = 3.5f;
        Enemy.acceleration = 8f;
        Enemy.SetDestination(Player.position);

        animator.SetBool("walking", true);

        if (!AudioSource.isPlaying) {
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

        animator.SetBool("walking", true);

        if (!AudioSource.isPlaying) {
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

        animator.SetBool("walking", false);
        animator.SetTrigger("attack");

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

            AudioSource.PlayOneShot(beam, 1.2f);

            Beam.SetActive(true);
            yield return new WaitForSeconds(AttackDuration);
            Beam.SetActive(false);
        }
        if (AttackType == 1) {
            ParryIndicator.SetActive(true);
            yield return new WaitForSeconds(IndicatorTimer);
            ParryIndicator.SetActive(false);

            AudioSource.PlayOneShot(fireball, 1.2f);

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