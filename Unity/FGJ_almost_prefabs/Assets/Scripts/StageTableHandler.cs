using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageData
{
	public float gameTime = -1;
	public int passRequestCount = -1;
	public int mapID = -1;
	public int mapWidth = -1;
	public int mapHeight = -1;
}

public class StageTableHandler
{

	private Dictionary<string, StageData> dataDic = new Dictionary<string, StageData>();

	public void InitData()
	{
		TextAsset csvTxt = Resources.Load<TextAsset>("Data/stage_table");
		List<Dictionary<string, string>> datas = CSVReader.ReadFile(csvTxt);
		foreach (Dictionary<string, string> data in datas)
		{
			StageData stageData = new StageData();
			stageData.gameTime = float.Parse(data["game_time"]);
			stageData.passRequestCount = int.Parse(data["pass_request"]);
			stageData.mapID = int.Parse(data["map_id"]);
			stageData.mapWidth = int.Parse(data["map_w"]);
			stageData.mapHeight = int.Parse(data["map_h"]);
			dataDic.Add(data["stage"], stageData);
		}
	}

	public StageData GetStageData(int _stageIndex)
	{
		return dataDic[_stageIndex.ToString()];
	}

}
