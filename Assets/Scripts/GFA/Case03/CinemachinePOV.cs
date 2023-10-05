using Cinemachine;
using GFA.Case03.Mediators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CinemachinePOV:CinemachineExtension
{
   [SerializeField] private PlayerMediator _playerInput;
    private Vector3 startingRotation;
    [SerializeField]
    private float verticalSpeed=10f;
    [SerializeField]
    private float horizontalSpeed=10f;
    [SerializeField]
    private float clambAngle=80f;
    protected override void Awake()
    {
        if (startingRotation == null)
        {
            startingRotation = transform.localRotation.eulerAngles;
        }
        base.Awake();

        Cursor.visible = false;
    }

    
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                //if (startingRotation == null)
                //{
                //    startingRotation = transform.localRotation.eulerAngles;
                //}
                Vector2 deltaInput = _playerInput.GetMouseDelta();
                //state.PositionCorrection += new Vector3(deltaInput.x,0,deltaInput.y);
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.x=Mathf.Clamp(startingRotation.x, -clambAngle,clambAngle);
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clambAngle, clambAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }

}
