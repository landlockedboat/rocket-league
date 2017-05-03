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
        for (int i = 0; i < fragments.Length; i++)
        {
            if (fragments[i] != null)
            {
                DestroyImmediate(fragments[i]);
                fragments[i] = null;
            }

            GameObject go = Instantiate(wallFragment, transform, true);
            go.transform.localPosition = new Vector3(
                0, 0, i * 5f - 22.5f);
            fragments[i] = go;
        }
    }
    void OnDisable()
    {
        for (int i = 0; i < fragments.Length; i++)
        {
            if (fragments[i] != null)
            {
                DestroyImmediate(fragments[i]);
                fragments[i] = null;
            }
        }
        // In case there is something else to be destroyed
        if (transform.childCount > 0)
        {
            Debug.LogWarning("Deleting children of " + name +
                transform.childCount + " childs left.");
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}

