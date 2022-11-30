using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour
{

    public GameObject fishPrefab;
    //public Transform InstantiatePos;
    public int tankSize = 5; //这个参数很重要，控制鱼群范围
    public int numFish = 10; //控制鱼群数量
    [HideInInspector] public GameObject[] allFish;
    [HideInInspector] public Vector3 goalPos = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        allFish = new GameObject[numFish];
        fishPrefab.GetComponent<Flock>().manager = transform.gameObject;

        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize), //这个参数很重要，控制不同鱼群不同的初始位置
                                      Random.Range(-tankSize * 3, tankSize * 3),
                                      Random.Range(-tankSize, tankSize)) + transform.position;
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 10000) < 500)
        {
            goalPos = new Vector3(Random.Range(-tankSize, tankSize),
                                 Random.Range(-tankSize * 3, tankSize * 3),
                                 Random.Range(-tankSize, tankSize));
        }
    }
}
