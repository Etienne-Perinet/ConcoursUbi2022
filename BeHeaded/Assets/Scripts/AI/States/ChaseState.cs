using UnityEngine;

public class ChaseState : EnemyState
{
    public ChaseState(EnemyManager enemyAI) : base(enemyAI) {}

    public override void RunCurrentState() 
    {
        if(enemy.IsPlayerInArea())
            enemy.Chase();
        else
            RunNextState();
    }

    public override void RunNextState()
    {
        enemy.ChangeTarget(enemy.LastPlayerPosition);
        enemy.State = nextState;
    }

    public override string ToString() => "Chase State";
}
