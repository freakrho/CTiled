using UnityEngine;
using System.Collections.Generic;

public class CTiled
{
    // Constants
    const string TAG_TILESET = "tileset";
    const string TAG_TILE = "tile";
    const string TAG_PROPERTY = "properties";
    const string TAG_LAYER = "layer";
    const string ATT_NAME = "name";
    const string ATT_ID = "id";
    const string ATT_VALUE = "value";
    const string ATT_WIDTH = "width";
    const string ATT_HEIGHT = "height";
    const string ATT_GID = "gid";
    const string ATT_TILEWIDTH = "tilewidth";
    const string ATT_TILEHEIGHT = "tileheight";
    const string ATT_SPACING = "spacing";
    const string ATT_TILECOUNT = "tilecount";
    const string ATT_COLUMNS = "columns";
    const string ATT_FIRST_GID = "firstgid";
    const string ATT_OPACITY = "opacity";

    public int width;
    public int height;
    public List<Tileset> tilesets;

    TextAsset _xmlAsset;
    Layer[] _map;

    public class Tileset
    {
        public int tileWidth;
        public int tileHeight;
        public int spacing;
        public int tilecount;
        public int columns;
        public int firstGID;
        public string name;
        public Dictionary<int, Dictionary<string, string>> properties;

        public Tileset(XmlNode aTileset)
        {
            tileWidth = int.Parse(aTileset.Attributes[ATT_TILEWIDTH].Value);
            tileHeight = int.Parse(aTileset.Attributes[ATT_TILEHEIGHT].Value);
            spacing = int.Parse(aTileset.Attributes[ATT_SPACING].Value);
            tilecount = int.Parse(aTileset.Attributes[ATT_TILECOUNT].Value);
            columns = int.Parse(aTileset.Attributes[ATT_COLUMNS].Value);
            firstGID = int.Parse(aTileset.Attributes[ATT_FIRST_GID].Value);
            name = aTileset.Attributes[ATT_NAME].Value;

            // Save properties
            properties = new Dictionary<int, Dictionary<string, string>>();
            foreach (XmlNode tContent in aTileset.ChildNodes)
            {
                if (tContent.Name == TAG_TILE)
                {
                    int tID = int.Parse(tContent.Attributes[ATT_ID].Value) + firstGID;
                    Dictionary<string, string> tDict = new Dictionary<string, string>();
                    foreach (XmlNode tData in tContent.ChildNodes)
                    {
                        if (tData.Name == TAG_PROPERTY)
                        {
                            foreach (XmlNode tProperty in tData.ChildNodes)
                            {
                                string tName = tProperty.Attributes[ATT_NAME].Value;
                                string tValue = tProperty.Attributes[ATT_VALUE].Value;
                                tDict.Add(tName, tValue);
                            }
                        }
                    }
                    properties.Add(tID, tDict);
                }
            }
        }
    }

    public class Layer
    {
        public string name;
        public int width;
        public int height;
        public float opacity;
        int[][] _map;

        public Layer(XmlNode aLayer)
        {
            // Properties
            width = int.Parse(aLayer.Attributes[ATT_WIDTH].Value);
            height = int.Parse(aLayer.Attributes[ATT_HEIGHT].Value);
            name = aLayer.Attributes[ATT_NAME].Value;
            XmlAttribute tOpacityAtt = aLayer.Attributes[ATT_OPACITY];
            if (tOpacityAtt != null)
                opacity = float.Parse(tOpacityAtt.Value);
            else
                opacity = 1;

            _map = new int[height][];

            int tRow = 0;
            int tCol = 0;

            int[] tTileRow = new int[width];
            foreach (XmlNode tData in aLayer.ChildNodes)
            {
                foreach (XmlNode tTileNode in tData.ChildNodes)
                {
                    tTileRow[tCol] = int.Parse(tTileNode.Attributes[ATT_GID].Value);

                    tCol++;
                    if (tCol >= width)
                    {
                        _map[tRow] = tTileRow;
                        tTileRow = new int[width];
                        tCol = 0;
                        tRow++;
                    }
                }
            }
        }

        public int GetTileID(int aCol, int aRow)
        {
            return _map[aRow][aCol];
        }
    }

    public CTiled(TextAsset aXMLAsset)
    {
        _xmlAsset = aXMLAsset;
        LoadMap();
    }

    public void SetXMLAsset(TextAsset aXMLAsset)
    {
        _xmlAsset = aXMLAsset;
    }

    public void LoadMap()
    {
        //Dictionary<int, int> tIdList = new Dictionary<int, int>();
        XmlDocument tXmlDoc = new XmlDocument();
        // load the file
        tXmlDoc.LoadXml(_xmlAsset.text);

        // Save tilesets
        tilesets = new List<Tileset>();
        XmlNodeList tTilesetsList = tXmlDoc.GetElementsByTagName(TAG_TILESET);
        foreach (XmlNode tTileset in tTilesetsList)
        {
            tilesets.Add(new Tileset(tTileset));
        }
        
        XmlNodeList tLayersList = tXmlDoc.GetElementsByTagName(TAG_LAYER);

        width = int.Parse(tXmlDoc.DocumentElement.Attributes[ATT_WIDTH].Value);
        height = int.Parse(tXmlDoc.DocumentElement.Attributes[ATT_HEIGHT].Value);
        _map = new Layer[tLayersList.Count];

        int tCount = 0;

        foreach (XmlNode tLayer in tLayersList)
        {
            _map[tCount] = new Layer(tLayer);
            tCount++;
        }
    }

    public int GetTileID(int aLayer, int aCol, int aRow)
    {
        return _map[aLayer].GetTileID(aCol, aRow);
    }

    public int GetNumberOfLayers()
    {
        return _map.Length;
    }

    public string GetLayerName(int aLayer)
    {
        return _map[aLayer].name;
    }

    public Layer GetLayer(int aLayer)
    {
        return _map[aLayer];
    }
}
