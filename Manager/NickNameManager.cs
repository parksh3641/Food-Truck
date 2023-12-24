using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class NickNameManager : MonoBehaviour
{
    public GameObject nickNameView;

    public Text signText;

    public GameObject closeButton;
    public GameObject buyFreeButton;
    public GameObject buyCoinButton;
    public Text coinPriceText;

    private int price = 200000;
    private int maxPrice = 2000000;
    private int nowPrice = 0;

    public InputField inputField;

    public string[] lines;
    string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    public NoticeManager noticeManager;

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

    public void OpenFreeNickName()
    {
        nickNameView.SetActive(true);

        closeButton.SetActive(false);
        buyFreeButton.SetActive(true);
        buyCoinButton.SetActive(false);
    }    

    public void OpenNickName()
    {
        if (!nickNameView.activeSelf)
        {
            inputField.text = "";

            nickNameView.SetActive(true);

            closeButton.SetActive(true);
            buyFreeButton.SetActive(false);
            buyCoinButton.SetActive(true);

            nowPrice = price * (playerDataBase.ChangeNicknameCount + 1);

            if(nowPrice > maxPrice)
            {
                nowPrice = maxPrice;
            }

            coinPriceText.text = MoneyUnitString.ToCurrencyString(nowPrice);
        }
        else
        {
            nickNameView.SetActive(false);
        }
    }
    public void CheckNickName(int number)
    {
        if (!NetworkConnect.instance.CheckConnectInternet())
        {
            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
            NotionManager.instance.UseNotion(NotionType.NetworkConnectNotion);
            return;
        }

        for (int i = 0; i < lines.Length; i++)
        {
            if (inputField.text.ToLower().Contains(lines[i]))
            {
                SoundManager.instance.PlaySFX(GameSfxType.Wrong);
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
                    if (number == 1)
                    {
                        if (playerDataBase.Coin >= nowPrice)
                        {
                            PlayfabManager.instance.UpdateSubtractGold(nowPrice);
                        }
                        else
                        {
                            SoundManager.instance.PlaySFX(GameSfxType.Wrong);
                            NotionManager.instance.UseNotion(NotionType.LowCoin);
                            return;
                        }
                    }

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

        signText.text = GameStateManager.instance.NickName;

        nickNameView.SetActive(false);

        if(playerDataBase.LockTutorial == 0)
        {
            noticeManager.OpenNoticeView();
        }

        playerDataBase.ChangeNicknameCount += 1;
        PlayfabManager.instance.UpdatePlayerStatisticsInsert("ChangeNicknameCount", playerDataBase.ChangeNicknameCount);

        if(!GameStateManager.instance.Tutorial)
        {
            GameStateManager.instance.Tutorial = true;
        }
    }

    public void Failure()
    {
        SoundManager.instance.PlaySFX(GameSfxType.Wrong);
        NotionManager.instance.UseNotion(NotionType.SignNotion5);
    }
}
