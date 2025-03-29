using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;




public class DerkeDictionary : MonoBehaviour
{

    // resources�������`
    private Dictionary<string, GameObject> resources = new Dictionary<string, GameObject>();

    public static DerkeDictionary derkeDic;




    private void Awake()
    {
        LoadAllAddressables();
        derkeDic = this;
    }

    void LoadAllAddressables()
    {
        // Addressables�̑S�A�Z�b�g�̃A�h���X���擾
        Addressables.LoadResourceLocationsAsync("prefab").Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var locations = handle.Result;
                foreach (var location in locations)
                {
                    string address = location.PrimaryKey;
                    LoadAssetAsync(address);
                }
            }
            else
            {
                Debug.LogError("Failed to load addressable locations.");
            }
        };
    }

    void LoadAssetAsync(string address)
    {
        // Address���Ƃɔ񓯊��Ń��[�h
        Addressables.LoadAssetAsync<GameObject>(address).Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                // resources �ɒǉ��i�d���`�F�b�N�t���j
                if (!resources.ContainsKey(address))
                {
                    resources[address] = op.Result;
                    Debug.Log($"Asset '{address}' loaded and added to resources.");
                }
                else
                {
                    Debug.Log($"Asset '{address}' already exists in resources.");
                }
            }
            else
            {
                Debug.LogError($"Failed to load asset: {address}");
            }
        };
    }

    public GameObject GetDerke(string name)
    {
        return resources[name];
    }

}
