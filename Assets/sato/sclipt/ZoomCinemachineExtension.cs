using Cinemachine;
using UnityEngine;

/// <summary>
/// マウスホイールによるズームを行えるようにする拡張
/// </summary>
/// 

public class ZoomCinemachineExtension : CinemachineExtension
{
    [Header("最初のズーム移動時のベクトル量")]
    [SerializeField] private Vector3 zome;

    [Header("最初のズーム移動時の止まる座標")]
    [SerializeField] private Vector3 stop_pos;

    [Header("ゴール時のカメラX移動の力")]
    public float goal_camera_move_powe_x;

    [Header("ゴール時のカメラY移動の力")]
    public float goal_camera_move_powe_y;

    [Header("この下いじらない")]
    public bool goal_camera_move = false;

    private int _scrollDelta;
    private float _adjustAngle;
    private bool first_move = true;
    
    public CinemachineVirtualCamera avcam;

    // カメラワーク処理
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime
    )
    {
        //最初の引きから寄りになるカメラの処理
        //グレゴリーさん感謝
        var transposer = avcam.GetCinemachineComponent<CinemachineTransposer>();
        if (first_move)
        {
            if ((transposer.m_FollowOffset.z > stop_pos.z))
                transposer.m_FollowOffset.z -= zome.z;
            else if ((transposer.m_FollowOffset.y > stop_pos.y))
            {
                transposer.m_FollowOffset.y -= zome.y;
            }
            else
            {
                first_move = false;
            }
        }

        if (goal_camera_move)
        {
            transposer.m_FollowOffset.z -= goal_camera_move_powe_x;
            transposer.m_FollowOffset.y += goal_camera_move_powe_y;
            //else if ((transposer.m_FollowOffset.y > stop_pos.y))
            //    transposer.m_FollowOffset.y -= zome.y;
        }

    }

    // マウスホイール操作チェック
    private void FixedUpdate()
    {
        // マウスホイールの移動量取得
        var delta = Input.mouseScrollDelta.y;
        if (Mathf.Approximately(delta, 0))
            return;

        // 移動量を保持
        _scrollDelta = (int)delta;
    }
}