using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CharacterController))]
    public class BlockCollector : MonoBehaviour
    {
        [SerializeField] private int maxBlocks = 40;
        [SerializeField] private Transform rootForPickUp;
        private List<GrassBlock> _grassBlocks = new List<GrassBlock>();

        public void RaiseBlock(GrassBlock grassBlock)
        {
            if(_grassBlocks.Count > maxBlocks-1) return;
            _grassBlocks.Add(grassBlock);
            grassBlock.PickUp(rootForPickUp, _grassBlocks.Count, maxBlocks);
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
    }
}