using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int Death = Animator.StringToHash(nameof(Death));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}
