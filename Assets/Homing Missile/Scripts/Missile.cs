using System;
using UnityEngine;

namespace Tarodev {
    
    public class Missile : MonoBehaviour {
        [Header("REFERENCES")] 
        [SerializeField] private Rigidbody _rb;
        public Transform _target;
        [SerializeField] private GameObject _explosionPrefab;

        [Header("MOVEMENT")] 
        [SerializeField] private float _speed = 15;
        [SerializeField] private float _rotateSpeed = 95;

        [Header("PREDICTION")] 
        [SerializeField] private float _maxDistancePredict = 100;
        [SerializeField] private float _minDistancePredict = 5;
        [SerializeField] private float _maxTimePrediction = 5;
        private Vector3 _standardPrediction, _deviatedPrediction;

        [Header("DEVIATION")] 
        [SerializeField] private float _deviationAmount = 50;
        [SerializeField] private float _deviationSpeed = 2;

        [Header("DAMAGE")]
        [SerializeField] private int _damage = 100;

        private Vector3 initForward;
        private Vector3 lastTargetPosition;

        private void Start()
        {
            if (!_rb) _rb = GetComponent<Rigidbody>();
            initForward = transform.forward;
            lastTargetPosition = _target.position;
        }

        private void FixedUpdate() {

            if (!_target)
            {
                _rb.velocity = initForward * _speed;

            }
            else
            {
                _rb.velocity = transform.forward * _speed;
                var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, _target.position));

                PredictMovement(leadTimePercentage);

                AddDeviation(leadTimePercentage);

                RotateRocket();

                lastTargetPosition = _target.position;
            }
        }

        private void PredictMovement(float leadTimePercentage) {
            var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);
            _standardPrediction = _target.position + (_target.position - lastTargetPosition) / Time.fixedDeltaTime * predictionTime;
        }

        private void AddDeviation(float leadTimePercentage) {
            var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);
            
            var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;

            _deviatedPrediction = _standardPrediction + predictionOffset;
        }

        private void RotateRocket() {
            var heading = _deviatedPrediction - transform.position;

            var rotation = Quaternion.LookRotation(heading);
            _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.transform.TryGetComponent<Submarine>(out var sub))
            {
                return;
            }
            if (collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
            {
                //if (collision.transform.TryGetComponent<WormManager>(out var wm))
                //{
                //    wm.OnReceiveDamage(_damage);
                //}
                collision.gameObject.GetComponentInParent<WormManager>().OnReceiveDamage(_damage);
            }
            else
            {
                if (collision.transform.TryGetComponent<IExplode>(out var ex)) ex.Explode();
            }
            Debug.Log("Missile hit: " + collision.gameObject.name);
            if (_explosionPrefab) Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _standardPrediction);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
        }
    }
}