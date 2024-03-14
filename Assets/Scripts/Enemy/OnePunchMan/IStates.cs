using StatePattern.Enemy;

public interface IStates
{
    public OnePunchManController Owner { get; set; }
    public void OnStateEnter();
    public void Update();
    public void OnStateExit();
}