using UnityEngine;
using System.Collections;

public class Fighter : Unit {

    public ParticleSystem ps;
    public AudioSource powerUp;
    public GameObject sword;
    private float buffTimer = 5f;
    private float idleTimer = 7f;
    // Use this for initialization
    public override void Start () {
        base.Start();

        unitName = "Fighter";
        print(unitName + " spawned here.");
        GetComponent<Animator>().Play("Idle");
    }

    public void Idle()
    {
        animator.SetBool("Idle", true);
    }

    public void Standing()
    {
        animator.SetBool("Standing", true);
    }

    public override void StartWalking()
    {
        animator.SetBool("StartWalking", true);
    }

    public override void StartAttacking()
    {
        animator.SetBool("Attack", true);
    }

    public void TakeDamage()
    {
        animator.SetBool("Damage", true);
    }

    public void GetHit()
    {
        TakeDamage();
    }

    public void Death()
    {
        animator.SetBool("Death", true);
    }

    public override void Update()
    {
        base.Update();
        //ps.transform.position = sword.transform.position;

        idleTimer -= Time.deltaTime;
        /*
        if(idleTimer < 0f && !this.IsMoving() && !this.IsIdling())
        {
            Idle();
            print("start idle");
            idleTimer = 7f;
        }
        */
        if (attackPowerSpecial == 0)
        {
            attackPowerSpecial += (int)Time.deltaTime;
        }
    }

    public override void ActivateSpecial()
    {
        //Death();
        powerUp.Play();
        animator.SetBool("Special", true);
        ps.Play();
        if (CanAttack(currentTarget))
        {
            attackPowerSpecial = 0;
            while(buffTimer <= 5)
            {
                attackPowerSpecial = 7; // Have a beefeing up enrage timer for five seconds
                print(buffTimer);
            }
            attackPowerSpecial = 0;
        }
        print("Beefing up!");


        //Attack(base.currentTarget);
        //GetHit();

    }
}
