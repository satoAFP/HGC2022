using Cinemachine;
using UnityEngine;

/// <summary>
/// マウスホイールによるズームを行えるようにする拡張
/// </summary>
public class ZoomCinemachineExtension : CinemachineExtension
{
    // 画角の最小値
    [SerializeField] private float _minFOV = 10;

    // 画角の最大値
    [SerializeField] private float _maxFOV = 80;

    // 画角の変更単位
    [SerializeField] private float _angleStep = 10;

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
        //グレゴリーさん感謝
        var transposer = avcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(0.0f, 2.5f, -4.0f);

        // Aimの直後だけ処理を実施
        if (stage != CinemachineCore.Stage.Aim)
            return;
        var lens = state.Lens;

        // マウスホイール操作があったら
        if (_scrollDelta != 0)
        {
            // 画角をスクロール方向に変更
            _adjustAngle = Mathf.Clamp(
                _adjustAngle - _scrollDelta * _angleStep,
                _minFOV - lens.FieldOfView,
                _maxFOV - lens.FieldOfView
            );

            _scrollDelta = 0;
        }

        // stateの内容は毎回リセットされるので、
        // 毎回補正する必要がある
        lens.FieldOfView += _adjustAngle;

        state.Lens = lens;
    }

    // マウスホイール操作チェック
    private void Update()
    {
        // マウスホイールの移動量取得
        var delta = Input.mouseScrollDelta.y;
        if (Mathf.Approximately(delta, 0))
            return;

        // 移動量を保持
        _scrollDelta = (int)delta;
    }
}