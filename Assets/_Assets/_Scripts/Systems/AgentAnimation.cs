using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Assets._Scripts.Systems
{
    public class AgentAnimation : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayAnimation(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.idle:
                    Play("Idle");
                    break;
                case AnimationType.run:
                    Play("Run");
                    break;
                case AnimationType.jump:
                    break;
                case AnimationType.dash:
                    break;
                case AnimationType.climb:
                    break;
                case AnimationType.land:
                    break;
                case AnimationType.fall:
                    break;
                case AnimationType.attack:
                    break;
                case AnimationType.hit:
                    break;
                case AnimationType.die:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
            }
        }
        
        public void Play(string animationName)
        {
            _animator.Play(animationName, -1, 0f);
        }
    }

    public enum AnimationType
    {
        idle,
        run,
        jump,
        dash,
        climb,
        land,
        fall,
        attack,
        hit, 
        die
    }
}

/*
 * Essa classe gerencia qual animação que vai ser inicializada
 */