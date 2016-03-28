using UnityEditor;
using UnityEngine;
using System.Text;

public class CTiledTools
{
    const string API_PATH = "Tiled/";
    const string API_MENU = "Assets/Tiled/";

    const string CREATE_TEXT_ASSET = "Create TextAsset";

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

    [MenuItem(API_PATH + CREATE_TEXT_ASSET, true)]
    [MenuItem(API_MENU + CREATE_TEXT_ASSET, true)]
    public static bool ValidateTmxFile()
    {
        string tAssetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        return tAssetPath.ToLower().EndsWith(".tmx");
    }
}
