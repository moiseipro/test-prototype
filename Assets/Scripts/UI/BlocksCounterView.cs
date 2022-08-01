using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class BlocksCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _blocksCountText;

        private StatisticsCounter _statisticsCounter;
        
        private void Awake()
        {
            _statisticsCounter = FindObjectOfType<StatisticsCounter>();
            _statisticsCounter.BlockCountChanged += UpdateBlockCounter;
        }

        private void UpdateBlockCounter(int value)
        {
            _blocksCountText.text = $"{value} / {_statisticsCounter.MaxBlocks}";
        }
    }
}