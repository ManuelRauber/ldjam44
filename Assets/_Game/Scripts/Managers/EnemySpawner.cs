using LdJam44.Enemies;
using LdJam44.Extensions;
using LdJam44.Managers.Lanes;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Managers
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("References")]
        public GameObject[] EnemyPrefabs;

        [Header("Variables")]
        public float SpawnRate = 0.5f;

        public bool IsSpawningEnabled = true;
        public FloatVariable DriverXPosition;
        public Vector2 SpawnOffset = new Vector2(30, 0);
        public LanesVariable Lanes;

        private float _timeToNextSpawn;

        private void Start()
        {
            _timeToNextSpawn = Time.time;
        }

        private void Update()
        {
            if (!IsSpawningEnabled || _timeToNextSpawn > Time.time)
            {
                return;
            }

            SpawnEnemy();
            _timeToNextSpawn = Time.time + SpawnRate;
        }

        private void SpawnEnemy()
        {
            var laneNumber = Random.Range(0, Lanes.Value.Length);
            var lane = Lanes.Value[laneNumber];
            
            var enemy = Instantiate(
                EnemyPrefabs.PickOne(),
                new Vector3(SpawnOffset.x + DriverXPosition, SpawnOffset.y, lane.Position.z),
                Quaternion.identity
            );

            enemy.transform.SetParent(transform);

            var enemyController = enemy.GetComponent<EnemyController>();
            
            enemyController.SwitchLane(laneNumber);

            if (Lanes.Value[laneNumber].Reverse)
            {
                enemy.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        public void GameOver()
        {
            IsSpawningEnabled = false;
        }
    }
}
