using System.Collections.Generic;
using UnityEngine;
using HexGen;
using System.Linq;

namespace HexGenExampleGame1
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private BoardManager boardManager;
        private Unit unit;

        private List<Hex> path = new List<Hex>();

        [HideInInspector]
        public Hex targetHex;
       // private Vector3 targetPosition = Vector3.zero;
        private Unit closestPlayerUnit;

        private readonly float speed = 15f;
        private readonly float rotationSpeed = 10f;

        void Awake()
        {
            boardManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BoardManager>();
            unit = GetComponent<Unit>();
        }

        void FixedUpdate()
        {
            if (path.Count > 0)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetHex.WorldPos, step);

                //Quaternion _lookRotation = Quaternion.LookRotation(targetHex.WorldPos, Vector3.up);
                //transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);

                if (Vector3.Distance(transform.position, targetHex.WorldPos) < 0.001f)
                {
                    if (closestPlayerUnit.isActiveAndEnabled == true && closestPlayerUnit.OccupiedHex == targetHex)
                    {
                        boardManager.ExistingUnits.Remove(closestPlayerUnit);
                        boardManager.ExistingUnits.Remove(unit);
                        closestPlayerUnit.gameObject.SetActive(false);
                        gameObject.SetActive(false);
                    }

                    path.RemoveAt(path.Count - 1);

                    if (path.Count >= 1)
                    {
                        targetHex = path[path.Count - 1];
                        Quaternion rightToForward = Quaternion.Euler(0f, -90f, 0f);
                        Quaternion r = Quaternion.LookRotation(targetHex.WorldPos - transform.position, Vector3.up);
                        transform.rotation = rightToForward * r;
                    }
                    else
                    {
                        unit.OccupiedHex = targetHex;
                        unit.Moved = true;
                    }
                }
            }
        }

        public void PerformAction()
        {
            closestPlayerUnit = FindClosestPlayerUnit();

            if (closestPlayerUnit == null)
                return;

            path = FindPath(closestPlayerUnit);

            targetHex = path[path.Count - 1];
        }

        private List<Hex> FindPath(Unit closestPlayerUnit)
        {
            AStarPathfinding pathfinding = (AStarPathfinding)ScriptableObject.CreateInstance(typeof(AStarPathfinding));

            List<Hex> hexPath = pathfinding.Search(unit.OccupiedHex, closestPlayerUnit.OccupiedHex);
            List<Hex> unitRange = HexRange.GetHexesInRange(unit.OccupiedHex, unit);

            List<Hex> resultingPath = hexPath.Intersect(unitRange)
                                     .OrderByDescending(x => Hex.GetDistance(unit.OccupiedHex, x))
                                     .ToList();

            return resultingPath;
        }

        private Unit FindClosestPlayerUnit()
        {
            Unit closestPlayerUnit = null;
            float lowestDistance = float.MaxValue;
            foreach (Unit otherUnit in boardManager.GetPlayerUnits())
            {
                if (Hex.GetDistance(unit.OccupiedHex, otherUnit.OccupiedHex) < lowestDistance)
                {
                    lowestDistance = Hex.GetDistance(unit.OccupiedHex, otherUnit.OccupiedHex);
                    closestPlayerUnit = otherUnit;
                }
            }

            if (closestPlayerUnit == null)
                Debug.LogWarning("EnemyBehaviour haven't found any player units");

            return closestPlayerUnit;
        }
    }
}