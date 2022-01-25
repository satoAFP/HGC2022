using Cinemachine;
using UnityEngine;

/// <summary>
/// �}�E�X�z�C�[���ɂ��Y�[�����s����悤�ɂ���g��
/// </summary>
/// 

public class ZoomCinemachineExtension : CinemachineExtension
{
    [Header("�ŏ��̃Y�[���ړ����̃x�N�g����")]
    [SerializeField] private Vector3 zome;

    [Header("�ŏ��̃Y�[���ړ����̎~�܂���W")]
    [SerializeField] private Vector3 stop_pos;

    [Header("�S�[�����̃J����X�ړ��̗�")]
    public float goal_camera_move_powe_x;

    [Header("�S�[�����̃J����Y�ړ��̗�")]
    public float goal_camera_move_powe_y;

    [Header("���̉�������Ȃ�")]
    public bool goal_camera_move = false;

    private int _scrollDelta;
    private float _adjustAngle;
    private bool first_move = true;
    
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

    // �}�E�X�z�C�[������`�F�b�N
    private void FixedUpdate()
    {
        // �}�E�X�z�C�[���̈ړ��ʎ擾
        var delta = Input.mouseScrollDelta.y;
        if (Mathf.Approximately(delta, 0))
            return;

        // �ړ��ʂ�ێ�
        _scrollDelta = (int)delta;
    }
}