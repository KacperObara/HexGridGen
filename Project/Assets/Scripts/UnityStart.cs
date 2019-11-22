using UnityEngine;

namespace HexGen
{
    [ExecuteInEditMode]
    public class UnityStart : MonoBehaviour
    {
        void Awake()
        {
            GameObject.FindGameObjectWithTag("Grid").GetComponent<Generator>().Generate();
        }
    }
}
