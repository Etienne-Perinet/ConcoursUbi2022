using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(EnemyManager enemyAI) : base(enemyAI) {}
    
    public override void RunCurrentState() => enemy.Idle();

    public override string ToString() => "Idle State";
}
