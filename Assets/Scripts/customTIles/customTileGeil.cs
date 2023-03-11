using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class customTileGeil : Tile
{
    [Header("Tile block")]
    public Vector2Int m_size = Vector2Int.one;
    public Vector2Int offset = Vector2Int.zero;
    public Sprite[] m_sprites;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref UnityEngine.Tilemaps.TileData tileData)
    {
        tileData.sprite = GetSprite(position);
    }

    public void loadTiles(string path)
    {
        m_sprites = Resources.LoadAll<Sprite>(path);
    }

    int mod(int x, int y)
    {
        return (x % y + y) % y;
    }

    public Sprite GetSprite(Vector3Int pos)
    {
        //check if array length matches the dimensions
        if (m_sprites.Length != m_size.x * m_size.y) return sprite;

        pos.x -= 15;
        pos.y -= 10;
        //get the index on each axis
        int x = mod(pos.x, m_size.x);
        int y = mod(pos.y, m_size.y);
        //invert y
        y = y * -1 + (m_size.y - 1);

        //get the index in the array
        int index = x + y * m_size.x;

        //returns the correct sprite
        if(y >= 18)
        {
            Debug.Log("posX: " + pos.x + ", posY: " + pos.y);
            //Debug.Log("lengthX: " + m_size.x + ", lengthY: " + m_size.y);
            Debug.Log("x: " + x + ", y: " + y);
            Debug.Log("index: " + index);
            Debug.Log(mod(pos.y, m_size.y));
            return m_sprites[0];
        }
        //Debug.Log("posX: " + pos.x + ", posY: " + pos.y + ", x: " + x + ", y: " + y + ", index: " + index);
        return m_sprites[index];
    }

#if UNITY_EDITOR

    [MenuItem("Assets/Create/2D/Custom Tiles/ Variable Tile")]

    public static void CreateVariableTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Variable Tile", "New Variable Tile", "Asset", "Save Variable Tile", "Assets");
        if (path == "") return;

        Debug.Log(path);

        AssetDatabase.CreateAsset(CreateInstance<customTileGeil>(), path);
    }
#endif
}
