using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapData
{
	public int mapID = -1;
	public int mapWidth = -1;
	public int mapHeight = -1;
	public int[,] mapArray;
}

public class MapTableHandler
{

	private Dictionary<int, MapData> dataDic = new Dictionary<int, MapData>();

	public void InitData(int _mapCount, int _width, int _height)
	{
		string file = string.Format("Data/map_table_{0}", _mapCount);
		MapData map = InitData(file, _width, _height);
		map.mapID = _mapCount;
		map.mapWidth = _width;
		map.mapHeight = _height;
		dataDic.Add(_mapCount, map);
	}

	public MapData InitData(string _filename, int _width, int _height)
	{
		TextAsset csvTxt = Resources.Load<TextAsset>(_filename);
		var mapArray = CSVReader.ReadMapFile(_width, _height, csvTxt);

		MapData mapData = new MapData();
		mapData.mapArray = mapArray;

		return mapData;
	}

	public MapData GetMapData(int _mapID)
	{
		return dataDic[_mapID];
	}

}
