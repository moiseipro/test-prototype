using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class BlockCollector : MonoBehaviour
    {
        [SerializeField] private Transform rootForPickUp;
        [SerializeField] private float delayBeforeDeliver;
        private float _timer;
        private List<GrassBlock> _grassBlocks = new List<GrassBlock>();

        private StatisticsCounter _statisticsCounter;
        private GrassReceiver _grassReceiver;

        private void Awake()
        {
            _statisticsCounter = FindObjectOfType<StatisticsCounter>();
        }

        public void RaiseBlock(GrassBlock grassBlock)
        {
            if(_grassBlocks.Count > _statisticsCounter.MaxBlocks-1) return;
            _grassBlocks.Add(grassBlock);
            _statisticsCounter.ChangeCountBlocks(_grassBlocks.Count);
            grassBlock.PickUp(rootForPickUp, _grassBlocks.Count, _statisticsCounter.MaxBlocks);
        }

        public void DeliverBlocks()
        {
            if(Time.time < _timer) return;
            else _timer = Time.time + delayBeforeDeliver;
            if (_grassBlocks.Count != 0)
            {
                var lastGrassBlock = _grassBlocks[_grassBlocks.Count-1];
                lastGrassBlock.SendDelivery(_grassReceiver.TargetDeliver);
                lastGrassBlock.Delivered += DeliveredBlock;
                _grassBlocks.Remove(lastGrassBlock);
                _statisticsCounter.ChangeCountBlocks(_grassBlocks.Count);
                
            }
            
        }

        public void DeliveredBlock(GrassBlock grassBlock)
        {
            _statisticsCounter.AddMoney(grassBlock.GetCostBlock(), _grassReceiver.TargetDeliver);
            grassBlock.Delivered -= DeliveredBlock;
            Destroy(grassBlock.gameObject);
        }
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            GrassBlock grassBlock = hit.gameObject.GetComponent<GrassBlock>();
            if (grassBlock)
            {
                Debug.Log("Collect Block");
                if(!grassBlock.IsPicked) RaiseBlock(grassBlock);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            _grassReceiver = other.GetComponent<GrassReceiver>();
            if (_grassReceiver)
            {
                DeliverBlocks();
            }
        }
    }
}