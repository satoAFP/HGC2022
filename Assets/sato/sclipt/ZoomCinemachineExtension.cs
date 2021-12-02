using Cinemachine;
using UnityEngine;

/// <summary>
/// �}�E�X�z�C�[���ɂ��Y�[�����s����悤�ɂ���g��
/// </summary>
public class ZoomCinemachineExtension : CinemachineExtension
{
    // ��p�̍ŏ��l
    [SerializeField] private float _minFOV = 10;

    // ��p�̍ő�l
    [SerializeField] private float _maxFOV = 80;

    // ��p�̕ύX�P��
    [SerializeField] private float _angleStep = 10;

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
        //�O���S���[���񊴎�
        var transposer = avcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(0.0f, 2.5f, -4.0f);

        // Aim�̒��ゾ�����������{
        if (stage != CinemachineCore.Stage.Aim)
            return;
        var lens = state.Lens;

        // �}�E�X�z�C�[�����삪��������
        if (_scrollDelta != 0)
        {
            // ��p���X�N���[�������ɕύX
            _adjustAngle = Mathf.Clamp(
                _adjustAngle - _scrollDelta * _angleStep,
                _minFOV - lens.FieldOfView,
                _maxFOV - lens.FieldOfView
            );

            _scrollDelta = 0;
        }

        // state�̓��e�͖��񃊃Z�b�g�����̂ŁA
        // ����␳����K�v������
        lens.FieldOfView += _adjustAngle;

        state.Lens = lens;
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