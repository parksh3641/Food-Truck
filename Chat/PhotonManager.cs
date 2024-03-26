using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;

public class PhotonManager : MonoBehaviour, IChatClientListener
{
    public GameObject chatUI;

	public Transform chatUIFullScreen;
	public Transform chatUISmallScreen;
	public ChatContent chatUISmallText;

	public GameObject closeButton;

    private ChatClient chatClient;
    private string userName;
    private string currentChannelName = "Channel 001";

	public ChatContent chatContent;
	public RectTransform chatTransform;
    public List<ChatContent> chatContentList = new List<ChatContent>();

	public InputField inputField;

	private int index = 0;

	private bool delay = false;

	public string[] lines;
	string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

	WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

	PlayerDataBase playerDataBase;


	private void Awake()
    {
		if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

		chatUI.SetActive(false);

		chatUISmallText.Initialize("");

		closeButton.SetActive(false);


		for (int i = 0; i < 30; i++)
        {
            ChatContent monster = Instantiate(chatContent);
            monster.transform.SetParent(chatTransform);
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = Quaternion.identity;
            monster.transform.localScale = Vector3.one;

            monster.gameObject.SetActive(false);
            chatContentList.Add(monster);
        }

		string file = SystemPath.GetPath() + "BadWord.txt";

		string source;

		if (File.Exists(file))
		{
			StreamReader word = new StreamReader(file);
			source = word.ReadToEnd();
			word.Close();

			lines = Regex.Split(source, LINE_SPLIT_RE);
		}
	}


    public void Initialize()
    {
        userName = GameStateManager.instance.NickName;
        currentChannelName = "Channel 001";

		chatClient = new ChatClient(this);
		chatClient.ChatRegion = "asia";
		chatClient.Connect("a0e02b55-5336-4fe9-aedc-8f54e4a5184a", Application.version, new AuthenticationValues(userName));

		StopAllCoroutines();
		StartCoroutine(CheckChatCoroution());

		Debug.Log("채팅 서버 연결 시도 중");
    }

	public void AddLine(string txt)
	{
		if (index > chatContentList.Count - 1)
        {
			index = 0;
        }

		chatUISmallText.Initialize(txt);

		chatContentList[index].gameObject.SetActive(true);
		chatContentList[index].Initialize(txt);
		chatContentList[index].gameObject.transform.SetAsFirstSibling();

		index++;
	}

	public void OnApplicationQuit()
	{
		if (chatClient != null)
		{
			chatClient.Disconnect();
		}
	}

	public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
	{
		if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
		{
			Debug.LogError(message);
		}
		else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
		{
			Debug.LogWarning(message);
		}
		else
		{
			Debug.Log(message);
		}
	}

	public void OnConnected()
	{
		Debug.Log("서버에 연결되었습니다.");

		Application.runInBackground = true;

		chatClient.Subscribe(new string[] { currentChannelName }, 30);

		chatUI.SetActive(true);

		chatUISmallText.Initialize(LocalizationManager.instance.GetString("ChatStart"));
		Close();
	}

	public void OnDisconnected()
	{
		chatUI.SetActive(false);

		Application.runInBackground = false;

		Debug.Log("서버에 연결이 끊어졌습니다.");

		Invoke("Initialize", 10f);
	}

	public void OnChatStateChange(ChatState state)
	{
		Debug.Log("OnChatStateChange = " + state);
	}

	public void OnSubscribed(string[] channels, bool[] results)
	{
		Debug.Log(string.Format("채널 입장 ({0})", string.Join(",", channels)));
	}

	public void OnUnsubscribed(string[] channels)
	{
		Debug.Log(string.Format("채널 퇴장 ({0})", string.Join(",", channels)));
	}

	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		for (int i = 0; i < messages.Length; i++)
		{
			AddLine(string.Format("<Color=#FFFF00>{0}</Color> : {1}", senders[i], messages[i].ToString()));
		}
	}

	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		Debug.Log("OnPrivateMessage : " + message);
	}

	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		Debug.Log("status : " + string.Format("{0} is {1}, Msg : {2} ", user, status, message));
	}

	IEnumerator CheckChatCoroution()
    {
		if(chatClient != null)
        {
			chatClient.Service();
		}
        else
        {
			yield break;
        }

		if(inputField.isFocused)
        {
			chatUIFullScreen.localScale = Vector3.one;
			chatUISmallScreen.localScale = Vector3.zero;
			closeButton.SetActive(true);
		}

		yield return waitForSeconds;
		StartCoroutine(CheckChatCoroution());
	}

	public void Close()
    {
		chatUIFullScreen.localScale = Vector3.zero;
		chatUISmallScreen.localScale = Vector3.one;

		closeButton.SetActive(false);
	}

	public void Input_OnEndEdit(string text)
	{
		if (chatClient.State == ChatState.ConnectedToFrontEnd)
		{
			//chatClient.PublishMessage(currentChannelName, text);

			if(inputField.text.Trim().Length > 0)
            {
				if (!delay)
				{
					for (int i = 0; i < lines.Length; i++)
					{
						if (inputField.text.ToLower().Contains(lines[i]))
						{
							inputField.text = inputField.text.Replace(lines[i], "**");
						}
					}

					chatClient.PublishMessage(currentChannelName, inputField.text);

					inputField.text = "";

					delay = true;
					Invoke("Delay", 2f);
				}
			}
            else
            {
				inputField.text = "";
			}
		}
	}

	void Delay()
    {
		delay = false;
    }

	public void OnUserSubscribed(string channel, string user)
	{
		throw new System.NotImplementedException();
	}

	public void OnUserUnsubscribed(string channel, string user)
	{
		throw new System.NotImplementedException();
	}
}
