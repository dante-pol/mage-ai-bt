using TMPro;
using UnityEngine;

namespace Root
{
    public class MeleeAttacker
    {
        public bool IsFreeze { get; set; }

        private readonly IEntityAttacked _agent;

        private readonly ITriggerHandler _triggerHandler;

        private readonly IMeleeAgentConfig _config;

        public MeleeAttacker(ITriggerHandler triggerHandler, IEntityAttacked agent, IMeleeAgentConfig config)
        {
            _triggerHandler = triggerHandler;
            _agent = agent;
            _config = config;

            _triggerHandler.TriggerEnterEvent += AttackHandler;
        }

        private void AttackHandler(Collider other)
        {
            if (IsFreeze) return;

            IEntityAttacked entity = other.GetComponent<IEntityAttacked>();

            if (entity == null) return;

            if (_agent.TeamID == entity.TeamID) return;

            entity.TakeAttack(new AttackProcess(_config.Damage));
        }
    }
}