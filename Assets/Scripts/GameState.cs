using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField]
    [Tooltip("A collection of locations for spawning players")]
    private Transform[] m_PlayerSpawnPoints;

    [SerializeField]
    [Tooltip("Make sure this is included in the NetworkManager's list of prefabs!")]
    private NetworkObject m_PlayerPrefab;

    private List<Transform> m_PlayerSpawnPointsList = null;

    private void Awake()
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnLoadEventCompleted;
    }

    void OnLoadEventCompleted(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if (loadSceneMode == LoadSceneMode.Single)
        {
            foreach (var kvp in NetworkManager.Singleton.ConnectedClients)
            {
                SpawnPlayer(kvp.Key, false);
            }
        }
    }

    void SpawnPlayer(ulong clientId, bool lateJoin)
    {
        Transform spawnPoint = null;

        if (m_PlayerSpawnPointsList == null || m_PlayerSpawnPointsList.Count == 0)
        {
            m_PlayerSpawnPointsList = new List<Transform>(m_PlayerSpawnPoints);
        }

        Debug.Assert(m_PlayerSpawnPointsList.Count > 0,
            $"PlayerSpawnPoints array should have at least 1 spawn points.");

        int index = Random.Range(0, m_PlayerSpawnPointsList.Count);
        spawnPoint = m_PlayerSpawnPointsList[index];
        m_PlayerSpawnPointsList.RemoveAt(index);

        var newPlayer = Instantiate(m_PlayerPrefab, Vector3.zero, Quaternion.identity);

        newPlayer.transform.position = spawnPoint.position;
        newPlayer.transform.rotation = spawnPoint.rotation;
    }
}
