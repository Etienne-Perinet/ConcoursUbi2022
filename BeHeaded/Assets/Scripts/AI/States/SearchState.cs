using UnityEngine;

public class SearchState : EnemyState
{
    private float timer;

    public SearchState(EnemyManager enemyAI) : base(enemyAI) 
    {
        timer = enemy.SearchTime;
    }
    
    public override void RunCurrentState() 
    {
        if(enemy.IsPlayerInArea())
        {
            timer = enemy.SearchTime;
            enemy.RedetectPlayer();
        }
        else
        {
            timer -= Time.deltaTime;
            
            if(timer <= 0) 
            {
                timer = enemy.SearchTime;
                Debug.Log("End of search timer");
                RunNextState();
            }
            else 
                enemy.Search();
        }
    }

    public override string ToString() => "Search State";
}
