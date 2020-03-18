using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Homing : MonoBehaviour
{
    private GameObject player, enemy;
    private Transform target;
    private int routeChoice;    //プレイヤーを狙うミサイルか否か
    private int destroyTime = 5;    //消滅時間
    private float destroyTimer; //消滅時間を測る
    private float bulletPeriod = 3f;  //弾着
    private float keepWatching = 10f;  //相手を向かなくなる距離
    private Vector3 velocity;   //速度
    private Vector3 acceleration;   //加速度
    private Vector3 missileDistance;   //相手とミサイルの距離
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        routeChoice = Random.Range(0, 2);    //ターゲットを狙うか否か
        //velocity = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0); //初速度
        //Debug.Log(velocity);
    }

    // Update is called once per frame
    void Update()
    {

        destroyTimer += Time.deltaTime;
        target = enemy.transform;

        acceleration = Vector3.zero;

        if (routeChoice == 0)   //0 :狙う , 1 :狙わない
        {
            missileDistance = target.position - transform.position; //間の距離を代入

            if (missileDistance.z >= keepWatching)  //ターゲットを見続ける
            {
                var look = Quaternion.LookRotation(missileDistance);
                transform.localRotation = look;
            }
            else
            {
                rigid.constraints = RigidbodyConstraints.FreezeRotation;    //角度を固定
            }

        }
        else
        {
            missileDistance = target.position - player.transform.position;  //間の距離を代入
            rigid.constraints = RigidbodyConstraints.FreezeRotation;    //角度を代入
        }

        //加速度
        acceleration += (missileDistance - velocity * bulletPeriod) * 2f / (bulletPeriod * bulletPeriod);

        //加速度が一定以上だと追尾を弱くする
        if (acceleration.magnitude > 100f)
        {
            acceleration = acceleration.normalized * 100f;
        }

        // 着弾時間を徐々に減らしていく
        bulletPeriod -= Time.deltaTime;

        // 速度の計算
        velocity += acceleration * Time.deltaTime;

        if (destroyTimer >= destroyTime)    //消滅
            Destroy(gameObject);

        //Debug.Log(rigid);
    }
    void FixedUpdate()
    {
        // 移動処理
        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }
    void OnCollisionEnter()
    {
        // 何かに当たったら自分自身を削除
        //Destroy(gameObject);

    }
}
