using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CharacterController), typeof(Collider))]
    public class BlockCollector : MonoBehaviour
    {
        [SerializeField] private int maxBlocks = 40;
        [SerializeField] private Transform rootForPickUp;
        [SerializeField] private float delayBeforeDeliver;
        private float _timer;
        private List<GrassBlock> _grassBlocks = new List<GrassBlock>();

        private StatisticsCounter _statisticsCounter;

        private void Awake()
        {
            _statisticsCounter = FindObjectOfType<StatisticsCounter>();
        }

        public void RaiseBlock(GrassBlock grassBlock)
        {
            if(_grassBlocks.Count > maxBlocks-1) return;
            _grassBlocks.Add(grassBlock);
            grassBlock.PickUp(rootForPickUp, _grassBlocks.Count, maxBlocks);
        }

        public void DeliverBlocks(GrassReceiver grassReceiver)
        {
            if(Time.time < _timer) return;
            else _timer = Time.time + delayBeforeDeliver;
            if (_grassBlocks.Count != 0)
            {
                var lastGrassBlock = _grassBlocks[_grassBlocks.Count-1];
                lastGrassBlock.Throw(grassReceiver.TargetDeliver);
                _statisticsCounter.AddMoney(lastGrassBlock.GetCostBlock(), grassReceiver.TargetDeliver);
                _grassBlocks.Remove(lastGrassBlock);
            }
            
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
            GrassReceiver grassReceiver = other.GetComponent<GrassReceiver>();
            if (grassReceiver)
            {
                DeliverBlocks(grassReceiver);
            }
        }
    }
}