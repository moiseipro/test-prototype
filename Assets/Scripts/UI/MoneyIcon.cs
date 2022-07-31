using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class MoneyIcon : MonoBehaviour
    {
        private Sequence _sequenceDoTween;
        
        public void MoveToTarget(Transform target)
        {
            _sequenceDoTween = DOTween.Sequence();
            _sequenceDoTween.Append(transform.DOMove(target.position, 1f, false));
            _sequenceDoTween.Append(transform.DOShakePosition(1f, 1f, 10));
            _sequenceDoTween.OnComplete(SelfDestruction);

        }

        private void SelfDestruction()
        {
            Destroy(gameObject);
        }
    }
}