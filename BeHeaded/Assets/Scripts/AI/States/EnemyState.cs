using UnityEngine;

public abstract class EnemyState 
{
    protected EnemyManager enemy;
    protected EnemyState nextState;
    public EnemyState NextState { set => this.nextState = value; }

    public EnemyState(EnemyManager enemy) => this.enemy = enemy;

    public abstract void RunCurrentState(); // Normal Behaviour

    public virtual void RunNextState() => enemy.State = nextState;
}
