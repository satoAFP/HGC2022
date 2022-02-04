using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //インスペクターで設定----------------------------------------------------
    [Header("自動操作モード")]
    public bool auto_move;

    [Header("主人公が壁に触れているときの判定オブジェクト")]
    public GameObject[] Around_collision;

    [Header("カード最大選択回数")]
    public int Max_Card;

    [Header("カードの種類数")]
    public int Kind_Card;

    [Header("移動速度")]
    public float moveSpeed;

    [Header("走る速度(移動速度の何倍か)")]
    public float runSpeed;

    [Header("前方向への力(幅跳び、スライディングで使用)")]
    public float flontMove;

    [Header("ジャンプ力")]
    public float push_power;

    [Header("壁キックのジャンプ力の倍率")]
    public float wall_kick_power;

    [Header("壁キックの横移動する時間")]
    public int walljump_time;

    [Header("壁キックの横移動する最初のパワー")]
    public float walljump_first_power;

    [Header("ハイジャンプ力(ジャンプの何倍か)")]
    public float highjump_power;

    [Header("最大ジャンプ回数")]
    public int Max_Jmup;

    [Header("選択されたアクション表示用テキスト")]
    public Text Select_text;

    [Header("表示用テキストオブジェクト")]
    public GameObject Select_text_obj;

    [Header("壁で止まって爆散するまでの時間")]
    public int stop_efect_time;

    [Header("壁で止まって死ぬまでの時間")]
    public int stop_deth_time;

    [Header("落下判定とってからの死ぬまでの時間")]
    public int fall_deth_time;

    [Header("死亡エフェクト")]
    public GameObject Deth_efect;

    [Header("死亡エフェクトが出た時消す主人公スキン")]
    public GameObject Delete_skin;

    [Header("ゲームが始まるカウントを表示するテキスト")]
    public Text start_time_text;

    [Header("ゲームが始まるカウントの秒数")]
    public int start_time;

    [Header("ゴール時のクラッカーエフェクト")]
    public GameObject goal_cracker;

    [Header("王冠エフェクト時の星エフェクト")]
    public GameObject clown_star;

    [Header("スライムのアニメーション")]
    [Header("アニメーション管理変数---------------------------------------------------")]
    public Animator anim;

    [Header("ヒストリーのスライムのモデル")]
    public GameObject sura_model;


    [Header("カード選択時のSE")]
    [Header("サウンド管理変数---------------------------------------------------------")]
    public AudioClip se_card;

    [Header("アクション時のSE")]
    public AudioClip se_action;

    [Header("王冠取得時のSE")]
    public AudioClip se_clown;

    [Header("最初のスタートカウント時のSE")]
    public AudioClip se_start_count;

    [Header("最初のスタートカウント終了時のSE")]
    public AudioClip se_start_count_end;

    [Header("ゴール時のSE")]
    public AudioClip se_goal;

    //他オブジェクトでも使用
    [Header("主人公を止める用")]
    [Header("ここから下は触らない------------------------------------------------------")]
    public bool Movestop = true;        //主人公が動くかどうか

    public bool count_check = false;    //最初のカウントが終わるとき判定をとる

    [Header("王冠取得判定用")]
    public int clown_get = 0;

    [Header("ミッション判定用")]
    public int[] Use_Card_Amount;

    [Header("幅跳び引っかかる問題")]
    public bool Longjump_check = false;


    //private変数--------------------------------------------------------------
    private Vector3 push;                   //加算したいベクトル量
    private float inputX = 0;               //X軸の移動ベクトル
    private float inputZ = 1;               //Z軸の移動ベクトル
    private int Select_order = 0;           //ボタンを押された順番を記憶
    private bool[] Action_check;            //アクションを一回しか使えないよう管理
    private int[] Card_order;               //カードを選択した順番を記憶
    private bool wall_stick = false;        //壁にくっつける状態
    private bool around_collision_check = false;//プレイヤーの周りの当たり判定に壁があるとtrueになる
    private Vector3 sura_angle;             //壁にくっついている時のスライムの角度調整
    private Vector3 sura_pos;               //壁にくっついている時のスライムの位置調整
    private bool all_stick = false;         //壁くっつき状態をアクションリセットまで続行
    private Vector3 squrt_check;            //しゃがむときの主人公位置変更
    private float walljump = 0.0f;          //壁ジャンプするときのジャンプ力
    private bool walljump_check = false;    //壁ジャンプかどうか判断
    private float run_power = 1;            //移動速度代入
    private Vector3 flont_push;             //移動方向へより力を加える(幅跳びで使用)
    private Vector3 flont_sliding;          //移動方向へより力を加える(スライディングで使用)
    private string[] text_data;             //アクション内容格納変数
    private bool select_time = true;        //開始ボタンを押すと、カード選択できない
    private bool safe_flag = true;          //死亡判定用フラグ
    private Vector3 stop_check;             //フレーム毎に主人公の座標取得
    private int stop_time_count = 0;        //主人公のストップ時、実際カウントする変数
    private int start_time_count = 0;       //スタート時、実際カウントする変数
    private int start_text_time_count = 3;  //実際にテキストに秒数を入れる変数
    private AudioSource audio;              //使用するオーディオソース
    private Select_Card_Manager SCM;        //Select_Card_Manager格納スクリプト
    private int after_card_order = -1;      //使用した最新のカード情報記憶
    private bool fall_deth_flag = false;    //落下で死亡時trueになる
    private int camera_num = 0;             //何番カメラ使用か決定用
    private bool first_ground_check = false;//最初地面に着いた時の判定


    //構造体-------------------------------------------------------------------
    //ボタン使用時周り
    //private struct Buttan
    //{
    //    public bool push;      //ボタンが押されたかの判定
    //    public int push_num;   //押された時の順番記憶
    //    //初期化用関数
    //    public Buttan(bool a, int b) {
    //        push = a;
    //        push_num = b;
    //    }
    //}
    ////構造体の初期化
    //Buttan jump = new Buttan(false, 0);
    //Buttan squat = new Buttan(false, 0);
    //Buttan stick = new Buttan(false, 0);
    //Buttan stop = new Buttan(false, 0);

    //列挙----------------------------------------------------------------------
    //カードの種類
    public enum Card
    {
        JUMP,
        SQUAT,
        STICK,
        RUN,
        HIGHJUMP,
        WALLKICK,
        LONGJUMP,
        SLIDING,
    }


    // Start is called before the first frame update
    void Start() {
        //初期化
        push = new Vector3(0.0f, push_power, 0.0f);

        Card_order = new int[Max_Card];

        for (int i = 0; i < Max_Card; i++)  {
            Card_order[i] = -1;
        }

        Action_check = new bool[Kind_Card];

        text_data = new string[Max_Card];

        for (int i = 0; i < Max_Card; i++) {
            text_data[i] = "";
        }

        stop_check = new Vector3(0.0f, 0.0f, 0.0f);

        squrt_check = new Vector3(0.0f, 0.25f, 0.0f);

        //ステージ内にあるSE_manager格納
        audio = GameObject.Find("SE_manager").GetComponent<AudioSource>();
        //スタートのカウント時のSE再生
        audio.PlayOneShot(se_start_count);

        //Select_Card_Managerを取得
        SCM = Select_text_obj.GetComponent<Select_Card_Manager>();


        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        //スタート処理-------------------------------------------------
        //インスペクターで設定した秒数待ってスタート
        if (Movestop == true)
        {
            start_time_count++;
            if (start_time_count == start_time)
            {
                start_time_text.gameObject.SetActive(false);
                count_check = true; //pauseに判定を送る
                Movestop = false;   //アクションループのメイン部分を動かす
                Select_order = 0;   //アクションブロックに乗った時、最初に加算されてしまうから-1
                select_time = false;//アクション開始するとカードを選択できない
                audio.PlayOneShot(se_start_count_end);
            }
            //60フレーム毎に１秒減らす
            if ((start_time_count % 60) == 0 && start_time_count <= 120) 
            {
                start_text_time_count--;
                //スタートのカウント時のSE再生
                audio.PlayOneShot(se_start_count);
            }
            //テキストに秒数を出力
            start_time_text.text = "" + start_text_time_count;
        }


        //Card_orderの一番目にデータが入ってないとき順番を一つずらす
        if (Card_order[0] == -1)
        {
            Card_order[0] = Card_order[1];
            text_data[0] = text_data[1];
            SCM.Select[0].GetComponent<Image>().sprite = SCM.Select[1].GetComponent<Image>().sprite;
            for (int i = 1; i < Max_Card - 1; i++) 
            {
                Card_order[i] = Card_order[i + 1];
                text_data[i] = text_data[i + 1];
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Select[i + 1].GetComponent<Image>().sprite;
            }
            //カードの一番最後のデータを初期化
            Card_order[Max_Card - 1] = -1;
            text_data[Max_Card - 1] = "";
            SCM.Select[Max_Card - 1].GetComponent<Image>().sprite = null;
        }

        //選んだアクションをtext_dataに格納
        for (int i = 0; i < Max_Card; i++)  {
            if(Card_order[i] == (int)Card.JUMP) {
                text_data[i] = "          →";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.JUMP];
            }
            if (Card_order[i] == (int)Card.SQUAT) {
                text_data[i] = "          →";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.SQUAT];
            }
            if (Card_order[i] == (int)Card.STICK) {
                text_data[i] = "          →";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.STICK];
            }
            if (Card_order[i] == (int)Card.RUN) {
                text_data[i] = "          →";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.RUN];
            }
            if (Card_order[i] == (int)Card.HIGHJUMP) {
                text_data[i] = "          →";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.HIGHJUMP];
            }
            if (Card_order[i] == (int)Card.WALLKICK) {
                text_data[i] = "          →";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.WALLKICK];
            }
            if (Card_order[i] == (int)Card.LONGJUMP) {
                text_data[i] = "          →";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.LONGJUMP];
            }
            if (Card_order[i] == (int)Card.SLIDING) {
                text_data[i] = "          →";
                SCM.Select[i].GetComponent<Image>().sprite = SCM.Card_img[(int)Card.SLIDING];
            }

            if (Card_order[i]==-1)
                SCM.Select[i].SetActive(false);
            else
                SCM.Select[i].SetActive(true);

        }
        //選択したアクション実際表示
        Select_text.text = "" + text_data[0] + text_data[1] + text_data[2] + text_data[3] + text_data[4];

        //壁に触れるとaround_collision_check = trueにする
        if (Around_collision[0].GetComponent<Around_collider>().wall_check == true ||
            Around_collision[1].GetComponent<Around_collider>().wall_check == true) 
        {
            //左右それぞれ壁に触れているときの見た目を調整
            if (Around_collision[0].GetComponent<Around_collider>().wall_check == true) 
            {
                around_collision_check = true;
                sura_angle = new Vector3(45.0f, 0.0f, -90.0f);
                sura_pos = new Vector3(-0.5f, 0.0f, 0.0f);
            }
            if (Around_collision[1].GetComponent<Around_collider>().wall_check == true) 
            {
                around_collision_check = true;
                sura_angle = new Vector3(-45.0f, 0.0f, 90.0f);
                sura_pos = new Vector3(0.5f, 0.0f, 0.0f);
            }
        }
        else 
        {
            around_collision_check = false;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        //幅跳び選択時、ジャンプブロックに引っかかる問題解決
        if (Around_collision[2].GetComponent<Around_collider>().wall_check == true)
        {
            if (after_card_order == (int)Card.LONGJUMP || after_card_order == (int)Card.SLIDING) 
            {
                Longjump_check = true;
            }
        }

        //幅跳び、スライディングで使用する移動量の向き変更
        if (inputX == -1) {
            flont_push = new Vector3(-flontMove, push_power, 0.0f);
            flont_sliding = new Vector3(-flontMove, 0.0f, 0.0f);
        }
        if (inputX == 1) {
            flont_push = new Vector3(flontMove, push_power, 0.0f);
            flont_sliding = new Vector3(flontMove, 0.0f, 0.0f);
        }
        if (inputZ == -1) {
            flont_push = new Vector3(0.0f, push_power, -flontMove);
            flont_sliding = new Vector3(0.0f, 0.0f, -flontMove);
        }
        if (inputZ == 1) {
            flont_push = new Vector3(0.0f, push_power, flontMove);
            flont_sliding = new Vector3(0.0f, 0.0f, flontMove);
        }


        //最初アクションを選択するときと、セレクトブロックに到達するといったん止める処理
        if (Movestop == false) {

            //壁で止まった時死ぬ処理
            if (stop_check == this.gameObject.transform.position)
            {
                stop_time_count++;

                if (stop_efect_time == stop_time_count) 
                {
                    //死亡時のエフェクト
                    Deth_efect.SetActive(true);
                    Delete_skin.SetActive(false);
                    
                    //SE流す
                    audio.PlayOneShot(se_action);
                }
                if (stop_efect_time + stop_deth_time == stop_time_count) 
                {
                    stop_time_count = 0;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            stop_check = this.gameObject.transform.position;

            //移動処理
            MOVE(inputX, inputZ);

            //Select_orderが-1のとき配列がエラーを起こすので
            if (Select_order != -1) {

                //ジャンプを選択したとき--------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.JUMP] == true) {
                    //ジャンプさせる処理
                    this.GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);

                    //ジャンプアニメーション移行
                    anim.SetBool("jump", true);

                    //ジャンプ処理終了
                    Action_check[(int)Card.JUMP] = false;
                }


                //しゃがみを選択したとき--------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.SQUAT] == true) {
                    //y軸のサイズ変更
                    this.gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);

                    //しゃがみ処理終了
                    Action_check[(int)Card.SQUAT] = false;
                }


                //くっつきを選択したとき--------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.STICK] == true) {
                    //くっつき状態維持
                    all_stick = true;

                    //前に壁がある処理
                    //if (Around_collision[2].GetComponent<Around_collider>().wall_check == true)
                    //    wall_stick = true;
                    //else
                    //    wall_stick = false;
                }
                if (all_stick == true) 
                {
                    if (around_collision_check == true)
                    {
                        //Y軸が動かないよう固定
                        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                        //スライムの調整
                        //sura_model.transform.Rotate(sura_angle);
                        sura_model.transform.localEulerAngles = sura_angle;
                        sura_model.transform.localPosition = sura_pos;
                    }
                    else
                    {
                        //壁がなくなると元の状態に戻す
                        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        sura_model.transform.localEulerAngles = new Vector3(0.0f, 45.0f, 0.0f);
                        sura_model.transform.localPosition = new Vector3(0.0f, -0.5f, 0.0f);
                    }
                }


                //走るを選択したとき------------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.RUN] == true) {

                    //倍率が変わる
                    run_power = runSpeed;

                    //ジャンプアニメーション移行
                    //anim.SetBool("run", true);

                    Action_check[(int)Card.RUN] = false;
                }


                //ハイジャンプを選択したとき----------------------------------------------------------------------------------------
                if (Action_check[(int)Card.HIGHJUMP] == true) {
                    //ジャンプさせる処理
                    this.GetComponent<Rigidbody>().AddForce(push * highjump_power, ForceMode.Impulse);

                    //ジャンプアニメーション移行
                    anim.SetBool("jump", true);

                    Action_check[(int)Card.HIGHJUMP] = false;
                }


                //壁キックを選択したとき--------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.WALLKICK] == true) {
                    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    //ジャンプさせる処理
                    this.GetComponent<Rigidbody>().AddForce(push * wall_kick_power, ForceMode.Impulse);

                    //ジャンプアニメーション移行
                    anim.SetBool("jump", true);

                    //左に壁がある処理
                    if (Around_collision[0].GetComponent<Around_collider>().wall_check == true) {
                        walljump_check = true;
                        walljump = walljump_first_power;
                    }
                    //右に壁がある処理
                    if (Around_collision[1].GetComponent<Around_collider>().wall_check == true) {
                        walljump_check = true;
                        walljump = -walljump_first_power;
                    }

                    Action_check[(int)Card.WALLKICK] = false;
                }
                //壁ジャンプ処理
                if (walljump_check == true) {
                    if (walljump_time != 0) {
                        transform.Translate(walljump, 0.0f, 0.0f);
                        if (walljump < 0)
                            walljump += walljump_first_power / walljump_time;
                        else
                            walljump -= walljump_first_power / walljump_time;
                    }
                    else
                        walljump_check = false;
                    walljump_time--;
                }


                //幅跳びを選択したとき----------------------------------------------------------------------------------------------
                if (Action_check[(int)Card.LONGJUMP] == true) {
                    this.GetComponent<Rigidbody>().AddForce(flont_push, ForceMode.Impulse);

                    //ジャンプアニメーション移行
                    anim.SetBool("jump", true);

                    Action_check[(int)Card.LONGJUMP] = false;
                }

                //スライディングを選択したとき--------------------------------------------------------------------------------------
                if (Action_check[(int)Card.SLIDING] == true) {

                    this.GetComponent<Rigidbody>().AddForce(flont_sliding, ForceMode.Impulse);

                    this.gameObject.transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);

                    Action_check[(int)Card.SLIDING] = false;
                }
            }
        }

        //落下死亡判定時のエフェクト
        if (fall_deth_flag)
        {
            stop_time_count++;

            if (fall_deth_time - 50 == stop_time_count)
            {
                //死亡時のエフェクト
                Deth_efect.SetActive(true);
                Delete_skin.SetActive(false);
                
                //SE流す
                audio.PlayOneShot(se_action);
            }
            if (fall_deth_time == stop_time_count)
            {
                stop_time_count = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }
    

    void OnCollisionEnter(Collision collision) {
        //自動移動設定時、このオブジェクトに触れると、指定方向に移動する
        if (collision.gameObject.tag == "Move_direction") {

            if (camera_num == -1)
                camera_num = 3;

            switch(collision.gameObject.GetComponent<Direction>().direction) {
                case 1://左
                    this.gameObject.transform.Rotate(new Vector3(0, -90, 0));

                    //カメラの向き変更
                    camera_num--; 
                    if (camera_num == -1)
                        camera_num = 3;
                    GameObject.Find("VCameraManeger").GetComponent<VCamera_maneger>().CameraChange(camera_num);
                    break;
                case 2://右
                    this.gameObject.transform.Rotate(new Vector3(0, 90, 0));

                    //カメラの向き変更
                    if (camera_num == 4)
                        camera_num = 0;
                    camera_num++;
                    GameObject.Find("VCameraManeger").GetComponent<VCamera_maneger>().CameraChange(camera_num);
                    break;
            }
        }

        if (collision.gameObject.tag == "Ground") {
            //着地判定
            anim.SetBool("jump", false);
            if (first_ground_check)
                audio.PlayOneShot(se_action);
            first_ground_check = true;
        }


        //アクションを選択した順番に実行される
        if (collision.gameObject.tag == "Action") 
        {
            //一度乗ったアクションブロックは消す
            collision.gameObject.SetActive(false);

            //今のカード処理内容記憶
            after_card_order = Card_order[0];

            //次のアクションのフラグをtrueにする
            switch (Card_order[0]) {
                case (int)Card.JUMP:
                    Action_check[(int)Card.JUMP] = true;
                    Use_Card_Amount[(int)Card.JUMP]++;
                    //アクション時のSE再生
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.SQUAT:
                    Action_check[(int)Card.SQUAT] = true;
                    Use_Card_Amount[(int)Card.SQUAT]++;
                    //アクション時のSE再生
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.STICK:
                    Action_check[(int)Card.STICK] = true;
                    Use_Card_Amount[(int)Card.STICK]++;
                    //アクション時のSE再生
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.RUN:
                    Action_check[(int)Card.RUN] = true;
                    Use_Card_Amount[(int)Card.RUN]++;
                    //アクション時のSE再生
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.HIGHJUMP:
                    Action_check[(int)Card.HIGHJUMP] = true;
                    Use_Card_Amount[(int)Card.HIGHJUMP]++;
                    //アクション時のSE再生
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.WALLKICK:
                    Action_check[(int)Card.WALLKICK] = true;
                    Use_Card_Amount[(int)Card.WALLKICK]++;
                    //アクション時のSE再生
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.LONGJUMP:
                    Action_check[(int)Card.LONGJUMP] = true;
                    Use_Card_Amount[(int)Card.LONGJUMP]++;
                    //アクション時のSE再生
                    audio.PlayOneShot(se_action);
                    break;
                case (int)Card.SLIDING:
                    Action_check[(int)Card.SLIDING] = true;
                    Use_Card_Amount[(int)Card.SLIDING]++;
                    //アクション時のSE再生
                    audio.PlayOneShot(se_action);
                    break;
            }
            //実行したアクションを最低数として設定
            GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().executed_Action(Card_order[0]);
            //アクションの内容消去
            Card_order[0] = -1;

            //幅跳びのバグ修正関連
            Longjump_check = false;
        }

        //アクション再選択
        if (collision.gameObject.tag == "Select") {
            //元のサイズに戻す
            this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            //しゃがみ状態解除
            Action_check[(int)Card.SQUAT] = false;

            //くっつき状態解除
            Action_check[(int)Card.STICK] = false;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            wall_stick = false;
            all_stick = false;

            //走る状態解除
            run_power = 1.0f;

            //動きを止める
            Movestop = true;

            //再選択できる
            select_time = true;

            //順番を最初からにする
            Select_order = 0;

            //アクション選択の中身初期化
            for (int i = 0; i < Max_Card; i++) {
                Card_order[i] = -1;
            }

            //アクション表示テキストの中身初期化
            for (int i = 0; i < Max_Card; i++) {
                text_data[i] = "";
            }
        }

        //アクション内容リセット
        if (collision.gameObject.tag == "action_delete") {
            //元のサイズに戻す
            this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            //しゃがみ状態解除
            Action_check[(int)Card.SQUAT] = false;

            //くっつき状態解除
            Action_check[(int)Card.STICK] = false;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            wall_stick = false;
            all_stick = false;

            //走る状態解除
            run_power = 1.0f;

            //幅跳びのバグ修正関連
            Longjump_check = false;
        }

        //ゴール処理　リザルトに飛ぶ
        if (collision.gameObject.tag == "Goal") {
            this.gameObject.GetComponent<Goal_After>().goal_move = true;

            //ゴール時のSE
            audio.PlayOneShot(se_goal);

            Destroy(collision.gameObject);

            //ゴール時のエフェクト
            goal_cracker.SetActive(true);

            //ミッションUI非表示
            GameObject.Find("Mission_UI").SetActive(false);
            GameObject.Find("select_card_UI").SetActive(false);

            GameObject.Find("ActionBotton").GetComponent<ActionButton_SC>().Set_OffActive();
        }



        //ジャンプブロックに触れた時
        if (collision.gameObject.tag == "Jumpblock")
        {
            //オブジェクト削除
            Destroy(collision.gameObject);
            if (after_card_order == (int)Card.STICK || after_card_order == (int)Card.RUN || after_card_order == -1)
            {
                //ジャンプさせる処理
                this.GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);

                //ギミック使用時初期化
                after_card_order = -1;

                //ジャンプアニメーション移行
                anim.SetBool("jump", true);
            }
            else if (after_card_order == (int)Card.SQUAT) 
            {
                //元のサイズに変更
                this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                
                //ジャンプさせる処理
                this.GetComponent<Rigidbody>().AddForce(push * 1.6f, ForceMode.Impulse);

                //ギミック使用時初期化
                after_card_order = -1;

                //ジャンプアニメーション移行
                anim.SetBool("jump", true);
            }
            else if (after_card_order == (int)Card.JUMP)
            {
                //ジャンプさせる処理
                this.GetComponent<Rigidbody>().AddForce(push * 0.5f, ForceMode.Impulse);

                //ギミック使用時初期化
                after_card_order = -1;

                //ジャンプアニメーション移行
                anim.SetBool("jump", true);
            }
        }
    }


    void OnCollisionStay(Collision collision) {
        //くっつき状態の時、壁に触れているとY軸への力付与
        if (collision.gameObject.tag == "Wall") {
            if (wall_stick == true) {
                transform.Translate(0.0f, 0.2f, 0.0f);
            }
            //着地判定
            anim.SetBool("jump", false);
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        //王冠取得時
        if (collider.gameObject.tag == "clown")
        {
            //SE流す
            audio.PlayOneShot(se_clown);

            clown_get++;
            Destroy(collider.gameObject);

            //王冠取得時のエフェクト
            clown_star.SetActive(true);

        }

        //加速床の処理
        if (collider.gameObject.tag == "Acceleration")
        {
            //オブジェクト削除
            Destroy(collider.gameObject);
            if (after_card_order == (int)Card.JUMP || after_card_order == (int)Card.SQUAT ||
                after_card_order == (int)Card.STICK || after_card_order == (int)Card.RUN || after_card_order == -1)
            {
                //倍率が変わる
                run_power = runSpeed;

                //ギミック使用時初期化
                after_card_order = -1;
            }
        }

    }
    //死亡処理
    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "safe_zone") {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            fall_deth_flag = true;
            
        }
    }


    //移動処理関数
    private void MOVE(float x, float z) {
        if (auto_move == false) {
            //左右操作
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }
        //速度の設定
        float moveX = x * moveSpeed * Time.deltaTime * run_power;
        float moveZ = z * moveSpeed * Time.deltaTime * run_power;
        transform.Translate(moveX, 0.0f, moveZ);
    }

    //ボタンでの操作選択----------------------------------------------------------------
    //通常アクション-----------------------------------
    public void Push_jump() {//ジャンプボタン
        //Card_orderの一番目にデータが入ってないとき順番を一つずらす
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //カードの一番最後のデータを初期化
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.JUMP;  //ボタンを押した順番を記憶
                break;
            }
        }

        //カードめくられた時のSE再生
        audio.PlayOneShot(se_card);
    }

    public void Push_squat()
    {//しゃがみボタン
     //Card_orderの一番目にデータが入ってないとき順番を一つずらす
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //カードの一番最後のデータを初期化
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.SQUAT;  //ボタンを押した順番を記憶
                break;
            }
        }
        //カードめくられた時のSE再生
        audio.PlayOneShot(se_card);
    }

    public void Push_stick()
    {//くっつきボタン
     //Card_orderの一番目にデータが入ってないとき順番を一つずらす
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //カードの一番最後のデータを初期化
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.STICK;  //ボタンを押した順番を記憶
                break;
            }
        }
        //カードめくられた時のSE再生
        audio.PlayOneShot(se_card);
    }

    public void Push_run()
    {//走るボタン
     //Card_orderの一番目にデータが入ってないとき順番を一つずらす
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //カードの一番最後のデータを初期化
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.RUN;  //ボタンを押した順番を記憶
                break;
            }
        }
        //カードめくられた時のSE再生
        audio.PlayOneShot(se_card);

    }

    //合体アクション------------------------------------
    public void Push_highjump()
    {//しゃがみ+ジャンプ＝ハイジャンプ
     //Card_orderの一番目にデータが入ってないとき順番を一つずらす
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //カードの一番最後のデータを初期化
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.HIGHJUMP;  //ボタンを押した順番を記憶
                break;
            }
        }
        //カードめくられた時のSE再生
        audio.PlayOneShot(se_card);
    }

    public void Push_wallkick()
    {//くっつき+ジャンプ＝壁キック
     //Card_orderの一番目にデータが入ってないとき順番を一つずらす
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //カードの一番最後のデータを初期化
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.WALLKICK;  //ボタンを押した順番を記憶
                break;
            }
        }
        //カードめくられた時のSE再生
        audio.PlayOneShot(se_card);
    }

    public void Push_longjump()
    {//走る+ジャンプ＝幅跳び
     //Card_orderの一番目にデータが入ってないとき順番を一つずらす
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //カードの一番最後のデータを初期化
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.LONGJUMP;  //ボタンを押した順番を記憶
                break;
            }
        }
        //カードめくられた時のSE再生
        audio.PlayOneShot(se_card);
    }

    public void Push_sliding()
    {//しゃがみ+走る＝スライディング
     //Card_orderの一番目にデータが入ってないとき順番を一つずらす
        if (Card_order[0] == -1)
        {
            for (int i = 0; i < Max_Card - 1; i++)
            {
                Card_order[i] = Card_order[i + 1];
            }
            //カードの一番最後のデータを初期化
            Card_order[Max_Card - 1] = -1;
        }
        for (int i = 0; i < Max_Card; i++)
        {
            if (Card_order[i] == -1)
            {
                Card_order[i] = (int)Card.SLIDING;  //ボタンを押した順番を記憶
                break;
            }
        }
        //カードめくられた時のSE再生
        audio.PlayOneShot(se_card);
    }

    //アクション開始ボタン
    public void Push_start() {
        Movestop = false;   //アクションループのメイン部分を動かす
        Select_order = 0;  //アクションブロックに乗った時、最初に加算されてしまうから-1
        select_time = false;//アクション開始するとカードを選択できない
    }

    //選択したアクションを一つ戻す
    public void Push_back() {
        //マルチ状態を戻すときに使うboolを取得
        if (GameObject.Find("BackButton").GetComponent<DeletAction>().multi_backflag == false)
        {
            for (int i = 0; i < Max_Card; i++)
            {
                if (Card_order[i] == -1)
                {
                    //0番目が最初のアクションなのでそれ未満にはならない
                    Card_order[i - 1] = -1;
                    text_data[i - 1] = "";   //カードテキストの中身消去
                    SCM.Select[i - 1].GetComponent<Image>().sprite = null;
                }
            }
            if (Card_order[Max_Card - 1] != -1)
            {
                Card_order[Max_Card - 1] = -1;
                text_data[Max_Card - 1] = "";   //カードテキストの中身消去
                SCM.Select[Max_Card - 1].GetComponent<Image>().sprite = null;
            }
        }
    }

    public void check() {
        for(int i=0;i<Select_order;i++) {
            Debug.Log($"{Card_order[i]}");
        }
    }


}