using StatePattern.Enemy;
using System;
using UnityEngine;

public class IdleState : IStates
{
    public OnePunchManController Owner { get; set; }
    private OnePunchManStateMachine onePunchManStateMachine;
    private float timer;
    public IdleState(OnePunchManStateMachine onePunchManStateMachine) => this.onePunchManStateMachine = onePunchManStateMachine;

    public void OnStateEnter() => ResetTimer();

    public void Update() 
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            onePunchManStateMachine.ChangeState(OnePunchManStates.ROTATING);
        }
    }

    public void OnStateExit() => timer = 0;

    private void ResetTimer() => timer = Owner.Data.IdleTime;
}