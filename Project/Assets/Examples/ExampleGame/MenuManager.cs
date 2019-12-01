using UnityEngine;
using TMPro;
using UnityEngine.UI;
using HexGen;
using System;

namespace HexGenExampleGame1
{
    public class MenuManager : MonoBehaviour
    {
        public TMP_InputField WidthField;
        public TMP_InputField HeightField;
        public TMP_InputField ScaleField;
        public TMP_InputField OctavesField;
        public Slider PersistanceSlider;
        public TextMeshProUGUI PersistanceText;
        public TMP_InputField LacunarityField;
        public TMP_InputField SeedField;
        public Toggle AutoGenerateToggle;
        public Button GenerateButton;
        public Button AcceptButton;

        [SerializeField]
        private MapSettings mapSettings;

        [SerializeField]
        private Generator generator;

        public void UpdateSliderText()
        {
            PersistanceText.text = $"Persistance: {PersistanceSlider.value}";
        }

        public void CheckUpdateWorld()
        {
            if (AutoGenerateToggle.isOn)
                UpdateWorld();
        }

        public void UpdateWorld()
        {
            /// Values are restricted in order to avoid float frequency in DefaultPerlinNoiseGenerator from overflow (NaN)

            int WorldWidth = Convert.ToInt32(WidthField.text);
            if (WorldWidth > 150)
                WorldWidth = 150;
            if (WorldWidth < 1)
                WorldWidth = 1;
            mapSettings.WorldWidth = WorldWidth;
            WidthField.text = WorldWidth.ToString();

            int WorldHeight = Convert.ToInt32(HeightField.text);
            if (WorldHeight > 150)
                WorldHeight = 150;
            if (WorldHeight < 1)
                WorldHeight = 1;
            mapSettings.WorldHeight = WorldHeight;
            HeightField.text = WorldHeight.ToString();

            // there is a problem with converting floats, and this is a fix
            float Scale;
            if (float.TryParse(ScaleField.text, out Scale))
            {
                if (Scale > 40)
                    Scale = 40f;
                if (Scale <= 0)
                    Scale = 0.01f;

                mapSettings.Scale = Scale;
                ScaleField.text = Scale.ToString();
            }

            int octaves = Convert.ToInt32(OctavesField.text);
            if (octaves > 30)
                octaves = 30;
            if (octaves < 1)
                octaves = 1;
            mapSettings.Octaves = octaves;
            OctavesField.text = octaves.ToString();

            mapSettings.Persistance = PersistanceSlider.value;

            // there is a problem with converting floats, and this is a fix
            float lacunarity;
            if (float.TryParse(LacunarityField.text, out lacunarity))
            {
                if (lacunarity > 5f)
                    lacunarity = 5f;
                if (lacunarity < 1f)
                    lacunarity = 1f;

                mapSettings.Lacunarity = lacunarity;
                LacunarityField.text = lacunarity.ToString();
            }

            mapSettings.Seed = Convert.ToInt32(SeedField.text);

            generator.Generate();
        }
    }
}
