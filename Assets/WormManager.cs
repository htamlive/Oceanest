using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormManager : MonoBehaviour
{
    [SerializeField] private GameObject wormboss;
    [SerializeField] private GameObject submarine;
    [SerializeField] private float radius = 2f;
    [SerializeField] private float attackDuration = 7f;
    [SerializeField] private float attackDelayTime = 10f;
    [SerializeField] private float maxHealth = 100f;

    private Animator animator;
    private Vector3 basePos;
    private bool foundNewPos = false;
    private const float attackLength = 85f;
    private float attackTime = 0;
    private float currentHealth;


    public float GetHealthPercent()
    {
        return currentHealth / maxHealth * 100f;
    }

    public void OnReceiveDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = wormboss.GetComponent<Animator>();
        animator.SetBool("appear", false);
        basePos = wormboss.GetComponent<Transform>().position;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private bool IsHidden()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Hidden");
    }

    private void OnDeath()
    {
        animator.SetBool("Death", true);
    }

    private void FixedUpdate()
    {
        if (IsHidden() && Time.time > attackTime)
        {
            if (!foundNewPos)
            {
                Vector3 submarineVelocity = submarine.GetComponent<Rigidbody>().velocity;
                float dx, dz, len;
                // FRONT ATTACK: rate < 0.5
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    float ang;
                    dx = submarineVelocity.x;
                    dz = submarineVelocity.z;
                    len = Mathf.Sqrt(dx * dx + dz * dz);
                    dx /= len;
                    dz /= len;

                    if (Mathf.Abs(dx) < 0.001 && Mathf.Abs(dz) < 0.001)
                    {
                        ang = Random.Range(0f, 2f);
                    }
                    else
                    {
                        ang = Mathf.Acos(dz);
                        if (Mathf.Abs(Mathf.Sin(ang) - dx) > 0.0001)
                        {
                            ang = -ang;
                        }
                    }

                    float distance = attackLength - Random.Range(0, 10);
                    Vector3 appearPos = new Vector3(submarine.transform.position.x + distance * Mathf.Sin(ang) + submarineVelocity.x * attackDuration, 
                        basePos.y, submarine.transform.position.z + distance * Mathf.Cos(ang) + submarineVelocity.z * attackDuration);
                    dx = appearPos.x - basePos.x;
                    dz = appearPos.z - basePos.z;
                    if (Mathf.Sqrt(dx * dx + dz * dz) <= radius)
                    {
                        //Debug.Log("Front " + Time.time);
                        wormboss.transform.position = appearPos;
                        wormboss.transform.eulerAngles = new Vector3(0, ang * 180f / Mathf.PI + 180f, 0);
                        foundNewPos = true;
                        animator.SetBool("appear", true);
                        attackTime = Time.time + attackDuration + attackDelayTime * Random.Range(0.5f, 1f);
                        return;
                    }
                }

                //Debug.Log("Random " + Time.time);

                // RANDOM APPEAR POS
                // position
                float r = Mathf.Sqrt(1.0f - Random.Range(0f, 1f)) * radius;
                float posAng = Random.Range(0f, 2f);
                Vector3 newPos = new Vector3(basePos.x + r * Mathf.Cos(posAng * Mathf.PI), basePos.y, basePos.z + r * Mathf.Sin(posAng * Mathf.PI));
                wormboss.transform.position = newPos;

                // attack angle, predict position
                dx = (submarine.transform.position.x + submarineVelocity.x * attackTime) - newPos.x;
                dz = (submarine.transform.position.z + submarineVelocity.z * attackTime) - newPos.z;

                len = Mathf.Sqrt(dx * dx + dz * dz);
                dx /= len;
                dz /= len;
                float atkAng = Mathf.Acos(dz);
                if (Mathf.Abs(Mathf.Sin(atkAng) - dx) > 0.0001)
                {
                    atkAng = -atkAng;
                }


                wormboss.transform.eulerAngles = new Vector3(0, atkAng * 180f / Mathf.PI, 0);

                foundNewPos = true;
                animator.SetBool("appear", true);
                attackTime = Time.time + attackDuration + attackDelayTime * Random.Range(0.5f, 1f);
            }
        }
        else if (!IsHidden())
        {
            animator.SetBool("appear", false);
            foundNewPos= false;
        }
    }
}
