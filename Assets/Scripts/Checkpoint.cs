using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint
{
    private Stack<int> scoreOnCheckpoint;
    private Stack<int> checkpointNumber;

    public Checkpoint()
    {
        scoreOnCheckpoint = new Stack<int>();
        checkpointNumber = new Stack<int>();
    }

    public void AddCheckpoint(int score, int checkpointNumber)
    {
        scoreOnCheckpoint.Push(score);
        this.checkpointNumber.Push(checkpointNumber);
    }
    public int GetLastCheckpointScore()
    {
        return scoreOnCheckpoint.Peek();
    }
    public int GetLastCheckpointNumber()
    {
        return checkpointNumber.Peek();
    }

    public string GetCheckpointToString()
    {
        if (checkpointNumber.Count == scoreOnCheckpoint.Count)
        {
            string returnString = "";
            Stack<int> scoreOnCheckpointInverse = new Stack<int>();
            Stack<int> checkpointNumberInverse = new Stack<int>();
            foreach (int score in scoreOnCheckpoint)
            {
                scoreOnCheckpointInverse.Push(score);
            }
            foreach (int scoreNumber in checkpointNumber)
            {
                checkpointNumberInverse.Push(scoreNumber);
            }
            int originalScoreOnCheckpointCount = scoreOnCheckpoint.Count, originalCheckpointNumberCount = checkpointNumber.Count;
            for (int i = 0; i < originalScoreOnCheckpointCount && i < originalCheckpointNumberCount; i++) //The two stacks must have the same size, otherwise something went wrong!
            {
                returnString = returnString + checkpointNumberInverse.Pop() + " | ";
                returnString = returnString + scoreOnCheckpointInverse.Pop();
                returnString = returnString + "\n";
            }
            return returnString;
        }
        else
        {
            return null;
        }
    }

    public int[,] GetCheckpointNumberAndScoreArray()
    {
        if (checkpointNumber.Count == scoreOnCheckpoint.Count)
        {
            int[,] checkpointArray = new int[checkpointNumber.Count, 2];
            Stack<int> scoreOnCheckpointInverse = new Stack<int>();
            Stack<int> checkpointNumberInverse = new Stack<int>();
            foreach (int score in scoreOnCheckpoint)
            {
                scoreOnCheckpointInverse.Push(score);
            }
            foreach (int scoreNumber in checkpointNumber)
            {
                checkpointNumberInverse.Push(scoreNumber);
            }
            int originalScoreOnCheckpointCount = scoreOnCheckpoint.Count, originalCheckpointNumberCount = checkpointNumber.Count;
            for (int i = 0; i < originalScoreOnCheckpointCount && i < originalCheckpointNumberCount; i++) //The two stacks must have the same size, otherwise something went wrong!
            {
                checkpointArray[i, 0] = checkpointNumberInverse.Pop();
                checkpointArray[i, 1] = scoreOnCheckpointInverse.Pop();
            }
            return checkpointArray;
        }
        else
        {
            return null;
        }
    }
}
