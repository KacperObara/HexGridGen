using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexGen;
using UnityEngine.EventSystems;

public class MapEditor : MonoBehaviour
{
    public Vector2Int Pos;
    public int TerrainIndex;
    private Generator generator;
    void Start()
    {
        generator = GetComponent<Generator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<Generator>())
            {
                Pos = HexInfo.PixelToHex(hit.point, generator.MapData.Hexes).LocalPos;
                generator.MapData.ColorMap[Pos.x, Pos.y] = generator.MapSettings.TerrainTypes[TerrainIndex].Color;
                generator.generators[2].Generate();
            }
        }
    }

    public void ChangeColor(int terrainIndex)
    {
        TerrainIndex = terrainIndex;
    }
}
