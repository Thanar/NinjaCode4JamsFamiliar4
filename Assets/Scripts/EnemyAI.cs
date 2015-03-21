using UnityEngine;
using System.Collections;

public class EnemyAI : Character {

    public PlayerController player;

    public Weapon weapon;
    public bool hasWeapon = false;

    public Transform weaponTransform;


    Vector3 newRotation = new Vector3(0, 0, 0);
    Vector3 player2DDirection = new Vector3(0,0,0);

    Vector3 auxVector = new Vector3(0, 0, 0);


    public void Push(Vector3 direction)
    {
        pushForce = direction + direction.normalized * maxSpeed;
    }

	void Start () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        if (weapon != null)
        {
            TakeWeapon(weapon);
        }
	}

    void Update()
    {
        if (player != null)
        {
            newRotation.y = Quaternion.LookRotation(player.transform.position - transform.position).eulerAngles.y;
            transform.eulerAngles = newRotation;
        }
        else
        {
            return;
        }



        if (hasWeapon)
        {
            weapon.Attack();
            if (weapon.IsDepleted())
            {
                weapon.Dropped();
                hasWeapon = false;
                weapon = null;
            }
        }
        else
        {
            if (fistsTimeReady < Time.time)
            {
                if(Vector3.Distance(transform.position,player.transform.position)<fistsRange)
                {
                    Instantiate(fistAttackEffectPrefab,player.transform.position+Vector3.up,Quaternion.identity);
                    //Debug.Log("FALCON PUNCH");
                    fistsTimeReady = Time.time + fistsCooldown;
                    player.Damage(fistsDamage,fistsArmorPenetration);
                    //player.rigidbody.AddForce((transform.forward.normalized+Vector3.up)*2,ForceMode.VelocityChange);
                    player.Push(transform.forward.normalized * 5);
                }
            }
        }


        pushForce = Vector3.Lerp(pushForce, Vector3.zero, 0.05f);
    }

    public void TakeWeapon(Weapon w)
    {
        this.weapon = w;
        hasWeapon = true;
        //weapon.rigidbody.isKinematic = true;
        //weapon.collider.enabled = false;
        weapon.transform.position = weaponTransform.position;
        weapon.transform.rotation = weaponTransform.rotation;
        weapon.transform.parent = weaponTransform;
        weapon.Taken();
    }

    public void DropWeapon()
    {

        hasWeapon = false;
        //weapon.rigidbody.isKinematic = false;
        //weapon.collider.enabled = true;
        //weapon.transform.position = weaponTransform.position;
        //weapon.transform.rotation = weaponTransform.rotation;
        weapon.transform.parent = null;
        weapon.Dropped();
        this.weapon = null;

    }

    void FixedUpdate()
    {
        player2DDirection = (player.transform.position - transform.position);
        player2DDirection.y = 0;
        player2DDirection.Normalize();
        //if (Vector3.Project(rigidbody.velocity, player2DDirection).magnitude < maxSpeed)
        //{
        //    rigidbody.AddForce(player2DDirection * charachterImpulse);
        //}
        float playerDistance = (player.transform.position - transform.position).magnitude;
        if (hasWeapon)
        {
            if ( playerDistance > 10)
            {
                MoveTowardsPlayer();
            }
            else if (playerDistance > 6)
            {
                MoveAlongPlayer();
            }
            else
            {
                MoveAwayFromPlayerDodging();
            }
        }
        else
        {
            if (health > 10)
            {
                MoveTowardsPlayer();
            }
            else
            {
                MoveAwayFromPlayer();
            }
        }
    }

    public void MoveAwayFromPlayer()
    {
        rigidbody.velocity = -(player2DDirection * maxSpeed + pushForce) + (Vector3.up * rigidbody.velocity.y);
    }

    public void MoveAwayFromPlayerDodging()
    {
        auxVector.x = -player2DDirection.z;
        auxVector.z = player2DDirection.x;

        player2DDirection += auxVector;

        player2DDirection.Normalize();

        rigidbody.velocity = (player2DDirection * maxSpeed + pushForce) + (Vector3.up * rigidbody.velocity.y);
    }

    public void MoveTowardsPlayer()
    {
        rigidbody.velocity = (player2DDirection * maxSpeed + pushForce) + (Vector3.up * rigidbody.velocity.y);
    }

    public void MoveAlongPlayer()
    {
        float aux = player2DDirection.x;
        player2DDirection.x = player2DDirection.z;
        player2DDirection.z = -aux;
        player2DDirection.Normalize();
        rigidbody.velocity = (player2DDirection * maxSpeed + pushForce) + (Vector3.up * rigidbody.velocity.y);
    }


    public override void Die()
    {
        //base.Die();

        if (hasWeapon)
        {
            DropWeapon();
        }

        GameObject.Destroy(this.gameObject);
    }
}
