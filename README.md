# ScoreManager_and_Leaderboard
scoreboard with a local leaderboard

## Usage

creates a Scoreboard

- With each target that should be giving a score, create a variable called ScoreValue, and add the lines

,,,C#
ScoreManager.Multiplier++;
ScoreManager.CalculateScore(ScoreValue);
,,,

### For Example

on an enemy getting attacked by a weapon from the player and it should add to a combo per hit, this could be added

,,,C#

public void HurtEnemy(Collider other)
    {
        //adds to multiplier
        ScoreManager.Multiplier++;
        //adds to score
        ScoreManager.CalculateScore(ScoreValue);

        Enemy.GetHurt(f_Damage);
        EnemyHitAnim();
    }
,,,
