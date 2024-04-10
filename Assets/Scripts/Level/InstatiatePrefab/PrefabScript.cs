using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScript : MonoBehaviour
{
   private PrefabInstatiate type = new PrefabInstatiate();

    [SerializeField]
    private GameObject PrefabParent;


    [SerializeField]
    private Vector3[] vectors;

    // Start is called before the first frame update


    void Start()
    {
        foreach (Vector3 vector in vectors)
        {

            if (PrefabParent.CompareTag("PrefabBigBox"))
            {
                type.InstantiatePrefab(PrefabParent, vector, "Prefab/Boxes/BigBox");

            }
            if (PrefabParent.CompareTag("PrefabSmallBox"))
            {
                type.InstantiatePrefab(PrefabParent, vector, "Prefab/Boxes/Box");
            }
            if (PrefabParent.CompareTag("PrefabSheep"))
            {
                type.InstantiatePrefab(PrefabParent, vector, "Prefab/Sheep/Sheep");
            }


        }

    }
    // The Map could be generated before runtime with code
    //private void Awake()
    //{
    //                if (PrefabParent.CompareTag("Grid"))
    //        {
    //            type.InstantiatePrefab(PrefabParent, vector, "Ground/Ground");
    //        }
  
    //}

}
