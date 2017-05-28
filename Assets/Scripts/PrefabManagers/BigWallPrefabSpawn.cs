using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BigWallPrefabSpawn : MonoBehaviour
{

    [SerializeField]
    GameObject wallFragment;

    GameObject[] fragments = new GameObject[10];

    void OnEnable()
    {
        if (transform.childCount >= 10)
            return;

        for (int i = 0; i < fragments.Length; i++)
        {
            GameObject go = Instantiate(wallFragment, transform, true);
            go.transform.localPosition = new Vector3(
                0, 0, i * 5f - 22.5f);
            go.transform.rotation = transform.rotation;
            fragments[i] = go;
        }
    }    
}

