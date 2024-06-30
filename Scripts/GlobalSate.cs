namespace GlobalState
{
    public class PlayerState
    {

        internal static GameManager hpManager = GameManager.GetInstance;
        internal static float NomalJump = 8f;
        internal static float powerJump = 13f;
        internal static int maxHealthPlayer = 300;
        internal static float powerNomalAttackPlayer = 40f;
        internal static float kickAttackDamage = 35f;
        internal static float punchAttackDamage = 30f;
        internal static float BowDamage = 30f;

        internal static float Speed = 5f;


        internal static readonly string OnWalk = "OnWalk";
        internal static readonly string IsOnWall = "IsOnWall";
        internal static readonly string IsOnCeiling = "IsOnCeiling";
        internal static readonly string OnRun = "OnRun";
        internal static readonly string OnJump = "OnJump";
        internal static readonly string attack = "attack";
        internal static readonly string IsGrounded = "IsGrounded";
        internal static readonly string YVelocity = "YVelocity";
        internal static readonly string Crouch = "Crouch";
        internal static readonly string IsDead = "IsDead";
        internal static readonly string hurt = "hurt";
        internal static readonly string attack2 = "attack2";
        internal static readonly string attack3 = "attack3";
        internal static readonly string attack4 = "attack4";
    }

    internal class EnemyState
    {
        internal static readonly float maxHealth = 100f;
        internal static readonly string hurt = "hurt";
        internal static readonly string IsDead = "IsDead";
        internal static readonly string KnightAttack = "KnightAttack";
    }
}
