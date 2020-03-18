using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;   //弾
    private Vector3 playerPosition;
    private const int BLAZEFIRE= 5;  //一度に発射する数
    private Vector3[] missileAppear = new Vector3[BLAZEFIRE];   //出現位置格納
    private float timer, flagTimer; //次弾までのクールタイム , 初弾クールタイム
    private float span = 1.5f;  //連射スパン
    private bool bulletFlag;    //初弾フラグ
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = gameObject.transform.position;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < BLAZEFIRE; i++) //ランダムな出現位置を格納
        {
            missileAppear[i] = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
        }

        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Z))
        {
            //Debug.Log(null);

            if(timer >= span || bulletFlag == false)
            {
                bulletFlag = true;
                for (int i = 0; i < BLAZEFIRE; i++) //決めた数ミサイル出す
                {
                    Instantiate(bullet, missileAppear[i], Quaternion.identity);
                    //Instantiate(bullet, playerPosition, Quaternion.identity);
                }
                flagTimer = 0f;
                timer = 0f;
            }
        }
        else if (bulletFlag == true)    //初弾処理
        {
            flagTimer += Time.deltaTime;
            if (flagTimer >= span)
            {
                bulletFlag = false;
            }
        }
        else    //タイマーリセット
        {
            timer = 0;
        }
        //Debug.Log(bulletFlag);
    }
}
