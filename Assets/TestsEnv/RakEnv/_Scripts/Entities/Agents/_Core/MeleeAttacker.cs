using UnityEngine;

namespace Root
{
    public class MeleeAttacker
    {
        public bool IsFreeze { get; set; }

        private readonly ITriggerHandler _triggerHandler;

        private readonly MeleeAgent _agent;

        private readonly IMeleeAgentConfig _config;

        public MeleeAttacker(ITriggerHandler triggerHandler, MeleeAgent agent, IMeleeAgentConfig config)
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

            entity.TakeAttack(new AttackProcess(_config.Damage));
        }
    }
}