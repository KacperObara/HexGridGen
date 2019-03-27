using UnityEngine;

namespace HexGen
{
    public class HexInfo : MonoBehaviour
    {
        public const float OuterRadius = 10f;
        public const float InnerRadius = 8.6602540378f; // http://www.drking.org.uk/hexagons/misc/ratio.html

        public static readonly Vector3[] vertices = {
        new Vector3(0f, 0f, OuterRadius),
        new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(0f, 0f, -OuterRadius),
        new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
        new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),
        new Vector3(0f, 0f, OuterRadius)
        };
    }
}
