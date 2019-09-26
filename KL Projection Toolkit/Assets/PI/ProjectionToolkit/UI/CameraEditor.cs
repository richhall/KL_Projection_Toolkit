using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PI.ProjectionToolkit.Models;

namespace PI.ProjectionToolkit.UI
{
    public class CameraEditor : MonoBehaviour
    {
        private PI.ProjectionToolkit.Models.Camera camera;
        public DecimalInput posX;
        public DecimalInput posY;
        public DecimalInput posZ;
        public DecimalInput rotationX;
        public DecimalInput rotationY;
        public DecimalInput rotationZ;
        public DecimalInput fieldOfView;
        public DecimalInput focalLength;
        public DecimalInput sensorX;
        public DecimalInput sensorY;
        public DecimalInput lensShiftX;
        public DecimalInput lensShiftY;

        public GameObject objDisplaySeperator;
        public GameObject objDisplayTitle;
        public GameObject objDisplay2;
        public Toggle checkDisplay2;
        public GameObject objDisplay3;
        public Toggle checkDisplay3;
        public GameObject objDisplay4;
        public Toggle checkDisplay4;

        private bool settingUp = true;

        public delegate void cameraEditorItemDelegate(PI.ProjectionToolkit.Models.Camera camera, bool updateDisplay2, bool updateDisplay3, bool updateDisplay4);
        public event cameraEditorItemDelegate OnCameraChanged;

        void Start()
        {
            posX.OnValueChanged += PosX_OnValueChanged;
            posY.OnValueChanged += PosY_OnValueChanged;
            posZ.OnValueChanged += PosZ_OnValueChanged;
            rotationX.OnValueChanged += RotationX_OnValueChanged;
            rotationY.OnValueChanged += RotationY_OnValueChanged;
            rotationZ.OnValueChanged += RotationZ_OnValueChanged;
            fieldOfView.OnValueChanged += FieldOfView_OnValueChanged;
            focalLength.OnValueChanged += FocalLength_OnValueChanged;
            sensorX.OnValueChanged += SensorX_OnValueChanged;
            sensorY.OnValueChanged += SensorY_OnValueChanged;
            lensShiftX.OnValueChanged += LensShiftX_OnValueChanged;
            lensShiftY.OnValueChanged += LensShiftY_OnValueChanged;
        }


        public void TriggerCameraChange()
        {
            if (!settingUp && OnCameraChanged != null) OnCameraChanged(this.camera, checkDisplay2.isOn, checkDisplay3.isOn, checkDisplay4.isOn);
        }


        private void PosX_OnValueChanged(decimal value)
        {
            if(!settingUp) camera.position.x = (float)value;
            TriggerCameraChange();
        }

        private void PosY_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.position.y = (float)value;
            TriggerCameraChange();
        }

        private void PosZ_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.position.z = (float)value;
            TriggerCameraChange();
        }

        private void RotationX_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.rotation.x = (float)value;
            TriggerCameraChange();
        }

        private void RotationY_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.rotation.y = (float)value;
            TriggerCameraChange();
        }

        private void RotationZ_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.rotation.z = (float)value;
            TriggerCameraChange();
        }

        private void FieldOfView_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.fieldOfView = (float)value;
            TriggerCameraChange();
        }

        private void FocalLength_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.focalLength = (float)value;
            TriggerCameraChange();
        }

        private void SensorX_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.sensorSize.x = (float)value;
            TriggerCameraChange();
        }

        private void SensorY_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.sensorSize.y = (float)value;
            TriggerCameraChange();
        }

        private void LensShiftX_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.lensShift.x = (float)value;
            TriggerCameraChange();
        }

        private void LensShiftY_OnValueChanged(decimal value)
        {
            if (!settingUp) camera.lensShift.y = (float)value;
            TriggerCameraChange();
        }

        public void SetDisplays(bool showDisplay2, bool showDisplay3, bool showDisplay4)
        {
            settingUp = true;
            checkDisplay2.isOn = false;
            checkDisplay3.isOn = false;
            checkDisplay4.isOn = false;
            objDisplaySeperator.SetActive(showDisplay2 || showDisplay3 || showDisplay4);
            objDisplayTitle.SetActive(showDisplay2 || showDisplay3 || showDisplay4);
            objDisplay2.SetActive(showDisplay2);
            objDisplay3.SetActive(showDisplay3);
            objDisplay4.SetActive(showDisplay4);
            settingUp = false;
        }

        public void SetData(PI.ProjectionToolkit.Models.Camera camera, bool attachedToDisplay2, bool attachedToDisplay3, bool attachedToDisplay4)
        {
            settingUp = true;
            this.camera = camera;
            posX.SetData(this.camera.position.x);
            posY.SetData(this.camera.position.y);
            posZ.SetData(this.camera.position.z);
            rotationX.SetData(this.camera.rotation.x);
            rotationY.SetData(this.camera.rotation.y);
            rotationZ.SetData(this.camera.rotation.z);
            fieldOfView.SetData(this.camera.fieldOfView);
            focalLength.SetData(this.camera.focalLength);
            sensorX.SetData(this.camera.sensorSize.x);
            sensorY.SetData(this.camera.sensorSize.y);
            lensShiftX.SetData(this.camera.lensShift.x);
            lensShiftY.SetData(this.camera.lensShift.y);
            checkDisplay2.isOn = attachedToDisplay2;
            checkDisplay3.isOn = attachedToDisplay3;
            checkDisplay4.isOn = attachedToDisplay4;
            settingUp = false;
        }

        public PI.ProjectionToolkit.Models.Camera GetCamera()
        {
            return this.camera;
        }

    }
}