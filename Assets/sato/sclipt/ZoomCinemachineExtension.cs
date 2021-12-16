using Cinemachine;
using UnityEngine;

/// <summary>
/// マウスホイールによるズームを行えるようにする拡張
/// </summary>
/// 

public class ZoomCinemachineExtension : CinemachineExtension
{
    [SerializeField] private Vector3 zome;

    [SerializeField] private Vector3 stop_pos;

    private int _scrollDelta;
    private float _adjustAngle;
    
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
        if ((transposer.m_FollowOffset.z > stop_pos.z))
            transposer.m_FollowOffset.z -= zome.z;
        else if ((transposer.m_FollowOffset.y > stop_pos.y))
            transposer.m_FollowOffset.y -= zome.y;

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