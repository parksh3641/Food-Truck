using Firebase.Analytics;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NamoManager : MonoBehaviour
{
	private LevelType scoreLevel = LevelType.Insane;

	public GameObject[] mapArray;

	public Material[] skyboxArray;

	public GameObject cmVCam;

	public Text timerText;
	public Text scoreText;

	public LocalizationContent bestRecordTitleText;
	public Text bestRecordText;

	public GameObject gameStartUI;
	public LocalizationContent gameTitleText;
	public LocalizationContent gameGoalText;

	public GameObject gameWinUI;
	public GameObject gameOverUI;

	public GameObject bestRecordObj;
	public Text clearTimeText;

	private float milliseconds = 0;

	private int timerMinutes = 0;
	private int timerSeconds = 0;
	private float timermilliseconds = 0;

	private int record = 0;

	[SerializeField]
	private bool isPlay = false;

	private int score = 0;
	private int plus = 0;

	[Space]
	[Title("Wall")]
	public float moveTime = 3.0f;
	public float delayTime = 3.0f;

	public GameObject target;
	public Vector3 targetPosition;
	public Vector3 startPosition;

	public SoundManager soundManager;

	PlayerDataBase playerDataBase;

	private void Awake()
	{
		if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

		cmVCam.SetActive(true);

		gameWinUI.SetActive(false);
		gameOverUI.SetActive(false);
		bestRecordObj.SetActive(false);

		score = 0;
		scoreText.text = "0";
		timerText.text = "";
	}

	void Start()
	{
		for(int i = 0; i < mapArray.Length; i ++)
        {
			mapArray[i].SetActive(false);
        }

		gameStartUI.SetActive(true);
		gameTitleText.localizationName = LocalizationManager.instance.GetString("Game" + ((int)GameStateManager.instance.LevelType + 1));
		gameGoalText.localizationName = LocalizationManager.instance.GetString("Game" + ((int)GameStateManager.instance.LevelType + 1) + "_Info");

		gameTitleText.ReLoad();
		gameGoalText.ReLoad();

		mapArray[(int)GameStateManager.instance.LevelType].SetActive(true);

		RenderSettings.skybox = skyboxArray[(int)GameStateManager.instance.LevelType];

		soundManager.PlayGameBGM();

		bestRecordTitleText.localizationName = "BestRecord";

		switch (GameStateManager.instance.LevelType)
		{
			case LevelType.Easy:

				//bestRecordTitleText.localizationName = "BestScore";

				if (!GameStateManager.instance.AutoLogin)
				{
					record = playerDataBase.Easy_Offline;
				}
				else
				{
					record = playerDataBase.Easy;
				}

				//NotionManager.instance.UseNotion(NotionType.Game1_Info);
				break;
			case LevelType.Normal:
				if (!GameStateManager.instance.AutoLogin)
				{
					record = playerDataBase.Normal_Offline;
				}
				else
				{
					record = playerDataBase.Normal;
				}

				StartCoroutine(MoveToTargetPosition());

				//NotionManager.instance.UseNotion(NotionType.Game2_Info);
				break;
			case LevelType.Hard:
				if (!GameStateManager.instance.AutoLogin)
				{
					record = playerDataBase.Hard_Offline;
				}
				else
				{
					record = playerDataBase.Hard;
				}

				//NotionManager.instance.UseNotion(NotionType.Game3_Info);
				break;
			case LevelType.Crazy:
				if (!GameStateManager.instance.AutoLogin)
				{
					record = playerDataBase.Crazy_Offline;
				}
				else
				{
					record = playerDataBase.Crazy;
				}

				break;
			case LevelType.Insane:
				if (!GameStateManager.instance.AutoLogin)
				{
					record = playerDataBase.Insane_Offline;
				}
				else
				{
					record = playerDataBase.Insane;
				}

				break;
		}

		bestRecordTitleText.ReLoad();

		milliseconds = 0;

		timerMinutes = 0;
		timerSeconds = 0;
		timermilliseconds = 0;

		if (GameStateManager.instance.LevelType != scoreLevel)
		{
			if (record != 0)
			{
				record = 3600000 - record;

				timerMinutes = (record / 60000);
				timerSeconds = (record % 60000) / 1000;
				timermilliseconds = int.Parse(record.ToString().Substring(Mathf.Max(record.ToString().Length - 3, 0)));

				if (timerMinutes >= 1)
				{
					bestRecordText.text = timerMinutes.ToString("D2") + ":" + timerSeconds.ToString("D2") + ".<size=10>" + timermilliseconds.ToString("000") + "</size>";
				}
				else
				{
					bestRecordText.text = timerSeconds.ToString("D2") + ".<size=10>" + timermilliseconds.ToString("000") + "</size>";
				}
			}
			else
			{
				bestRecordText.text = "00:00";
			}
		}
		else
		{
			bestRecordText.text = record.ToString();
		}
	}

	public void GameStart()
    {
		gameStartUI.SetActive(false);

		isPlay = true;
	}

	public void GameOver(bool won)
	{
		isPlay = false;

		if (won)
		{
			gameWinUI.SetActive(true);

			bestRecordObj.SetActive(false);

			if (GameStateManager.instance.LevelType != scoreLevel)
			{
				if (timerMinutes >= 1)
				{
					clearTimeText.text = timerMinutes.ToString("D2") + ":" + timerSeconds.ToString("D2") + ".<size=70>" + timermilliseconds.ToString("000") + "</size>";
				}
				else
				{
					clearTimeText.text = timerSeconds.ToString("D2") + ".<size=70>" + timermilliseconds.ToString("000") + "</size>";
				}
			}
			else
			{
				clearTimeText.text = score.ToString();
			}

			UpdateRecord();

			//PlayfabManager.instance.UpdateAddGold(100);
		}
		else
		{
			cmVCam.SetActive(false);

			gameOverUI.SetActive(true);

		}

		//GameStateManager.instance.AdCount += 1;

		//if (GameStateManager.instance.AdCount >= 3)
		//{
		//	Debug.Log("Show Screen Ad");

		//	GameStateManager.instance.AdCount = 0;

		//	GoogleAdsManager.instance.admobScreen.ShowAd();
		//}

	}

	public void BackToMainScene()
	{
		SceneManager.LoadScene("MainScene");
	}

	public void RetryButton()
	{
		SceneManager.LoadScene("GameScene");
	}

	void Update()
	{
		if (!isPlay) return;

		if (GameStateManager.instance.LevelType == scoreLevel) return;

		milliseconds += Time.deltaTime;
		TimeSpan timespan = TimeSpan.FromSeconds(milliseconds);

		timerMinutes = timespan.Minutes;
		timerSeconds = timespan.Seconds;
		timermilliseconds = timespan.Milliseconds;

		if (timespan.Hours >= 1)
		{
			isPlay = false;

			timerMinutes = 59;
			timerSeconds = 59;
			timermilliseconds = 999;

			GameOver(false);
		}
		else
		{
			if (timespan.Minutes >= 1)
			{
				timerText.text = timespan.Minutes.ToString("D2") + ":" + timespan.Seconds.ToString("D2") + ".<size=10>" + timespan.Milliseconds.ToString("000") + "</size>";
			}
			else
			{
				timerText.text = timespan.Seconds.ToString("D2") + ".<size=10>" + timespan.Milliseconds.ToString("000") + "</size>";
			}
		}
	}

	public void UpdateRecord()
	{
		//GameStateManager.instance.Record = true;

		if (GameStateManager.instance.LevelType != scoreLevel)
		{
			record = 3600000 - (timerMinutes * 60000) - (timerSeconds * 1000) - (int)timermilliseconds;
		}
		else
		{
			record = score;
		}

		switch (GameStateManager.instance.LevelType)
		{
			case LevelType.Easy:
				if (NetworkConnect.instance.CheckConnectInternet())
				{
					if (!GameStateManager.instance.AutoLogin)
					{
						if (record > playerDataBase.Easy_Offline)
						{
							bestRecordObj.SetActive(true);

							playerDataBase.Easy_Offline = record;

							PlayerPrefs.SetInt("Easy_Offline", record);
						}
					}
					else
					{
						if (record > playerDataBase.Easy)
						{
							bestRecordObj.SetActive(true);

							playerDataBase.Easy = record;

							PlayfabManager.instance.UpdatePlayerStatisticsInsert("Easy", record);
						}
					}
				}
				else
				{
					if (record > playerDataBase.Easy)
					{
						bestRecordObj.SetActive(true);

						playerDataBase.Easy = record;

						PlayfabManager.instance.UpdatePlayerStatisticsInsert("Easy", record);
					}
				}
				break;
			case LevelType.Normal:
				if (NetworkConnect.instance.CheckConnectInternet())
				{
					if (!GameStateManager.instance.AutoLogin)
					{
						if (record > playerDataBase.Normal_Offline)
						{
							bestRecordObj.SetActive(true);

							playerDataBase.Normal_Offline = record;

							PlayerPrefs.SetInt("Normal_Offline", record);
						}
					}
					else
					{
						if (record > playerDataBase.Normal)
						{
							bestRecordObj.SetActive(true);

							playerDataBase.Normal = record;

							PlayfabManager.instance.UpdatePlayerStatisticsInsert("Normal", record);
						}
					}
				}
				else
				{
					if (record > playerDataBase.Normal)
					{
						bestRecordObj.SetActive(true);

						playerDataBase.Normal = record;

						PlayfabManager.instance.UpdatePlayerStatisticsInsert("Normal", record);
					}
				}
				break;
			case LevelType.Hard:
				if (NetworkConnect.instance.CheckConnectInternet())
				{
					if (!GameStateManager.instance.AutoLogin)
					{
						if (record > playerDataBase.Hard_Offline)
						{
							bestRecordObj.SetActive(true);

							playerDataBase.Hard_Offline = record;

							PlayerPrefs.SetInt("Hard_Offline", record);
						}
					}
					else
					{
						if (record > playerDataBase.Hard)
						{
							bestRecordObj.SetActive(true);

							playerDataBase.Hard = record;

							PlayfabManager.instance.UpdatePlayerStatisticsInsert("Hard", record);
						}
					}
				}
				else
				{
					if (record > playerDataBase.Hard)
					{
						bestRecordObj.SetActive(true);

						playerDataBase.Hard = record;

						PlayfabManager.instance.UpdatePlayerStatisticsInsert("Hard", record);
					}
				}
				break;
			case LevelType.Crazy:
				if (NetworkConnect.instance.CheckConnectInternet())
				{
					if (!GameStateManager.instance.AutoLogin)
					{
						if (record > playerDataBase.Crazy_Offline)
						{
							bestRecordObj.SetActive(true);

							playerDataBase.Crazy_Offline = record;

							PlayerPrefs.SetInt("Crazy_Offline", record);
						}
					}
					else
					{
						if (record > playerDataBase.Crazy)
						{
							bestRecordObj.SetActive(true);

							playerDataBase.Crazy = record;

							PlayfabManager.instance.UpdatePlayerStatisticsInsert("Crazy", record);
						}
					}
				}
				else
				{
					if (record > playerDataBase.Crazy)
					{
						bestRecordObj.SetActive(true);

						playerDataBase.Crazy = record;

						PlayfabManager.instance.UpdatePlayerStatisticsInsert("Crazy", record);
					}
				}
				break;
			case LevelType.Insane:
				if (NetworkConnect.instance.CheckConnectInternet())
				{
					if (!GameStateManager.instance.AutoLogin)
					{
						if (record > playerDataBase.Insane_Offline)
						{
							bestRecordObj.SetActive(true);

							playerDataBase.Insane_Offline = record;

							PlayerPrefs.SetInt("Insane_Offline", record);
						}
					}
					else
					{
						if (record > playerDataBase.Insane)
						{
							bestRecordObj.SetActive(true);

							playerDataBase.Insane = record;

							PlayfabManager.instance.UpdatePlayerStatisticsInsert("Insane", record);
						}
					}
				}
				else
				{
					if (record > playerDataBase.Insane)
					{
						bestRecordObj.SetActive(true);

						playerDataBase.Insane = record;

						PlayfabManager.instance.UpdatePlayerStatisticsInsert("Insane", record);
					}
				}
				break;
		}

		Debug.Log(GameStateManager.instance.LevelType + " 데이터 업데이트");
	}

	IEnumerator MoveToTargetPosition()
    {
		yield return new WaitForSeconds(delayTime);

		startPosition = transform.position;
		float elapsedTime = 0f;

		//soundManager.PlaySFX(GameSfxType.WallMove);

		while (elapsedTime < moveTime)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / moveTime);
			target.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
			yield return null;
		}

		//soundManager.PlaySFX(GameSfxType.WallHit);

		yield return new WaitForSeconds(delayTime);

		elapsedTime = 0;

		//soundManager.PlaySFX(GameSfxType.WallMove);

		while (elapsedTime < moveTime)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / moveTime);
			target.transform.position = Vector3.Lerp(targetPosition, startPosition, t);
			yield return null;
		}

		StartCoroutine(MoveToTargetPosition());
	}
}
