using GFA.Case03.Mediators;
using GFA.Case03.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GFA.Case03.AI.States
{
    public class BasicAIState : AIState
    {
        public CharacterMovement CharacterMovement { get; set; }
        public EnemyAttacker Attacker { get; set; }
        public IDamageable PlayerDamageable { get; set; }
    }
}

