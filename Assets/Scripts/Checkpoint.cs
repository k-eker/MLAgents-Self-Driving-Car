using EVP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager m_CheckpointManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<VehicleController>(out VehicleController vc))
        {
            m_CheckpointManager.PlayerTriggerCheckpoint(this);
        }
    }

    public void SetCheckpointManager(CheckpointManager cm)
    {
        this.m_CheckpointManager = cm;
    }

}
