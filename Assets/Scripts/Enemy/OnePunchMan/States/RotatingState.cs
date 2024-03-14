using StatePattern.Enemy;
using UnityEngine;

public class RotatingState : IStates
{
    public OnePunchManController Owner { get; set; }
    private OnePunchManStateMachine onePunchManStateMachine;
    private float targetRotation;
    public RotatingState(OnePunchManStateMachine onePunchManStateMachine) => this.onePunchManStateMachine = onePunchManStateMachine;

    public void OnStateEnter() => targetRotation = (Owner.Rotation.eulerAngles.y + 180) % 360;

    public void Update() 
    {
        Owner.SetRotation(CalculateRotation());
        if (IsRotationComplete())
        {
            onePunchManStateMachine.ChangeState(OnePunchManStates.IDLE);
        }
    }

    public void OnStateExit() => targetRotation = 0;

    private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Owner.Rotation.eulerAngles.y, targetRotation, Owner.Data.RotationSpeed * Time.deltaTime);
    private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Owner.Data.RotationThreshold;
}