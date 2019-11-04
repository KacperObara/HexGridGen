using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HexGen
{
    /// <summary>
    /// The same as UnityEvent, but allows to use parameter of type "Hex" in Invoke method
    /// </summary>
    [System.Serializable]
    public class UnityEventHex : UnityEvent<Hex> { }

    public class InputHandler : MonoBehaviour
    {
        public UnityEventHex HexClickAction;

        void OnMouseUp()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Generator>())
                {
                    Hex clickedHex = HexData.PixelToHex(hit.point, hit.transform.GetComponent<Generator>().MapData.Hexes);
                    HexClickAction.Invoke(clickedHex);
                }
            }
        }
    }
}
