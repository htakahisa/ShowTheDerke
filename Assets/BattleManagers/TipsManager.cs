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
        // �e�I�u�W�F�N�g�̑S�Ă̎q�I�u�W�F�N�g���擾
        List<GameObject> children = GetAllChildObjects(parent);

        if (children.Count == 0) return;

        // �����_����1�̎q�I�u�W�F�N�g��I��
        int randomIndex = Random.Range(0, children.Count);
        GameObject selectedChild = children[randomIndex];

        // ���ׂĂ̎q�I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�ݒ�
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
