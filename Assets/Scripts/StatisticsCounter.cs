using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StatisticsCounter : MonoBehaviour
    {
        [SerializeField] private int maxBlocks = 40;
        public int MaxBlocks => maxBlocks;
        
        private int _money = 0;
        public int Money => _money;

        public event Action<Transform> MoneyAdded;
        public event Action<int> BlockCountChanged;

        public void AddMoney(int value, Transform iconSpawnPoint)
        {
            _money += value;
            MoneyAdded?.Invoke(iconSpawnPoint);
        }

        public void ChangeCountBlocks(int value)
        {
            BlockCountChanged?.Invoke(value);
        }
    }
}