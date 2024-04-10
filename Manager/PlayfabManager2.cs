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
    public List<int> inventoryList = new List<int>(); //유저 인벤토리 상태


    [SerializeField]
    public SaveJsonData saveJsonData = new SaveJsonData(); //Json 상태
    private Dictionary<string, string> jsonData = new Dictionary<string, string>();

    private List<string> itemData = new List<string>();
    private List<ItemInstance> itemList = new List<ItemInstance>();


    private void Awake() //맨 처음 초기화
    {
        inventoryList.Clear();

        for (int i = 0; i < 30; i ++) //30개 만큼 인벤토리 공간 확보
        {
            inventoryList.Add(0);
        }
    }


    public void SetPlayerJsonData() //서버에 Json 저장 예시
    {
        SaveJsonData data = new SaveJsonData();

        data.index1 = 1; //각각의 아이템 개수 지정
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

        //변동이 생길 경우 saveJsonData 에서 먼저 가지고 있는 개수를 불러오고 변동된 값 만큼 넣어준 뒤 다시 서버에 저장하면 됩니다.

        jsonData.Clear();
        jsonData.Add("SaveJsonData", JsonUtility.ToJson(data));
        SetPlayerJsonData(jsonData);
    }


    public void SetPlayerJsonData(Dictionary<string, string> data) //서버에 Json 데이터 저장
    {
        var request = new UpdateUserDataRequest() { Data = data, Permission = UserDataPermission.Public };
        PlayFabClientAPI.UpdateUserData(request, (result) =>
        {
            Debug.Log("플레이어 Json 서버 데이터 저장 성공!");
        },
        error =>
        {
            Debug.Log("플레이어 Json 서버 데이터 저장 실패");
        });
    }


    public void GetPlayerJsonData() //서버에서 Json 데이터 불러오기
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

            Debug.Log("플레이어 Json 데이터 불러오기 성공");
        },
        Error =>
        {
            Debug.Log("플레이어 Json 데이터 불러오기 실패");
        });
    }






    public void BuyItem() //유저 아이템 지급 테스트
    {
        itemData.Clear(); //초기화
        itemData.Add("Item1"); //지급할 아이템 지정

        GrantItemToUser("Item", itemData); //Item 이라는 카탈로그 안에 있는 곳에서 주겠다.
    }



    public void GrantItemToUser(string catalogversion, List<string> itemIds) //유저 인벤토리에 아이템 지급
    {
        PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
        {
            FunctionName = "GrantItemToUser",
            FunctionParameter = new { CatalogVersion = catalogversion, ItemIds = itemIds },
            GeneratePlayStreamEvent = true,
        }, success =>
        {
            Debug.Log("아이템 지급 성공");
        }
        , error =>
        {
            Debug.Log("아이템 지급 실패, 카탈로그가 존재하는지, 카탈로그 안에 아이템이 있는지 확인이 필요합니다");
        });
    }


    public void GetUserInventory() //유저 인벤토리 불러오기
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
                        inventoryList[0] = (int)list.RemainingUses; //보유한 아이템 개수 만큼 저장
                    }

                    if (list.ItemId.Equals("Item2"))
                    {
                        inventoryList[1] = (int)list.RemainingUses;
                    }
                }
            }
        }, error =>
        {
            Debug.Log("유저의 인벤토리를 불러오지 못했습니다.");
        });
    }
}
