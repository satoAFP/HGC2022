using Cinemachine;
using UnityEngine;

/// <summary>
/// �}�E�X�z�C�[���ɂ��Y�[�����s����悤�ɂ���g��
/// </summary>
/// 

public class ZoomCinemachineExtension : CinemachineExtension
{
    [SerializeField] private Vector3 zome;

    private int _scrollDelta;
    private float _adjustAngle;
    
    public CinemachineVirtualCamera avcam;

    // �J�������[�N����
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime
    )
    {
        //�ŏ��̈���������ɂȂ�J�����̏���
        //�O���S���[���񊴎�
        var transposer = avcam.GetCinemachineComponent<CinemachineTransposer>();
        if ((transposer.m_FollowOffset.y >= 2.5))
            transposer.m_FollowOffset -= zome;

    }

    // �}�E�X�z�C�[������`�F�b�N
    private void Update()
    {
        // �}�E�X�z�C�[���̈ړ��ʎ擾
        var delta = Input.mouseScrollDelta.y;
        if (Mathf.Approximately(delta, 0))
            return;

        // �ړ��ʂ�ێ�
        _scrollDelta = (int)delta;
    }
}