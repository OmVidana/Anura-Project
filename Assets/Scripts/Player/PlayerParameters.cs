
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anura
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Create Player Data")]
    public class PlayerParameters : ScriptableObject
    {
        [field: SerializeField] [field: Range(0f, 15f)] public float WalkingSpeed { get; private set; }
        [field: SerializeField] [field: Range(0f, 25f)] public float RunningSpeed { get; private set; }
        [field: SerializeField] [field: Range(0f, 50f)] public float JumpForce { get; private set; }
        [field: SerializeField] [field: Range(-10f, 0f)] public float JumpDownForce { get; private set; }
        
        [field: SerializeField] [field: Range(-25f, 0f)] public float MaxJumpDeceleration { get; private set; }
        [field: SerializeField] [field: Range(0f, 1f)] public float GroundOffset { get; private set; }
        [field: SerializeField] public LayerMask JumpableLayers { get; private set; }
        [field: SerializeField] public LayerMask TubeLayers { get; private set; }
        [field: SerializeField] [field: Range(0, 99)] public int AttackDmg { get; private set; }
        [field: SerializeField] [field: Range(0f, 10f)] public float AttackRange { get; private set; }
        [field: SerializeField] [field: Range(0f, 5f)] public float AttackRadius { get; private set; }
        [field: SerializeField] [field: Range(0f, 10f)] public float AttackCooldown { get; private set; }
        [field: SerializeField] [field: Range(0f, 10f)] public float TakingDmgCooldown { get; private set; }
    }
}
