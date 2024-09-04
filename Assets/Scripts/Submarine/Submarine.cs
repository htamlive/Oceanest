using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour {

    [Header("Movement")]
    public float maxSpeed = 5;
    public float acceleration = 2;
    public float smoothSpeed = 3;

    [Header("Turning")]
    public float maxSternAngle = 15;
    public float maxPitchSpeed = 50;
    public float maxTurnSpeed = 50;
    public float smoothTurnSpeed = 3;

    //public Transform propeller;
    //public Transform rudderPitch;
    //public Transform rudderYaw;
    //public float propellerSpeedFac = 2;
    //public float rudderAngle = 30;

    Vector3 velocity;
    float yawVelocity;
    float pitchVelocity;
    float currentSpeed;

    [Header("Spin Fan")]
    public float maxSpinSpeed = 50;
    public Transform spinFan;

    void Start () {
        currentSpeed = maxSpeed;
    }

    void Update () {
        float accelDir = 0;
        if (Input.GetKey (KeyCode.Q)) {
            accelDir -= 1;
        }
        if (Input.GetKey (KeyCode.E)) {
            accelDir += 1;
        }

        #region Movement
        currentSpeed += acceleration * Time.deltaTime * accelDir;
        currentSpeed = Mathf.Clamp (currentSpeed, 0, maxSpeed);
        float speedPercent = currentSpeed / maxSpeed;
        Vector3 targetVelocity = transform.forward * currentSpeed;
        velocity = Vector3.Lerp (velocity, targetVelocity, Time.deltaTime * smoothSpeed);

        transform.Translate (transform.forward * currentSpeed * Time.deltaTime, Space.World);
        #endregion

        #region Turning
        float targetSternAngle = Input.GetAxisRaw("Vertical") * (-maxSternAngle);
        float currentSternAngle = transform.localEulerAngles.x;
        if (currentSternAngle > 180)
        {
            currentSternAngle -= 360;
        }
        float targetPitchVelocity = (targetSternAngle - currentSternAngle) / (2*maxSternAngle) * maxPitchSpeed;
        pitchVelocity = Mathf.Lerp(pitchVelocity, targetPitchVelocity, Time.deltaTime * smoothTurnSpeed);

        float targetYawVelocity = Input.GetAxisRaw ("Horizontal") * maxTurnSpeed;
        yawVelocity = Mathf.Lerp (yawVelocity, targetYawVelocity, Time.deltaTime * smoothTurnSpeed);

        transform.localEulerAngles += (Vector3.up * yawVelocity + Vector3.right * pitchVelocity) * Time.deltaTime * Mathf.Max(0.1f, speedPercent);
        #endregion

        #region Balancing
        float targetRollAngle = 0;
        float currentRollAngle = transform.localEulerAngles.z;
        if (currentRollAngle > 180)
        {
            currentRollAngle -= 360;
        }
        float targetRollVelocity = (targetRollAngle - currentRollAngle) * maxTurnSpeed;
        float rollVelocity = Mathf.Lerp(0, targetRollVelocity, Time.deltaTime * smoothTurnSpeed);
        transform.localEulerAngles += Vector3.forward * rollVelocity * Time.deltaTime;
        #endregion


        //rudderYaw.localEulerAngles = Vector3.up * yawVelocity / maxTurnSpeed * rudderAngle;
        //rudderPitch.localEulerAngles = Vector3.left * pitchVelocity / maxPitchSpeed * rudderAngle;

        //propeller.Rotate (Vector3.forward * Time.deltaTime * propellerSpeedFac * speedPercent, Space.Self);
        //propSpinMat.color = new Color (propSpinMat.color.r, propSpinMat.color.g, propSpinMat.color.b, speedPercent * .3f);

        #region Spin Fan
        spinFan.Rotate(Vector3.right, speedPercent * maxSpinSpeed);
        #endregion
    }
}