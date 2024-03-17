using StatePattern.Player;

namespace StatePattern.Enemy
{
    public class RobotController : EnemyController
    {
        public RobotStateMachine stateMachine { get; private set; }
        public int cloneCount { get; private set; }
        public RobotController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            SetCloneCount(enemyScriptableObject.CloneCount);
            enemyView.SetController(this);
            ChangeColor(EnemyColorType.Default);
            CreateStateMachine();
            stateMachine.ChangeState(StateMachine.States.IDLE);
        }
        public void SetCloneCount(int cloneCountToSet) => cloneCount = cloneCountToSet;
        private void CreateStateMachine() => stateMachine = new RobotStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        public override void Shoot()
        {
            base.Shoot();
            stateMachine.ChangeState(StateMachine.States.TELEPORTING);
        }

        public override void Die()
        {
            if (cloneCount > 0)
                stateMachine.ChangeState(StateMachine.States.CLONING);

            base.Die();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(StateMachine.States.CHASING);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(StateMachine.States.IDLE);
        public void SetDefaultColor(EnemyColorType colorType) => enemyView.SetDefaultColor(colorType);
        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);
    }
}