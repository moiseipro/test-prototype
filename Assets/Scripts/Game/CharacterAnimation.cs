using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _braid;
        private Animator _animator;

        public event Action CutTheGrass;
        public event Action FinisMow;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Move(float speed)
        {
            _animator.SetFloat("speed", speed);
        }
        
        public void Mow()
        {
            _animator.SetBool("IsMow", true);
            _braid.SetActive(true);
        }
        
        public void StopMow()
        {
            _animator.SetBool("IsMow", false);
            _braid.SetActive(false);
        }

        // Animation Event
        public void CutGrass()
        {
            CutTheGrass?.Invoke();
        }
        
        // Animation Event
        public void EndMow()
        {
            FinisMow?.Invoke();
        }

        
    }
}