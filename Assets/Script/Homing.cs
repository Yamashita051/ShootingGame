using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Homing : MonoBehaviour
{
    private GameObject player, enemy;
    private float time;
    private Vector3 velocity, position;
    private Transform target;
    private float period = 2f;
    Rigidbody rigid;
    public Vector3 acceleration;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        velocity = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //position = enemy.transform.position;
        target = enemy.transform;

        acceleration = Vector3.zero;

        //ターゲットと自分自身の差
        var diff = target.position - transform.position;

        //加速度
        acceleration += (diff - velocity * period) * 2f / (period * period);

        //加速度が一定以上だと追尾を弱くする
        if (acceleration.magnitude > 100f)
        {
            acceleration = acceleration.normalized * 100f;
        }

        // 着弾時間を徐々に減らしていく
        period -= Time.deltaTime;

        // 速度の計算
        velocity += acceleration * Time.deltaTime;

        if (time >= 5)
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
