﻿using Helpers;
using Player;
using UnityEngine;

namespace Attacks
{
    [RequireComponent(typeof(MeleeWeaponAnimatorHandler))]
    public class MeleeWeapon : MeleeAttack
    {
        [SerializeField] private PlayerInput playerInput;
        
        private MeleeWeaponAnimatorHandler _animatorHandler;
        
        protected override void Awake()
        {
            _animatorHandler = GetComponent<MeleeWeaponAnimatorHandler>();
            GameManager.OnGameLost += DisableOnDead;
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();

            if (CantDamage()) return;

            _animatorHandler.PlayAttackAnimation();
            Attack();
        }

        protected override bool CantDamage()
        {
            return !playerInput.canShoot || !(Timer >= timeBetweenAttack);
        }

        private void DisableOnDead()
        {
            GetComponent<MeleeWeapon>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }

        private void OnDisable()
        {
            Timer = timeBetweenAttack;
        }

        private void OnDestroy()
        {
            GameManager.OnGameLost -= DisableOnDead;
        }
    }
}