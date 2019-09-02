using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexGen
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Generator : MonoBehaviour
    {
        public GridGen GridGen;
        public NoiseGen NoiseGen;
        public MeshGen MeshGen;

        public HexGrid HexGrid;
        public NoiseSettings NoiseSettings;

        public void Generate()
        {
            GridGen.Initialize(this);
            NoiseGen.Initialize(this);
            MeshGen.Initialize(this);

            GridGen.Generate();
            NoiseGen.Generate();
            MeshGen.Generate();
        }
    }
}
