using GFA.Case03.AI;
using GFA.Case03.AI.States;
using GFA.Case03.MatchSystem;
using GFA.Case03.Mediators;
using GFA.Case03.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.Case03.AI.Behaviour
{
    [CreateAssetMenu(menuName = "AI/Basic AI Behaviour")]
    public class BasicAIBehaviour : AIBehaviour
    {
        [SerializeField]
        private float _acceptanceRadius;

        [SerializeField]
        private MatchInstance _matchInstance;

        public override void Begin(AIController controller)
        {
            if (controller.TryGetState<BasicAIState>(out var state))
            {
                state.CharacterMovement = controller.GetComponent<CharacterMovement>();
                state.Attacker = controller.GetComponent<EnemyAttacker>();
                state.PlayerDamageable = _matchInstance.Player.GetComponent<IDamageable>();
            }
        }

        protected override void Execute(AIController controller)
        {
            if (!controller.TryGetState<BasicAIState>(out var state)) return;

            var player = _matchInstance.Player;

            var movement = state.CharacterMovement;

            var dist = Vector3.Distance(player.transform.position, controller.transform.position);
            var dir = (player.transform.position - controller.transform.position).normalized;

            if (dist < _acceptanceRadius || !state.Attacker.IsCurrentlyAttacking)
            {

                movement.MovementInput = new Vector2(dir.x, dir.z);
            }

            var rotation = Quaternion.LookRotation(dir);
            movement.Rotation = rotation.eulerAngles.y;

            if (dist < state.Attacker.Range)
            {
                state.Attacker.Attack(state.PlayerDamageable);
            }
        }

        public override void End(AIController controller)
        {
        }

        public override AIState CreateState()
        {
            return new BasicAIState();
        }
    }
}
