using System;
using UnityEngine;

public class HexGridLayout : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;

    [Header("Tile Settings")] 
    [SerializeField] private float outerSize = 1f;
    [SerializeField] private float innerSize = 0.0f;
    [SerializeField] private float height = 1f;
    [SerializeField] private bool isFlatTopped = false;
    [SerializeField] private Material material;


    private void OnEnable()
    {
        LayoutGrid();
    }
    
    private void OnValidate()
    {
        if(Application.isPlaying)
            LayoutGrid();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LayoutGrid()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject tile = new GameObject($"Hex {x}, {y}", typeof(HexRenderer));
                tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x, y));
                
                
                HexRenderer hexRenderer = tile.GetComponent<HexRenderer>();
                hexRenderer.IsFlatTopped = isFlatTopped;
                hexRenderer.OuterSize = outerSize;
                hexRenderer.InnerSize = innerSize;
                hexRenderer.Height = height;
                hexRenderer.SetMaterial(material);
                hexRenderer.DrawMesh();
                tile.transform.SetParent(transform, true);
            }
        }
    }

    private Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;
        float width;
        float height;
        float xPosition = 0f;
        float yPosition = 0f;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = outerSize;

        if (!isFlatTopped)
        {
            shouldOffset = (row % 2 == 0);
            width = MathF.Sqrt(3) * size;
            height = 2f * size;
            horizontalDistance = width;
            verticalDistance = height * 3f / 4f;
            
            offset = shouldOffset ? width / 2f : 0f;
            
            xPosition = (column * (horizontalDistance)) + offset;
            yPosition = (row * verticalDistance);
        }
        else
        {
            shouldOffset = (column % 2 == 0);
            width = 2f * size;
            height = MathF.Sqrt(3) * size;
            horizontalDistance = width * (3f/4f);
            verticalDistance = height;
            
            offset = shouldOffset ? height / 2f : 0f;
            
            xPosition = column * horizontalDistance;
            yPosition = (row * verticalDistance) - offset;
        }

        return new Vector3(xPosition, 0, -yPosition);
    }
    
}
