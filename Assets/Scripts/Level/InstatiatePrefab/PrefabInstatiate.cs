using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstatiate
{
    /// <summary>
    /// Methode for Instantiating Prefabs during run-Time in a Parent in the Level
    /// </summary>
    /// <param name="parent"> Is the Parent where the prefab get instantiate</param>
    /// <param name="vector"> Are the coordinates where the prefabs is spawning, in relation to the Parent</param>
    /// <param name="prefabType">Is the path of the prefab in the resorces folder. </param>
    public GameObject InstantiatePrefab(GameObject parent, Vector3 vector, string prefabType)
    {
        var prefab = GameObject.Instantiate(Resources.Load(prefabType));

        GameObject prefabObj = (GameObject)prefab;
        prefabObj.transform.parent = parent.transform;
        prefabObj.transform.localPosition = vector;
        prefabObj.SetActive(true);

        return prefabObj;
    }
}
