using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Vector3 bossPosition;
    public List<Vector3> checkpointPositions;


    public Vector3 GetCurrentBossPosition()
    {
        return this.bossPosition;
    }

    public List<Vector3> GetCheckpointPositions()
    {
        return this.checkpointPositions;
    }

    public Stage()
    {
        bossPosition = Vector3.zero;
        checkpointPositions = new List<Vector3>();
    }

    public void SetNextStage(Vector3 bossPosition, List<Vector3> checkpointPositions)
    {
        this.bossPosition = bossPosition;
        this.checkpointPositions = new List<Vector3>(checkpointPositions);
    }
}
