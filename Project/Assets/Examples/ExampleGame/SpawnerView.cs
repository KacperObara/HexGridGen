using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HexGenExampleGame1
{
    public class SpawnerView : MonoBehaviour
    {
        [SerializeField]
        private PlayerSpawner playerSpawner;

        [SerializeField]
        private TextMeshProUGUI unitsLeftText;

        private void Start()
        {
            unitsLeftText.text = $"Units left: {playerSpawner.PlayerTanksLeft}";
        }

        public void UpdateUnitsLeft()
        {
            unitsLeftText.text = $"Units left: {playerSpawner.PlayerTanksLeft}";
        }
    }
}
