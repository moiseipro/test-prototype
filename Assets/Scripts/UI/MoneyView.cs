using System;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private GameObject _moneyIcon;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private float _counterSpeed = 3;
        private float _moneyCount = 0;
        private StatisticsCounter _statisticsCounter;
        
        private Sequence _sequenceDoTween;

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
            GameObject animateMoneyIcon = Instantiate(_moneyIcon, Camera.main.WorldToScreenPoint(spawnPoint.position), Quaternion.identity, transform);
            _sequenceDoTween = DOTween.Sequence();
            _sequenceDoTween.Append(animateMoneyIcon.transform.DOMove(_moneyIcon.transform.position, 1f, false));
            _sequenceDoTween.Insert(1f,_moneyText.transform.DOShakePosition(1f, 10f, 10));
            _sequenceDoTween.OnComplete(() => Destroy(animateMoneyIcon));
        }

        private void OnDisable()
        {
            _statisticsCounter.MoneyAdded -= AnimateCoin;
        }
    }
}