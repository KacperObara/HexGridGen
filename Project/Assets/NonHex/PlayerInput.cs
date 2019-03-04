using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Hex hex;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            hex = HexInteraction.SelectHexagon();
        }
    }
}
