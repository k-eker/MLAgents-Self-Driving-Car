using AmazingAssets.CurvedWorld.Example;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] m_Obstacles;
    [SerializeField] [Range(0, 1)] float m_ObstacleSpawnRate = 0.5f;
    RunnerChunk[] m_AllChunks;
    //[SerializeField] private CarSpawner m_CarSpawner;
    public Transform agent;
    public float destoryZone = 300;
    private const float m_ChunkSize = 70;
    RunnerChunk m_LastChunk;

    private void Awake()
    {
        m_AllChunks = GetComponentsInChildren<RunnerChunk>();

        for (int i = 0; i < m_AllChunks.Length; i++)
        {
            m_AllChunks[i].spawner = this;
        }

    }

    public void ResetChunks()
    {
        for (int i = 0; i < m_AllChunks.Length; i++)
        {
            m_AllChunks[i].transform.position = new Vector3(i * m_ChunkSize, 0, transform.position.z);

            //if(m_AllChunks[i].obstacle != null)
            //    Destroy(m_AllChunks[i].obstacle);

            if (i > 1)
            {
                SpawnRandomObstacle(m_AllChunks[i]);
            }
            else
            {
                Destroy(m_AllChunks[i].obstacle.gameObject);
            }

            m_LastChunk = m_AllChunks[i];
        }
        //m_CarSpawner.transform.position = lastChunk.transform.position;
    }

    public void DestroyChunk(RunnerChunk thisChunk)
    {
        SpawnRandomObstacle(thisChunk);

        Vector3 newPos = m_LastChunk.transform.position;
        newPos.x += m_ChunkSize;


        m_LastChunk = thisChunk;
        m_LastChunk.transform.position = newPos;
        //m_CarSpawner.transform.position = lastChunk.transform.position;
    }


    private void SpawnRandomObstacle(RunnerChunk targetChunk)
    {
        if (Random.value < m_ObstacleSpawnRate)
        {
            targetChunk.SpawnObstacle(m_Obstacles[Random.Range(0, m_Obstacles.Length)]);
        }
    }
}