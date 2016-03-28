# CTiled
Tiled interpreter for Unity3D

CTiled is a class that works as an interpreter for Tiled tmx files.

## Use
You must create a new variable passing a TextAsset with the map as an argument.
```c#
using UnityEngine;
using System.Collections.Generic;

public class CTileMap : MonoBehaviour
{
    [SerializeField]
    TextAsset _xmlMap;
    
    CTiled _tilemap;
    
    void Start()
    {
        _tilemap = new CTiled(_xmlMap);
    }
}
```

## Public members
```c#
public int width;
public int height;
public List<Tileset> tilesets;
```

## Subclasses
### Tileset
The Tileset class has information about the tilesets with the following public members:
```c#
public int tileWidth;
public int tileHeight;
public int spacing;
public int tilecount;
public int columns;
public int firstGID;
public string name;
public Dictionary<int, Dictionary<string, string>> properties;
```
### Layer
The Layer class has information about a layer on the map, that is the tilemap itself
Public members:
```c#
public string name;
public int width;
public int height;
public float opacity;
int[][] _map;
```

## Public methods
```c#
public void SetXMLAsset(TextAsset aXMLAsset)
```
Sets the text asset in the case you want to use the same variable for a different map.

```c#
public void LoadMap()
```
Loads the map. This is run on the constructor.

```c#
public int GetTileID(int aLayer, int aCol, int aRow)
```
Returns the tile gid on a given layer and coordinates.

```c#
public int GetNumberOfLayers()
```
Returns the amount of layers in the map.

```c#
public string GetLayerName(int aLayer)
```
Returns the name of the layer number aLayer.

```c#
public Layer GetLayer(int aLayer)
```
Returns the layer number aLayer.

# Tiled tool
In order to use the CTiled class you need the map as a TextAsset but Unity doesn't take the .tmx as a TextAsset so I made a tool for creating it. It creates a duplicate of the map and changes the extension, I didn't make it just change the extension because it would disrupt the Tiled work environment.
To create the TextAsset you just have to right click on a tmx file and go to Tiled > Create TextAsset.
