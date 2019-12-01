using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;
using System.Linq;

namespace HexGenExampleGame1
{
    public class EnemySpawner : MonoBehaviour
    {
        private BoardManager boardManager;

        public MapData mapData;
        public MapSettings mapSettings;

        [Space]
        public int enemySpawners;

        void Awake()
        {
            boardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
        }

        public void SpawnEnemies()
        {
            foreach (GameObject spawner in boardManager.EnemySpawners)
            {
                Unit enemyUnit = Instantiate(boardManager.EnemyPrefab, spawner.transform.position, Quaternion.identity).GetComponent<Unit>();
                enemyUnit.Initialize(spawner.GetComponent<Entity>().OccupiedHex, Faction.Enemy);
                boardManager.ExistingUnits.Add(enemyUnit);
            }

            foreach (Unit enemyUnit in boardManager.GetEnemyUnits())
            {
                enemyUnit.GetComponent<EnemyBehaviour>().PerformAction();
            }
        }

        public void CreateEnemySpawner()
        {
            // Because we don't need to choose algorithm //TO DO: dont be stupid and change it
            AStarPathfinding pathfinding = (AStarPathfinding)ScriptableObject.CreateInstance(typeof(AStarPathfinding));

            List<Hex> spawnerHexes = new List<Hex>();

            List<Hex> borderHexes = mapData.Hexes.Where(x => x.LocalPos.x == 0 || x.LocalPos.y == 0
                                                          || x.LocalPos.x == mapSettings.WorldWidth - 1 || x.LocalPos.y == mapSettings.WorldHeight - 1).ToList();


            while(spawnerHexes.Count < enemySpawners)
            {
                int randomIndex = Random.Range(0, borderHexes.Count);

                if (spawnerHexes.Contains(borderHexes[randomIndex]))
                    continue;

                foreach (Unit unit in boardManager.GetPlayerUnits())
                {
                    if (pathfinding.Search(borderHexes[randomIndex], unit.OccupiedHex).Count > 0)
                    {
                        spawnerHexes.Add(borderHexes[randomIndex]);
                        break;
                    }
                }
            }


            foreach (Hex hex in spawnerHexes)
            {
                GameObject spawner = Instantiate(boardManager.EnemySpawnerPrefab, hex.WorldPos, Quaternion.identity);
                spawner.GetComponent<Entity>().OccupiedHex = hex;
                boardManager.EnemySpawners.Add(spawner);
            }
        }
    }
}
