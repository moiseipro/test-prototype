using System;
using DG.Tweening;
using GardenManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
    public class GrassBlock : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Collider _collider;
        private GardenBedFeatures _gardenBedFeatures;
        private Transform _targetPickUp;
        
        private MeshRenderer _meshRenderer;
        private Sequence _doTweenSequence;

        private bool _isPicked = false;
        public bool IsPicked => _isPicked;
        private int _number, _maxNumber;

        public event Action<GrassBlock> Delivered;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            
            _doTweenSequence = DOTween.Sequence();
        }

        public void Initialization(GardenBedFeatures gardenBedFeatures)
        {
            _gardenBedFeatures = gardenBedFeatures;
        }

        public int GetCostBlock()
        {
            return _gardenBedFeatures.Cost;
        }

        public void Toss()
        {
            float power = Random.Range(1f, 3f);
            _rigidbody.AddForce(Vector3.up * power, ForceMode.Impulse);
        }

        public void PickUp(Transform target, int number, int maxNumber)
        {
            _isPicked = true;
            _targetPickUp = target;
            _number = number;
            _maxNumber = maxNumber;
            _rigidbody.isKinematic = _isPicked;
            _collider.enabled = !_isPicked;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        public void SendDelivery(Transform target)
        {
            _targetPickUp = null;
            transform.DOJump(target.transform.position, 4f, 1, Random.Range(1f, 1.5f), false).OnComplete(Deliver);

        }
        
        private void Deliver()
        {
            Delivered?.Invoke(this);
        }

        private void FixedUpdate()
        {
            if (_targetPickUp)
            {
                transform.position = Vector3.Slerp(
                    transform.position, 
                    _targetPickUp.position - transform.forward/3f + transform.up * _meshRenderer.bounds.size.y/2f * _number, 
                    Time.fixedDeltaTime * _maxNumber / _number * 2f);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, 
                    _targetPickUp.rotation, 
                    Time.fixedDeltaTime * _maxNumber / _number * 3f);
                
            }
        }
    }
}