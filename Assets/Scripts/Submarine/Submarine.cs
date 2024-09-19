using System.Collections;
using System.Collections.Generic;
using Tarodev;
using UnityEngine;

public class Submarine : MonoBehaviour {

    [Header("Movement")]
    public float speedMaxLevel = 3;
    public float initialSpeedLevel = 0;
    public float maxSpeed = 50;
    public float acceleration = 10;
    public float smoothSpeed = 3;

    [Header("Turning")]
    public float maxSternAngle = 15;
    public float maxPitchSpeed = 50;
    public float maxRollSpeed = 100;
    public float maxTurnSpeed = 50;
    public float smoothTurnSpeed = 3;

    //public Transform propeller;
    //public Transform rudderPitch;
    //public Transform rudderYaw;
    //public float propellerSpeedFac = 2;
    //public float rudderAngle = 30;

    Vector2 moveInput;
    float speedLevel;
    Vector3 velocity;
    float yawVelocity;
    float pitchVelocity;
    float currentSpeed;
    Rigidbody body;
    SubmarineStat subStat;
    CharacterEffects characterEffects;

    float lastEmittTime = 0;

    [Header("Spin Fan")]
    public float maxSpinSpeed = 5;
    public Transform spinFan;

    [Header("Missile")]
    public GameObject missilePrefab;

    [Header("Skill Lock")]
    public SubmarineSkillLock skills;

    [Header("Damage")]
    public int wallCollisionDamage = 5;

    [Header("Attack")]
    public float cooldown = 1f;

    void Start () {
        var playerData = GameDataManager.GetPlayerData();
        skills = playerData.submarineSkillLock;

        currentSpeed = maxSpeed;
        speedLevel = initialSpeedLevel;
        body = GetComponent<Rigidbody>();
        subStat = GetComponent<SubmarineStat>();
        characterEffects = GetComponent<CharacterEffects>();

        lastEmittTime = Time.time;
    }

    void Update () {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Q)) {
            speedLevel -= 1;   
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            speedLevel += 1;
        }
        speedLevel = Mathf.Clamp(speedLevel, -speedMaxLevel, speedMaxLevel);

        if (Input.GetKeyDown(KeyCode.J))
        {
            EmittMissile();
        }
    }

    void FixedUpdate()
    {
        Turn();
        Balance();
        MoveToward(1f);
        SpinFan();
    }

    private void MoveToward(float lerpAmount)
    {
        float targetVelo = speedLevel / speedMaxLevel * maxSpeed;
        Vector3 targetVeloVec = Vector3.Lerp(body.velocity, transform.forward * targetVelo, lerpAmount);

        Vector3 veloDiff = targetVeloVec - body.velocity;
        Vector3 movement = veloDiff * acceleration;
        body.AddForce(targetVeloVec * acceleration, ForceMode.Force);
    }

    private void Turn()
    {
        float targetSternAngle = moveInput.y * (-maxSternAngle);
        float currentSternAngle = transform.localEulerAngles.x;
        if (currentSternAngle > 180)
        {
            currentSternAngle -= 360;
        }
        float targetPitchVelocity = (targetSternAngle - currentSternAngle) / (2 * maxSternAngle) * maxPitchSpeed;
        pitchVelocity = Mathf.Lerp(pitchVelocity, targetPitchVelocity, Time.deltaTime * smoothTurnSpeed);

        float targetYawVelocity = Input.GetAxisRaw("Horizontal") * maxTurnSpeed;
        yawVelocity = Mathf.Lerp(yawVelocity, targetYawVelocity, Time.deltaTime * smoothTurnSpeed);

        transform.localEulerAngles += Mathf.Max(0.2f, Mathf.Abs(speedLevel) / speedMaxLevel) * Mathf.Sign(speedLevel) * Time.deltaTime * (Vector3.up * yawVelocity + Mathf.Sign(speedLevel) * pitchVelocity * Vector3.right);
    }

    private void Balance()
    {
        float targetRollAngle = 0;
        float currentRollAngle = transform.localEulerAngles.z;
        if (currentRollAngle > 180)
        {
            targetRollAngle = 360;
        }
        float targetRollVelocity = (targetRollAngle - currentRollAngle) * maxRollSpeed;
        float rollVelocity = Mathf.Lerp(0, targetRollVelocity, Time.deltaTime * smoothTurnSpeed);
        transform.localEulerAngles += rollVelocity * Time.deltaTime * Vector3.forward;
    }

    private void SpinFan()
    {
        spinFan.Rotate(Vector3.forward, speedLevel * maxSpinSpeed / speedMaxLevel);
    }

    private void EmittMissile()
    {
        if (!skills.canEmittMissile || Time.time - lastEmittTime < cooldown) return;
        lastEmittTime = Time.time;

        GameObject[] targetList = GameObject.FindGameObjectsWithTag("Target");
        Debug.Log(targetList.Length);

        if (skills.doubleMissile)
        {
            GameObject missileObject_1 = Instantiate(missilePrefab, transform.position + transform.forward * 6f - transform.up * 3f - transform.right * 1, Quaternion.Euler(transform.localEulerAngles));
            GameObject missileObject_2 = Instantiate(missilePrefab, transform.position + transform.forward * 6f - transform.up * 3f + transform.right * 1, Quaternion.Euler(transform.localEulerAngles));
            Missile missile_1 = missileObject_1.GetComponent<Missile>();
            Missile missile_2 = missileObject_2.GetComponent<Missile>();
            missile_1.cameraEffects = characterEffects.cameraEffects;
            missile_2.cameraEffects = characterEffects.cameraEffects;

            if (skills.trackingMissile)
            {
                //missile_1._target = GameObject.FindFirstObjectByType<Target>();
                //missile_2._target = GameObject.FindFirstObjectByType<Target>();
                
                if (targetList.Length > 0)
                {
                    missile_1._target = targetList[Random.Range(0, targetList.Length - 1)].GetComponent<Transform>();
                    missile_2._target = targetList[Random.Range(0, targetList.Length - 1)].GetComponent<Transform>();

                } 
            }
        }
        else
        {
            GameObject missileObject = Instantiate(missilePrefab, transform.position + transform.forward * 6f - transform.up * 2f, Quaternion.Euler(transform.localEulerAngles));
            Missile missile = missileObject.GetComponent<Missile>();
            missile.cameraEffects = characterEffects.cameraEffects;

            if (skills.trackingMissile)
            {
                if (targetList.Length > 0)
                {
                    missile._target = targetList[Random.Range(0, targetList.Length - 1)].GetComponent<Transform>();
                }
            }
        }
        characterEffects.AttackEffect();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            subStat.Damage(wallCollisionDamage);
            characterEffects.cameraEffects.Shake(200f, 1f);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            subStat.Damage(collision.gameObject.GetComponentInParent<WormManager>().attackDamage);
        }
    }

}