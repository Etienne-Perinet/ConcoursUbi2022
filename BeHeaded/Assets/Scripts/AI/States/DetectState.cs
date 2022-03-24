using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectState : EnemyState
{
    private float timer;

    public DetectState(EnemyManager enemyAI) : base(enemyAI) 
    {
        timer = 0;
    } 
    
    public override void RunCurrentState() 
    {
        if(enemy.IsPlayerInArea() && enemy.DetectionTime > 0)  
        {
            timer += Time.deltaTime;
            enemy.Detect((timer*100)/enemy.DetectionTime);

            if(timer >= enemy.DetectionTime) 
            {
                EndState();
                RunNextState();
            }
        }
        else
        {
            EndState();
            enemy.InitializeState();
        }
            
    }

    void EndState()
    {
        timer = 0;
        enemy.ReinitializeBar();
        enemy.NavMesh.isStopped = false;
    }

    public override string ToString() => "Detection State";
}
