using UnityEngine;

namespace Game
{
    public class GrassReceiver : MonoBehaviour
    {
        [SerializeField] private Transform _targetDeliver;
        public Transform TargetDeliver => _targetDeliver;
    }
}