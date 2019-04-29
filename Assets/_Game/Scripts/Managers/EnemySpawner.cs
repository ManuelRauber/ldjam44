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

        public GameObject BloodPackPrefab;

        [Header("Variables")]
        public float SpawnRate = 0.5f;

        public float BloodPackSpawnRate = 5;

        public bool IsSpawningEnabled;
        public FloatVariable DriverXPosition;
        public Vector2 SpawnOffset = new Vector2(30, 0);
        public LanesVariable Lanes;
        public bool IsIntroMode;
        public float DestroyEnemyInIntroModeAfterSeconds = 15f;

        private float _timeToNextSpawn;
        private float _timeToNextBloodPackSpawn;

        private void Start()
        {
            _timeToNextSpawn = Time.time;
            _timeToNextBloodPackSpawn = Time.time;
        }

        private void Update()
        {
            if (!IsSpawningEnabled)
            {
                return;
            }

            if (Time.time > _timeToNextSpawn)
            {
                SpawnEnemy();
                _timeToNextSpawn = Time.time + SpawnRate;
            }

            if (Time.time > _timeToNextBloodPackSpawn)
            {
                SpawnBloodPack();
                _timeToNextBloodPackSpawn = Time.time + BloodPackSpawnRate;
            }
        }

        private void SpawnBloodPack()
        {
            var laneNumber = Random.Range(0, Lanes.Value.Length);
            var lane = Lanes.Value[laneNumber];

            var bloodPack = Instantiate(
                EnemyPrefabs.PickOne(),
                new Vector3(SpawnOffset.x + DriverXPosition, SpawnOffset.y, lane.Position.z),
                Quaternion.identity
            );

            bloodPack.transform.SetParent(transform);
            Destroy(bloodPack, 20);
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
            enemyController.IsIntroMode = IsIntroMode;
            enemyController.SwitchLane(laneNumber);

            if (Lanes.Value[laneNumber].Reverse)
            {
                enemy.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (IsIntroMode)
            {
                enemy.transform.position = new Vector3(-SpawnOffset.x, SpawnOffset.y, lane.Position.z);
            }

            if (IsIntroMode)
            {
                Destroy(enemy.gameObject, DestroyEnemyInIntroModeAfterSeconds);
            }
        }

        public void GameOver()
        {
            IsSpawningEnabled = false;
        }

        public void StartupSequenceDone()
        {
            IsSpawningEnabled = true;
        }
    }
}
