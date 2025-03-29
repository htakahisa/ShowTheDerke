using System.Collections.Generic;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    void Awake()
    {
        ActivateRandomChildObject(gameObject);
        Invoke("LoadEnd", 3f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ActivateRandomChildObject(gameObject);
        }
    }

    void ActivateRandomChildObject(GameObject parent)
    {
        // 親オブジェクトの全ての子オブジェクトを取得
        List<GameObject> children = GetAllChildObjects(parent);

        if (children.Count == 0) return;

        // ランダムに1つの子オブジェクトを選ぶ
        int randomIndex = Random.Range(0, children.Count);
        GameObject selectedChild = children[randomIndex];

        // すべての子オブジェクトのアクティブ状態を設定
        foreach (GameObject child in children)
        {
            child.SetActive(child == selectedChild);
        }
    }

    List<GameObject> GetAllChildObjects(GameObject parent)
    {
        List<GameObject> childObjects = new List<GameObject>();
        foreach (Transform child in parent.transform)
        {
            childObjects.Add(child.gameObject);
        }
        return childObjects;
    }

    public void LoadEnd()
    {
        Destroy(gameObject);
    }

}
