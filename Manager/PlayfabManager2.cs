using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveJsonData
{
    public int index1 = 0;
    public int index2 = 0;
    public int index3 = 0;
    public int index4 = 0;
    public int index5 = 0;
    public int index6 = 0;
    public int index7 = 0;
    public int index8 = 0;
    public int index9 = 0;
    public int index10 = 0;
    public int index11 = 0;
    public int index12 = 0;
    public int index13 = 0;
    public int index14 = 0;
    public int index15 = 0;
    public int index16 = 0;
    public int index17 = 0;
    public int index18 = 0;
    public int index19 = 0;
    public int index20 = 0;
    public int index21 = 0;
    public int index22 = 0;
    public int index23 = 0;
    public int index24 = 0;
    public int index25 = 0;
    public int index26 = 0;
    public int index27 = 0;
    public int index28 = 0;
    public int index29 = 0;
    public int index30 = 0;
}


public class PlayfabManager2 : MonoBehaviour
{
    public List<int> inventoryList = new List<int>(); //���� �κ��丮 ����


    [SerializeField]
    public SaveJsonData saveJsonData = new SaveJsonData(); //Json ����
    private Dictionary<string, string> jsonData = new Dictionary<string, string>();

    private List<string> itemData = new List<string>();
    private List<ItemInstance> itemList = new List<ItemInstance>();


    private void Awake() //�� ó�� �ʱ�ȭ
    {
        inventoryList.Clear();

        for (int i = 0; i < 30; i ++) //30�� ��ŭ �κ��丮 ���� Ȯ��
        {
            inventoryList.Add(0);
        }
    }


    public void SetPlayerJsonData() //������ Json ���� ����
    {
        SaveJsonData data = new SaveJsonData();

        data.index1 = 1; //������ ������ ���� ����
        data.index2 = 2;
        data.index3 = 10;
        data.index4 = 100;
        data.index5 = 1;
        data.index6 = 1;
        data.index7 = 1;
        data.index8 = 1;
        data.index9 = 1;
        data.index10 = 1;
        data.index11 = 1;
        data.index12 = 1;
        data.index13 = 1;
        data.index14 = 1;
        data.index15 = 1;
        data.index16 = 7;
        data.index17 = 1;
        data.index18 = 1123;
        data.index19 = 999;
        data.index20 = 1;
        data.index21 = 1;
        data.index22 = 1;
        data.index23 = 1;
        data.index24 = 1;
        data.index25 = 1;
        data.index26 = 1;
        data.index27 = 1;
        data.index28 = 1;
        data.index29 = 1;

        //������ ���� ��� saveJsonData ���� ���� ������ �ִ� ������ �ҷ����� ������ �� ��ŭ �־��� �� �ٽ� ������ �����ϸ� �˴ϴ�.

        jsonData.Clear();
        jsonData.Add("SaveJsonData", JsonUtility.ToJson(data));
        SetPlayerJsonData(jsonData);
    }


    public void SetPlayerJsonData(Dictionary<string, string> data) //������ Json ������ ����
    {
        var request = new UpdateUserDataRequest() { Data = data, Permission = UserDataPermission.Public };
        PlayFabClientAPI.UpdateUserData(request, (result) =>
        {
            Debug.Log("�÷��̾� Json ���� ������ ���� ����!");
        },
        error =>
        {
            Debug.Log("�÷��̾� Json ���� ������ ���� ����");
        });
    }


    public void GetPlayerJsonData() //�������� Json ������ �ҷ�����
    {
        var request = new GetUserDataRequest() { PlayFabId = GameStateManager.instance.PlayfabId };
        PlayFabClientAPI.GetUserData(request, (result) =>
        {
            SaveJsonData data = new SaveJsonData();

            foreach (var eachData in result.Data)
            {
                string key = eachData.Key;

                if (key.Contains("SaveJsonData"))
                {
                    data = JsonUtility.FromJson<SaveJsonData>(eachData.Value.Value);
                    saveJsonData = data;
                }
            }

            Debug.Log("�÷��̾� Json ������ �ҷ����� ����");
        },
        Error =>
        {
            Debug.Log("�÷��̾� Json ������ �ҷ����� ����");
        });
    }






    public void BuyItem() //���� ������ ���� �׽�Ʈ
    {
        itemData.Clear(); //�ʱ�ȭ
        itemData.Add("Item1"); //������ ������ ����

        GrantItemToUser("Item", itemData); //Item �̶�� īŻ�α� �ȿ� �ִ� ������ �ְڴ�.
    }



    public void GrantItemToUser(string catalogversion, List<string> itemIds) //���� �κ��丮�� ������ ����
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "GrantItemToUser",
            FunctionParameter = new { CatalogVersion = catalogversion, ItemIds = itemIds },
            GeneratePlayStreamEvent = true,
        }, success =>
        {
            Debug.Log("������ ���� ����");
        }
        , error =>
        {
            Debug.Log("������ ���� ����, īŻ�αװ� �����ϴ���, īŻ�α� �ȿ� �������� �ִ��� Ȯ���� �ʿ��մϴ�");
        });
    }


    public void GetUserInventory() //���� �κ��丮 �ҷ�����
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result =>
        {
            var Inventory = result.Inventory;

            if (Inventory != null)
            {
                for (int i = 0; i < Inventory.Count; i++)
                {
                    itemList.Add(Inventory[i]);
                }

                foreach (ItemInstance list in itemList)
                {
                    if (list.ItemId.Equals("Item1"))
                    {
                        inventoryList[0] = (int)list.RemainingUses; //������ ������ ���� ��ŭ ����
                    }

                    if (list.ItemId.Equals("Item2"))
                    {
                        inventoryList[1] = (int)list.RemainingUses;
                    }
                }
            }
        }, error =>
        {
            Debug.Log("������ �κ��丮�� �ҷ����� ���߽��ϴ�.");
        });
    }
}
