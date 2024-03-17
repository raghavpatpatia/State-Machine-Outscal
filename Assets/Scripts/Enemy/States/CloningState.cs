using StatePattern.Main;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class CloningState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        public CloningState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() 
        {
            CreateClone();
            CreateClone();
        }

        public void OnStateExit() { }

        public void Update() { }

        private void CreateClone()
        {
            RobotController clonedRobot = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as RobotController;
            clonedRobot.SetCloneCount((Owner as RobotController).cloneCount - 1);
            clonedRobot.stateMachine.ChangeState(States.TELEPORTING);
            clonedRobot.SetDefaultColor(EnemyColorType.Clone);
            clonedRobot.ChangeColor(EnemyColorType.Clone);
            GameService.Instance.EnemyService.AddEnemy(clonedRobot);
        }
    }
}