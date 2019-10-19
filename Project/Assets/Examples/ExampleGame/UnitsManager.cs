using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitsManager : ScriptableObject
{
    public GameObject playerUnit;
    public GameObject enemyUnit;

    public List<Unit> playerUnits;
    public List<Unit> enemyUnits;
}
