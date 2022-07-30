using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator _animator;

        public event Action CutTheGrass;

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
            _animator.SetTrigger("Mow");
        }

        public void CutGrass()
        {
            CutTheGrass?.Invoke();
        }
    }
}