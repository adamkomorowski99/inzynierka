using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class TankControls : MonoBehaviour
{
    [SerializeField] public HingeJoint leftMotor, rightMotor;
    [SerializeField] public float maxVelocity;
    private InputDevice _leftController, _rightController;

    private bool _leftControllerInitialized = false, _rightControllerInitialized = false;

    private void Start()
    {
        OVRManager.SetSpaceWarp(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_leftControllerInitialized)
        {
            var leftHandedControllers = new List<InputDevice>();
            var desiredCharacteristics = InputDeviceCharacteristics.Left;
            InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);
            if (leftHandedControllers.Count>0)
            {
                _leftController = leftHandedControllers[0];
                _leftControllerInitialized = true;
            }
        }

        if (!_rightControllerInitialized)
        {
            var rightHandedControllers = new List<InputDevice>();
            var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right |
                                     InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);
            if (rightHandedControllers.Count > 0)
            {
                _rightController = rightHandedControllers[0];
                _rightControllerInitialized = true;
            }
        }


        _leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out var leftJoystick);
        _rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out var rightJoystick);

        leftMotor.useMotor = Mathf.Abs(leftJoystick.y) > 0.01f;
        rightMotor.useMotor = Mathf.Abs(rightJoystick.y) > 0.01f;
        var leftMotorMotor = leftMotor.motor;
        leftMotorMotor.targetVelocity = leftJoystick.y * maxVelocity;
        leftMotor.motor = leftMotorMotor;
        var rightMotorMotor = rightMotor.motor;
        rightMotorMotor.targetVelocity = rightJoystick.y * maxVelocity;
        rightMotor.motor = rightMotorMotor;
    }
}
