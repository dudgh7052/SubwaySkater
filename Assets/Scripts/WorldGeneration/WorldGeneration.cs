using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    // Gameplay
    float m_chunkSpawnZ;
    Queue<Chunk> m_activeChunks = new Queue<Chunk>();
    List<Chunk> m_chunkPool = new List<Chunk>();

    // Configurable fields
    [SerializeField] int m_firstChuckSpawnPosition = -10;
    [SerializeField] int m_chunkOnScreen = 5;
    [SerializeField] float m_despawnDistance = 5.0f;

    [SerializeField] List<GameObject> m_chunkPrefab;
    [SerializeField] Transform m_cameraTransform;

    void Awake()
    {
        ResetWorld();
    }

    void Start()
    {
        // Check if we have an empty chunkPrefab list
        if (m_chunkPrefab.Count == 0)
        {
            Debug.LogError("No chunk prefab foudn the world generator, please assign some chunks!");
            return;
        }

        // Try to assign the cameraTransform
        if (!m_cameraTransform)
        {
            m_cameraTransform = Camera.main.transform;
            Debug.Log("We've assingned m_cameraTransform automaticly to the Camera.main");
        }
    }

    public void ScanPosition()
    {
        float _cameraZ = m_cameraTransform.position.z;
        Chunk _lastChunk = m_activeChunks.Peek();

        if (_cameraZ >= _lastChunk.transform.position.z + _lastChunk.m_chunkLength + m_despawnDistance)
        {
            SpawnNewChuck();
            DeleteLastChunk();
        }
    }

    void SpawnNewChuck()
    {
        // ������ �ε��� �̱�
        int _randomIndex = Random.Range(0, m_chunkPrefab.Count);

        // ûũ Ǯ���� Ȱ��ȭ�� �ȵ��ְų� ûũ �����ո���Ʈ���� �̸��� ���� �͸� �־��ش�.
        Chunk _chunk = m_chunkPool.Find(x => !x.gameObject.activeSelf && x.name == m_chunkPrefab[_randomIndex].name + "(Clone)");

        // Ǯ���� ������ ûũ�� ���� ���
        if (!_chunk)
        {
            GameObject _go = Instantiate(m_chunkPrefab[_randomIndex], transform);
            _chunk = _go.GetComponent<Chunk>();
        }

        // Place the object, and show it
        _chunk.transform.position = new Vector3(0, 0, m_chunkSpawnZ); // ���� �����ؾ��ϴ� ûũ ��ġ ����
        m_chunkSpawnZ += _chunk.m_chunkLength; // ûũ ��ũ��Ʈ���� ���� ��������

        // Store the value, to reuse in our pool
        m_activeChunks.Enqueue(_chunk); // Ȱ��ȭ ûũ �ֱ�
        _chunk.ShowChunk();
    }

    void DeleteLastChunk()
    {
        Chunk _chunk = m_activeChunks.Dequeue();
        _chunk.HideChunk();
        m_chunkPool.Add(_chunk);
    }

    public void ResetWorld()
    {
        // Reset the ChunkSpawn Z
        m_chunkSpawnZ = m_firstChuckSpawnPosition;

        for (int i = m_activeChunks.Count; i != 0; i--)
            DeleteLastChunk();

        for (int i = 0; i < m_chunkOnScreen; i++)
            SpawnNewChuck();
    }
}
