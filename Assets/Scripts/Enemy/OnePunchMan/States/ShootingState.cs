using StatePattern.Enemy;
using StatePattern.Main;
using StatePattern.Player;
using UnityEngine;

public class ShootingState : IStates
{
    private OnePunchManStateMachine onePunchManStateMachine;
    private PlayerController target;
    private float shootTimer;
    public ShootingState(OnePunchManStateMachine onePunchManStateMachine)
    {
        this.onePunchManStateMachine = onePunchManStateMachine;
    }

    public OnePunchManController Owner { get; set; }

    public void OnStateEnter() 
    {
        SetPlayerController();
        shootTimer = 0;
    }

    public void Update() 
    {
        Quaternion desiredRotation = CalculateRotationTowardsPlayer();
        Owner.SetRotation(RotateTowards(desiredRotation));

        if (IsFacingPlayer(desiredRotation))
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = Owner.Data.RateOfFire;
                Owner.Shoot();
            }
        }
    }

    public void OnStateExit() => target = null;

    private void SetPlayerController() => target = GameService.Instance.PlayerService.GetPlayer();

    private Quaternion CalculateRotationTowardsPlayer()
    {
        Vector3 directionToPlayer = target.Position - Owner.Position;
        directionToPlayer.y = 0f;
        return Quaternion.LookRotation(directionToPlayer, Vector3.up);
    }

    private Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(Owner.Rotation, desiredRotation, Owner.Data.RotationSpeed / 30 * Time.deltaTime);

    private bool IsFacingPlayer(Quaternion desiredRotation) => Quaternion.Angle(Owner.Rotation, desiredRotation) < Owner.Data.RotationThreshold;
}