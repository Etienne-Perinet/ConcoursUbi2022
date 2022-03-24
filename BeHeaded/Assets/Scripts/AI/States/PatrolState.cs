using UnityEngine;

public class PatrolState : EnemyState
{
    public PatrolState(EnemyManager enemyAI) : base(enemyAI) {}
    
    public override void RunCurrentState() 
    {
        if(enemy.IsPlayerInArea())  
            RunNextState();
        else
            enemy.Patrol();
    }

    public override string ToString() => "Patrol State";
}
