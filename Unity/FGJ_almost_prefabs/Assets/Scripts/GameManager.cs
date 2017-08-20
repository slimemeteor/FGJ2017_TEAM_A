#define DEBUG
#if !UNITY_EDITOR
#undef DEBUG
#endif
#undef DEBUG

using UnityEngine;
using System.Collections;

// created by gooku // GGJ2016
public partial class GameManager : Singleton<GameManager> {
	//gooku, setting at the object/-s
//	public GameObject StartMenu;
//	public GameObject[] StageArray;
	public AudioSource LoopMusic;
	public AudioSource BtnAudio;
	//gooku, setting at the object/-e

	public bool IsPlayable { get { return gamestatus == GameStatus.PROCESS; } }

	private int currentStageIndex = 0;
	private int passRequestCount = 3;
	//private float punishTime = 1;

	private GameStatus gamestatus = GameStatus.NON;
	private StageTableHandler stageTableHandler = new StageTableHandler();
	private MapTableHandler mapTableHandler = new MapTableHandler();

	private int stageCount = 5;
	private GameMission missionNow = new GameMission();
	private GameMission missionStage = new GameMission();
	private bool isMissionClear = false;

	protected override void Awake()
	{
		base.Awake();
		stageTableHandler.InitData();

		for (int i = 0; i < stageCount; ++i) {
			StageData stageData = stageTableHandler.GetStageData(i);
			mapTableHandler.InitData(i, stageData.mapWidth, stageData.mapHeight);
		}
	}

	public void StartMission(int stageIndex)
	{
		missionNow.stageIndex = stageIndex;
		missionNow.timeCount = 0;
		missionNow.killCount = 0;
		missionNow.saveCount = 0;
		missionStage = GetStage(stageIndex).mapMission;
		isMissionClear = false;
	}

	public void MissionKillAdd(int value)
	{
		missionNow.killCount += value;
	}

	public void MissionSaveAdd(int value)
	{
		missionNow.saveCount += value;
	}

	public bool IsMissionCleared()
	{
		bool isKillOver = missionNow.killCount >= missionStage.killCount;
		bool isSaveOver = missionNow.saveCount >= missionStage.saveCount;
		Debug.Log(string.Format("kill = ({0}/{1}) | save = ({2}/{3})",
			missionNow.killCount, missionStage.killCount,
			missionNow.saveCount, missionStage.saveCount));
		isMissionClear = isKillOver || isSaveOver;
		return isMissionClear;
	}

	private void Start() {
#if DEBUG
		GameStart();
#else
		gamestatus = GameStatus.NON;
		//StartMenu.SetActive(true);
#endif
	}

	private void Update()
	{
		if (gamestatus == GameStatus.PROCESS) {
			// somthing in stage...			
		}

		if (Input.GetKeyDown(KeyCode.U))
		{
			MissionKillAdd(1);
			var isClear = IsMissionCleared();
			Debug.Log(string.Format("Is Clear = {0}", isClear));
		}
		if (Input.GetKeyDown(KeyCode.I))
		{
			MissionSaveAdd(1);
			var isClear = IsMissionCleared();
			Debug.Log(string.Format("Is Clear = {0}", isClear));
		}
		if (Input.GetKeyDown(KeyCode.O))
		{
			StartMission(1);
			Debug.Log("Reset Mission");
		}

	}

	public void GameStart()
	{
		//StartMenu.SetActive(false);
		currentStageIndex = 0;
		InitStage(currentStageIndex);
		gamestatus = GameStatus.PROCESS;
		LoopMusic.clip = Resources.Load<AudioClip>("sound/bg_music_loop");
		LoopMusic.Play();
	}

	public void GetDecide(bool _isPass)
	{

		if (gamestatus != GameStatus.PROCESS) {
			return;
		}

		if (_isPass)
		{
			BtnAudio.clip = Resources.Load<AudioClip>("sound/correct_sound");
			BtnAudio.Play();
			passRequestCount--;
			if (passRequestCount == 0) {
				Pass();
			}
		} else {
			BtnAudio.clip = Resources.Load<AudioClip>("sound/sound_wrong");
			BtnAudio.Play();

			GameOver();
		}
	}

	private void Pass()
	{
		gamestatus = GameStatus.PASS;
	}

	private void GameOver()
	{
		gamestatus = GameStatus.FAIL;
	}

	private void InitStage(int _index)
	{
		StageData stageData = stageTableHandler.GetStageData(_index);

		MapData mapData = mapTableHandler.GetMapData(stageData.mapID);

		//passTime = stageData.gameTime;
		passRequestCount = stageData.passRequestCount;

//		for (int i=0; i < StageArray.Length; ++i)
//		{
//			StageArray[i].SetActive(_index == i);
//		}
	}

	public Vector3 GetPlayerPosition(int stageIndex)
	{
		var stage = GetStage(stageIndex);

		var map = GetMap(stage.mapID);

		for (int y = 0; y < map.mapHeight; ++y) {

			for (int x = 0; x < map.mapWidth; ++x) {
			
				if (map.mapArray[x,y] == 99) {
					return new Vector3(x, y, 0);
				}

			}

		}

		return Vector3.zero;
	}

	public StageData GetStage(int stageIndex)
	{
		return stageTableHandler.GetStageData(stageIndex);
	}

	public MapData GetMap(int mapID)
	{
		return mapTableHandler.GetMapData(mapID);
	}

	public void NextStage()
	{
		currentStageIndex++;

//		if (currentStageIndex >= StageArray.Length) {
//			currentStageIndex = 0;
//		}

		BtnAudio.clip = Resources.Load<AudioClip>("sound/change_stage");
		BtnAudio.Play();


		InitStage(currentStageIndex);
		gamestatus = GameStatus.PROCESS;
	}

	public void Restart() 
	{
		InitStage(currentStageIndex);
		gamestatus = GameStatus.PROCESS;
	}

}

public enum GameStatus { NON, PROCESS, PASS, FAIL }
