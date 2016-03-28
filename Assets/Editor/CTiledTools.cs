using UnityEditor;
using UnityEngine;
using System.Text;

public class CTiledTools
{
    const string API_PATH = "API/Tiled/";
    const string API_MENU = "Assets/API/Tiled/";

    const string CREATE_TEXT_ASSET = "Create TextAsset";
    const string CREATE_TILEMAP = "Create Tilemap";

    [MenuItem(API_PATH + CREATE_TEXT_ASSET)]
    [MenuItem(API_MENU + CREATE_TEXT_ASSET)]
    public static void CreateModeList()
    {
        string tAssetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        StringBuilder tBuilder = new StringBuilder(tAssetPath);
        tBuilder.Replace(".tmx", "Txt.txt");
        string tNewFile = tBuilder.ToString();
        FileUtil.ReplaceFile(tAssetPath, tNewFile);
        AssetDatabase.ImportAsset(tNewFile);
        Debug.Log("File " + tNewFile + " created.");
    }

    [MenuItem(API_PATH + CREATE_TILEMAP)]
    [MenuItem(API_MENU + CREATE_TILEMAP)]
    public static void CreateTilemapObject()
    {
        CreateModeList();
        string tAssetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        StringBuilder tBuilder = new StringBuilder(tAssetPath);
        tBuilder.Replace(".tmx", "Txt.txt");
        string tNewFile = tBuilder.ToString();
        TextAsset tAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(tNewFile);

        GameObject tObj = new GameObject("Tilemap");
        CTileMap tMap = tObj.AddComponent<CTileMap>();
        tMap.SetXMLMap(tAsset);
    }

    [MenuItem(API_PATH + CREATE_TEXT_ASSET, true)]
    [MenuItem(API_MENU + CREATE_TEXT_ASSET, true)]
    [MenuItem(API_PATH + CREATE_TILEMAP, true)]
    [MenuItem(API_MENU + CREATE_TILEMAP, true)]
    public static bool ValidateTmxFile()
    {
        string tAssetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        return tAssetPath.ToLower().EndsWith(".tmx");
    }
}
