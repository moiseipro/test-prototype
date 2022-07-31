using System;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private MoneyIcon _moneyIcon;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private float _counterSpeed = 3;
        private float _moneyCount = 0;
        private StatisticsCounter _statisticsCounter;

        private void Awake()
        {
            _statisticsCounter = FindObjectOfType<StatisticsCounter>();
            _statisticsCounter.MoneyAdded += AnimateCoin;
        }

        private void Update()
        {
            _moneyCount = Mathf.Lerp(_moneyCount, _statisticsCounter.Money, Time.deltaTime * _counterSpeed);
            _moneyText.text = Mathf.Round(_moneyCount).ToString();
        }

        private void AnimateCoin(Transform spawnPoint)
        {
            MoneyIcon animateMoneyIcon = Instantiate(_moneyIcon, Camera.main.WorldToScreenPoint(spawnPoint.position), Quaternion.identity, transform);
            animateMoneyIcon.MoveToTarget(_moneyIcon.transform);
        }

        private void OnDisable()
        {
            _statisticsCounter.MoneyAdded -= AnimateCoin;
        }
    }
}