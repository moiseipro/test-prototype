using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StatisticsCounter : MonoBehaviour
    {
        private int _money = 0;
        public int Money => _money;

        public event Action<Transform> MoneyAdded;

        public void AddMoney(int value, Transform iconSpawnPoint)
        {
            _money += value;
            MoneyAdded?.Invoke(iconSpawnPoint);
        }
    }
}