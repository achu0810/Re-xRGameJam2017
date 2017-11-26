using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMass : MonoBehaviour {


    [SerializeField]
    private GameObject massRoot;
    //一マスのグリッド
    [SerializeField]
    private GameObject mass;
    [SerializeField]
    private int width = 6;
    [SerializeField]
    private int length = 8;

    // Use this for initialization
    void Start()
    {
        GameObject go = Instantiate(mass) as GameObject;
        float xSize = go.GetComponent<Collider>().bounds.size.x;
        float ySize = go.GetComponent<Collider>().bounds.size.z;
        Destroy(go);

        GameObject[,] grids = new GameObject[length, width];

        for (int j = 0; j < length; j++)
        {
            for (int i = 0; i < width; i++)
            {
                grids[j, i] = Instantiate(mass, new Vector3(i * xSize, 0, j * ySize), Quaternion.identity) as GameObject;
            }
        }
        float aveX = (grids[0, width - 1].transform.position.x - grids[0, 0].transform.position.x) / 2.0f;
        float aveY = (grids[length - 1, 0].transform.position.y - grids[0, 0].transform.position.y) / 2.0f;

        for (int j = 0; j < length; j++)
        {
            for (int i = 0; i < width; i++)
            {
                grids[j, i].transform.position -= new Vector3(aveX, 0, aveY);
                grids[j, i].transform.parent = massRoot.transform;
            }
        }
    }
}
