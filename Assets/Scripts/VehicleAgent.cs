using EVP;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class VehicleAgent : Agent
{
    [SerializeField] private CheckpointManager m_CheckpointManager;
    [SerializeField] private ChunkSpawner m_ChunkSpawner;
    private Rigidbody m_Rigidbody;
    private ExperimentCounter m_ExperimentCounter;
    private VehicleController m_VehicleController;
    private Vector3 m_StartPos;
    private Quaternion m_StartRot;

    public override void Initialize()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Random.value > 0.5f ? 3f : -3f);
        m_ExperimentCounter = FindObjectOfType<ExperimentCounter>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_VehicleController = GetComponent<VehicleController>();
        m_StartPos = transform.position;
        m_StartRot = transform.rotation;

        m_CheckpointManager.OnPlayerCorrectCheckpoint.AddListener(OnCorrectCheckpoint);
        m_CheckpointManager.OnPlayerWrongCheckpoint.AddListener(OnWrongCheckpoint);


        m_VehicleController.throttleInput = 1f;
    }

    private void OnCorrectCheckpoint()
    {
        AddReward(+1f);
        Debug.Log("<color=green>CORRECT CHECKPOINT!</color>");
    }
    private void OnWrongCheckpoint()
    {
        AddReward(-0.5f);
        Debug.Log("<color=red>WRONG CHECKPOINT!</color>");
    }

    public override void OnEpisodeBegin()
    {
        transform.position = m_StartPos;
        transform.rotation = m_StartRot;

        m_Rigidbody.velocity = Vector3.zero;

        m_ChunkSpawner.ResetChunks();
        m_VehicleController.ResetVehicle();
        m_CheckpointManager.ResetCheckpoints();

        m_ExperimentCounter.IncrementAttempts();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // 1 observation
        sensor.AddObservation(StepCount / (float)MaxStep);

        //Vector3 checkpointForward = m_CheckpointManager.GetNextCheckpoint().transform.right;
        //float dotProduct = Vector3.Dot(transform.right, checkpointForward);

        // 1 observation
        sensor.AddObservation(m_CheckpointManager.GetNextCheckpoint().transform.position.x - transform.position.x);


        // 1 observation
        sensor.AddObservation(transform.localPosition.x);
        // 1 observation
        sensor.AddObservation(transform.localPosition.z);

        // 1 observation
        sensor.AddObservation(m_Rigidbody.velocity.x);
        // 1 observation
        sensor.AddObservation(m_Rigidbody.velocity.z);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        AddReward(-1f / MaxStep);
        MoveAgent(actions);
    }

    private void MoveAgent(ActionBuffers actions)
    {
        //float throttle = 0f;

        //switch (actions.DiscreteActions[0])
        //{
        //    case 0: throttle = 0f; break;
        //    case 1: throttle = +1f; break;
        //    case 2: throttle = -1f; break;
        //}

        m_VehicleController.steerInput = actions.ContinuousActions[0];
        m_VehicleController.throttleInput = actions.ContinuousActions[1];
        //m_VehicleController.brakeInput = actions.ContinuousActions[2];
    }

    //For testing only
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
        continuousActionsOut[1] = Input.GetAxisRaw("Vertical");
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle")) 
        {
            SetReward(-0.1f);
            EndEpisode();
            Debug.Log("<color=red>Obstacle hit.</color>");
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Lane"))
        {
            AddReward(-0.01f);
            //Debug.Log("<color=red>Staying on lane.</color>");
        }
    }

}
