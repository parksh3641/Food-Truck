using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class NickNameManager : MonoBehaviour
{
    public GameObject nickNameView;

    public GameObject closeButton;

    public InputField inputField;

    public string[] lines;
    string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    public PlayerDataBase playerDataBase;

    private void Awake()
    {
        if (playerDataBase == null) playerDataBase = Resources.Load("PlayerDataBase") as PlayerDataBase;

        closeButton.SetActive(false);

        inputField.text = "";

        string file = SystemPath.GetPath() + "BadWord.txt";

        string source;

        if (File.Exists(file))
        {
            StreamReader word = new StreamReader(file);
            source = word.ReadToEnd();
            word.Close();

            lines = Regex.Split(source, LINE_SPLIT_RE);
        }

        nickNameView.SetActive(false);
    }

    public void OpenNickName()
    {
        if (!nickNameView.activeSelf)
        {
            inputField.text = "";

            nickNameView.SetActive(true);
            closeButton.SetActive(true);
        }
        else
        {
            nickNameView.SetActive(false);
        }
    }
    public void CheckNickName()
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        for (int i = 0; i < lines.Length; i++)
        {
            if (inputField.text.Contains(lines[i]))
            {
                NotionManager.instance.UseNotion(NotionType.SignNotion3);
                return;
            }
        }

        string Check = Regex.Replace(inputField.text, @"[^a-zA-Z0-9가-힣]", "", RegexOptions.Singleline);

        if (inputField.text.Equals(Check) == true)
        {
            string newNickName = ((inputField.text.Trim()).Replace(" ", ""));
            string oldNickName = "";

            if (GameStateManager.instance.NickName != null)
            {
                oldNickName = GameStateManager.instance.NickName.Trim().Replace(" ", "");
            }
            else
            {
                oldNickName = "";
            }

            if (newNickName.Length > 2)
            {
                if (!(newNickName.Equals(oldNickName)))
                {
                    PlayfabManager.instance.UpdateDisplayName(newNickName, Success, Failure);
                }
                else
                {
                    SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                    NotionManager.instance.UseNotion(NotionType.SignNotion1);
                }
            }
            else
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                NotionManager.instance.UseNotion(NotionType.SignNotion2);
            }
        }
        else
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.SignNotion3);
        }
    }

    public void Success()
    {
        SoundManager.instance.PlaySFX(GameSfxType.Success);
        NotionManager.instance.UseNotion(NotionType.SignNotion6);

        nickNameView.SetActive(false);
    }

    public void Failure()
    {
        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        NotionManager.instance.UseNotion(NotionType.SignNotion5);
    }
}
