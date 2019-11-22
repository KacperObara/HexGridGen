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
        public UnityEventHex HexLeftClickAction;
        public UnityEventHex HexRightClickAction;

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Hex selectedHex = MouseInputToHex();
                if (selectedHex != null)
                    HexLeftClickAction.Invoke(selectedHex);
            }
            else if(Input.GetMouseButtonUp(1))
            {
                Hex selectedHex = MouseInputToHex();
                if (selectedHex != null)
                    HexRightClickAction.Invoke(selectedHex);
            }
        }

        Hex MouseInputToHex()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Generator>())
                {
                    Hex selectedHex = HexData.PixelToHex(hit.point, hit.transform.GetComponent<Generator>().MapData.Hexes);
                    return selectedHex;
                }
            }
            return null;
        }
    }
}
