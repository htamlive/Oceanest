using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour {

    [Header("Movement")]
    public float speedMaxLevel = 5;
    public float initialSpeedLevel = 3;
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

    [Header("Spin Fan")]
    public float maxSpinSpeed = 50;
    public Transform spinFan;

    [Header("Missile")]
    public GameObject missilePrefab;

    void Start () {
        currentSpeed = maxSpeed;
        speedLevel = initialSpeedLevel;
        body = GetComponent<Rigidbody>();
    }

    void Update () {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Q)) {
            speedLevel -= 1;   
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            speedLevel += 1;
        }
        speedLevel = Mathf.Clamp(speedLevel, 0, speedMaxLevel);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(missilePrefab, transform.position + transform.forward * 20 - transform.up * 10, Quaternion.identity);
        }

        #region Turning
        //float targetSternAngle = Input.GetAxisRaw("Vertical") * (-maxSternAngle);
        //float currentSternAngle = transform.localEulerAngles.x;
        //if (currentSternAngle > 180)
        //{
        //    currentSternAngle -= 360;
        //}
        //float targetPitchVelocity = (targetSternAngle - currentSternAngle) / (2*maxSternAngle) * maxPitchSpeed;
        //pitchVelocity = Mathf.Lerp(pitchVelocity, targetPitchVelocity, Time.deltaTime * smoothTurnSpeed);

        //float targetYawVelocity = Input.GetAxisRaw ("Horizontal") * maxTurnSpeed;
        //yawVelocity = Mathf.Lerp (yawVelocity, targetYawVelocity, Time.deltaTime * smoothTurnSpeed);

        //transform.localEulerAngles += (Vector3.up * yawVelocity + Vector3.right * pitchVelocity) * Time.deltaTime * Mathf.Max(0.1f, (float)speedLevel/speedMaxLevel);
        #endregion

        #region Balancing
        //float targetRollAngle = 0;
        //float currentRollAngle = transform.localEulerAngles.z;
        //if (currentRollAngle > 180)
        //{
        //    targetRollAngle = 360;
        //}
        //float targetRollVelocity = (targetRollAngle - currentRollAngle) * maxTurnSpeed;
        //float rollVelocity = Mathf.Lerp(0, targetRollVelocity, Time.deltaTime * smoothTurnSpeed);
        //transform.localEulerAngles += transform.forward * rollVelocity * Time.deltaTime;
        //Debug.Log(transform.localEulerAngles);
        #endregion


        //rudderYaw.localEulerAngles = Vector3.up * yawVelocity / maxTurnSpeed * rudderAngle;
        //rudderPitch.localEulerAngles = Vector3.left * pitchVelocity / maxPitchSpeed * rudderAngle;

        //propeller.Rotate (Vector3.forward * Time.deltaTime * propellerSpeedFac * speedPercent, Space.Self);
        //propSpinMat.color = new Color (propSpinMat.color.r, propSpinMat.color.g, propSpinMat.color.b, speedPercent * .3f);

        #region Spin Fan
        //spinFan.Rotate(Vector3.forward, speedPercent * maxSpinSpeed);
        #endregion
    }

    void FixedUpdate()
    {
        Turn();
        Balance();
        MoveToward(1f);
        SpinFan();
        //body.velocity = velocity;
        Debug.Log(speedLevel + " " + speedLevel / speedMaxLevel);
    }

    private void MoveToward(float lerpAmount)
    {
        float targetVelo = speedLevel / speedMaxLevel * maxSpeed;
        Vector3 targetVeloVec = Vector3.Lerp(body.velocity, transform.forward * targetVelo, lerpAmount);

        Vector3 veloDiff = targetVeloVec - body.velocity;
        Vector3 movement = veloDiff * acceleration;
        body.AddForce(targetVeloVec * acceleration, ForceMode.Force);

        //currentSpeed += acceleration * Time.deltaTime * speedLevel;
        //currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        //float speedPercent = currentSpeed / maxSpeed;

        //Vector3 targetVelocity = transform.forward * currentSpeed;
        //velocity = Vector3.Lerp(velocity, targetVelocity, Time.deltaTime * smoothSpeed);

        //transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

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

        transform.localEulerAngles += (Vector3.up * yawVelocity + Vector3.right * pitchVelocity) * Time.deltaTime * Mathf.Max(0.1f, (float)speedLevel / speedMaxLevel);
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
        transform.localEulerAngles += Vector3.forward * rollVelocity * Time.deltaTime;
        Debug.Log(transform.localEulerAngles);
    }

    private void SpinFan()
    {
        spinFan.Rotate(Vector3.forward, speedLevel * maxSpinSpeed / speedMaxLevel);
    }
}