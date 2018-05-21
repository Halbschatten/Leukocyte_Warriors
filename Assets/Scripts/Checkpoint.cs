using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint
{
    private Stack<Vector3> bossPosistion;
    private Stack<int> scoreOnCheckpoint;
    private Stack<int> checkpointNumber;

    public Checkpoint()
    {
        bossPosistion = new Stack<Vector3>();
        scoreOnCheckpoint = new Stack<int>();
        checkpointNumber = new Stack<int>();
    }

    public void AddCheckpoint(Vector3 bossPosition, int score, int checkpointNumber)
    {
        this.bossPosistion.Push(bossPosition);
        this.scoreOnCheckpoint.Push(score);
        this.checkpointNumber.Push(checkpointNumber);
    }
    public Vector3 GetLastCheckpointBossPosition()
    {
        return this.bossPosistion.Peek();
    }
    public int GetLastCheckpointScore()
    {
        return this.scoreOnCheckpoint.Peek();
    }
    public int GetLastCheckpointNumber()
    {
        return this.checkpointNumber.Peek();
    }

    public string GetCheckpointToString()
    {
        if (this.bossPosistion.Count == this.checkpointNumber.Count && this.checkpointNumber.Count == this.scoreOnCheckpoint.Count)
        {
            string returnString = "";
            Stack<Vector3> bossPosistionInverse = new Stack<Vector3>();
            Stack<int> scoreOnCheckpointInverse = new Stack<int>();
            Stack<int> checkpointNumberInverse = new Stack<int>();
            foreach (Vector3 position in this.bossPosistion)
            {
                bossPosistionInverse.Push(position);
            }
            foreach (int score in this.scoreOnCheckpoint)
            {
                scoreOnCheckpointInverse.Push(score);
            }
            foreach (int scoreNumber in this.checkpointNumber)
            {
                checkpointNumberInverse.Push(scoreNumber);
            }
            int originalBossPositionCount = this.bossPosistion.Count, originalScoreOnCheckpointCount = this.scoreOnCheckpoint.Count, originalCheckpointNumberCount = this.checkpointNumber.Count;
            for (int i = 0; i < originalBossPositionCount && i < originalScoreOnCheckpointCount && i < originalCheckpointNumberCount; i++) //The three stacks must have the same size, otherwise something went wrong!
            {
                returnString = returnString + checkpointNumberInverse.Pop() + " | ";
                returnString = returnString + scoreOnCheckpointInverse.Pop() + " | ";
                returnString = returnString + bossPosistionInverse.Pop();
                returnString = returnString + "\n";
            }
            return returnString;
        }
        else
        {
            //return this.bossPosistion.Count + "|" + this.checkpointNumber.Count + "|" + this.scoreOnCheckpoint.Count;
            return null;
        }
    }

    //public int[,] GetCheckpointNumberAndScoreArray()
    //{
    //    if (bossPosistion.Count == checkpointNumber.Count && checkpointNumber.Count == scoreOnCheckpoint.Count)
    //    {
    //        int[,] checkpointArray = new int[checkpointNumber.Count, 3];
    //        Stack<float> bossPosistionInverse = new Stack<float>();
    //        Stack<int> scoreOnCheckpointInverse = new Stack<int>();
    //        Stack<int> checkpointNumberInverse = new Stack<int>();
    //        foreach (float position in bossPosistion)
    //        {
    //            bossPosistionInverse.Push(position);
    //        }
    //        foreach (int score in scoreOnCheckpoint)
    //        {
    //            scoreOnCheckpointInverse.Push(score);
    //        }
    //        foreach (int scoreNumber in checkpointNumber)
    //        {
    //            checkpointNumberInverse.Push(scoreNumber);
    //        }
    //        int originalBossPositionCount = bossPosistion.Count, originalScoreOnCheckpointCount = scoreOnCheckpoint.Count, originalCheckpointNumberCount = checkpointNumber.Count;
    //        for (int i = 0; i < originalBossPositionCount && i < originalScoreOnCheckpointCount && i < originalCheckpointNumberCount; i++) //The three stacks must have the same size, otherwise something went wrong!
    //        {
    //            checkpointArray[i, 0] = checkpointNumberInverse.Pop();
    //            checkpointArray[i, 1] = scoreOnCheckpointInverse.Pop();
    //            checkpointArray[i, 2] = bossPosistionInverse.Pop(); -> float to int! cannot return 2 arrays!!
    //        }
    //        return checkpointArray;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}
}
