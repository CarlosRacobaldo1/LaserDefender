using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 1f;
    WaveConfigSO currentWave;
    [SerializeField] bool isLooping;
    [SerializeField] bool randomizeWaveOrder = true;
    void Start()
        {
            StartCoroutine (SpawnEnemyWaves());
        }

    public WaveConfigSO GetCurrentWave()
        {
            return currentWave;
        }
    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            List<WaveConfigSO> wavesToPlay = new List<WaveConfigSO>(waveConfigs);

            if (randomizeWaveOrder)
            {
                ShuffleWaves(wavesToPlay);
            }

            foreach (WaveConfigSO wave in wavesToPlay)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0, 0, 180), transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
    }

    void ShuffleWaves(List<WaveConfigSO> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }
}