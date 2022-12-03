using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointManager : MonoBehaviour
{
    public UnityEvent OnPlayerCorrectCheckpoint;
    public UnityEvent OnPlayerWrongCheckpoint;

    private List<Checkpoint> m_Checkpoints;
    private int m_NextCheckpointIndex = 0;
    private Checkpoint m_LastCheckpoint;
    private void Awake()
    {
        Checkpoint[] m_CheckpointArray = GetComponentsInChildren<Checkpoint>();
        m_Checkpoints = new List<Checkpoint>(m_CheckpointArray);
        for (int i = 0; i < m_Checkpoints.Count; i++)
        {
            m_Checkpoints[i].SetCheckpointManager(this);
        }

    }
    public void PlayerTriggerCheckpoint(Checkpoint checkpoint)
    {
        if (m_Checkpoints.IndexOf(checkpoint) == m_NextCheckpointIndex)
        {
            //Correct checkpoint
            m_NextCheckpointIndex = (m_NextCheckpointIndex + 1) % m_Checkpoints.Count;

            OnPlayerCorrectCheckpoint.Invoke();
            m_LastCheckpoint = checkpoint;
        }
        else if(m_LastCheckpoint != checkpoint)
        {
            //Wrong checkpoint
            OnPlayerWrongCheckpoint.Invoke();
        }
    }

    public Checkpoint GetNextCheckpoint()
    {
        return m_Checkpoints[m_NextCheckpointIndex];
    }

    public void ResetCheckpoints()
    {
        m_NextCheckpointIndex = 0;
        m_LastCheckpoint = null;
    }
}
