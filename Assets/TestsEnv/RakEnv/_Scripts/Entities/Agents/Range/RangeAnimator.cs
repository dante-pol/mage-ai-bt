﻿using UnityEngine;

namespace Root.Core.Entities.Agents.Range
{
    public class RangeAnimator
    {
        private readonly GameObject _agent;

        private readonly GameObject _magicBall;

        private Material _magicBallMaterial;

        public RangeAnimator(GameObject agent, GameObject magicBall)
        {
            _agent = agent;

            _magicBall = magicBall;
        }

        // Legacy. TODO: ReCode
        public void SetIdle() { }

        // Legacy. TODO: ReCode
        public void UpdateEffectMagicBall(int attackLevel) { }
    }
}
