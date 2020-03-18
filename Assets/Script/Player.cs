using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;   //弾
    private Vector3 playerPosition;
    private float timer, flagTimer; //次弾までのクールタイム , 初弾クールタイム
    private float span = 1.5f;  //連射スパン
    private bool bulletFlag;    //初弾フラグ
    private int blazeFire = 5;  //一度に発射する数
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = gameObject.transform.position;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Z))
        {
            //Debug.Log(null);

            if(timer >= span || bulletFlag == false)
            {
                bulletFlag = true;
                for (int i = 0; i < blazeFire; i++)
                {
                    Instantiate(bullet, playerPosition, Quaternion.identity);
                }
                flagTimer = 0f;
                timer = 0f;
            }
        }
        else if (bulletFlag == true)
        {
            flagTimer += Time.deltaTime;
            if (flagTimer >= span)
            {
                bulletFlag = false;
            }
        }
        else
        {
            timer = 0;
        }
        //Debug.Log(bulletFlag);
    }
}
