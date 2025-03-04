using UnityEngine;

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

            _magicBallMaterial = _magicBall.GetComponent<MeshRenderer>().material;
        }

        public void SetIdle()
            => UpdateEffectMagicBall(Color.blue);

        public void UpdateEffectMagicBall(Color color) 
            => _magicBallMaterial.color = color;
    }
}
