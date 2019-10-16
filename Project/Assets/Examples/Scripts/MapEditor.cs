using UnityEngine;
using HexGen;
using UnityEngine.EventSystems;

public class MapEditor : MonoBehaviour
{
    private MapSettings mapSettings;
    public Vector2Int Pos;
    public int TerrainIndex;
    private Generator generator;

    void Start()
    {
        generator = GetComponent<Generator>();
        mapSettings = generator.MapSettings;
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
                Pos = HexData.PixelToHex(hit.point, generator.MapData.Hexes).LocalPos;
                generator.MapData.ChangeHexTerrainType(Pos.x + mapSettings.WorldWidth * Pos.y, mapSettings.TerrainTypes[TerrainIndex]);
                generator.UpdateMesh();
            }
        }
    }

    public void ChangeColor(int terrainIndex)
    {
        TerrainIndex = terrainIndex;
    }
}
