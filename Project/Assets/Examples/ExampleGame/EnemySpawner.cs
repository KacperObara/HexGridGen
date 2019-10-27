using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;
using System.Linq;

namespace HexGenExampleGame1
{
    public class EnemySpawner : MonoBehaviour
    {
        public UnitsManager unitsManager;
        public MapData mapData;
        public MapSettings mapSettings;
        public GameObject EnemySpawnerPrefab;

        [Space]
        public int enemySpawners;

        public void CreateEnemySpawner()
        {
            AStarPathfinding pathfinding = (AStarPathfinding)ScriptableObject.CreateInstance(typeof(AStarPathfinding));

            List<Hex> spawnerHexes = new List<Hex>();

            List<Hex> borderHexes = mapData.Hexes.Where(x => x.LocalPos.x == 0 || x.LocalPos.y == 0
                                                          || x.LocalPos.x == mapSettings.WorldWidth - 1 || x.LocalPos.y == mapSettings.WorldHeight - 1).ToList();


            while(spawnerHexes.Count < enemySpawners)
            {
                int randomIndex = Random.Range(0, borderHexes.Count);

                if (spawnerHexes.Contains(borderHexes[randomIndex]))
                    continue;

                foreach (Unit unit in unitsManager.playerUnits)
                {
                    if (pathfinding.Search(borderHexes[randomIndex], unit.occupiedHex).Count > 0)
                    {
                        spawnerHexes.Add(borderHexes[randomIndex]);
                        break;
                    }
                }
            }


            foreach (Hex hex in spawnerHexes)
            {
                Instantiate(EnemySpawnerPrefab, hex.WorldPos, Quaternion.identity);
            }
        }
    }
}
