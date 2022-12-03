using UnityEngine;
using System.Collections;

public class RunnerChunk : MonoBehaviour
{
    [HideInInspector]public ChunkSpawner spawner;
    [HideInInspector] public GameObject obstacle;

    void FixedUpdate()
    {
        if (spawner.agent.position.x - spawner.destoryZone > transform.position.x)
            spawner.DestroyChunk(this);
    }

    public void SpawnObstacle(GameObject _obstacle)
    {
        if (obstacle != null)
        {
            Destroy(obstacle.gameObject);
        }
        obstacle = Instantiate(_obstacle, transform.position , Quaternion.identity);
        obstacle.transform.parent = this.transform;
    }
}