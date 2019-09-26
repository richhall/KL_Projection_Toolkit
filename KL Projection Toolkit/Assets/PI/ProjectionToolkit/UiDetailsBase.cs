using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PI.ProjectionToolkit.Models;
using TMPro;
using PI.ProjectionToolkit.UI;

namespace PI.ProjectionToolkit
{
    public class UiDetailsBase : MonoBehaviour
    {
        public GameObject prefabUiHeader;
        public GameObject prefabUiSubHeader;
        public GameObject prefabUiTextLine;
        public GameObject prefabUiTextLineButton;
        public GameObject prefabUiTextMultiLine;
        public GameObject prefabUiSwitch;
        public GameObject prefabUiEditTextLine;
        public GameObject prefabSeperator;
        public GameObject objList;
        public Color colorAlert;

        void Start()
        {
        }

        public void AddTransform(PI.Common.Transform transform, string prefix = "")
        {
            prefix = string.IsNullOrEmpty(prefix) ? "" : prefix + " ";
            AddTextLine(prefix + "POSITION", transform.position);
            AddTextLine(prefix + "ROTATION", transform.rotation);
            AddTextLine(prefix + "SCALE", transform.scale);
        }


        public void AddProjectorStack(List<ProjectorStack> stacks, bool addHeader = true, string header = "PROJECTOR STACK")
        {
            foreach (var stack in stacks) AddProjectorStack(stack, addHeader, header);
        }

        public void AddProjectorStack(ProjectorStack stack, bool addHeader = true, string header = "PROJECTOR STACK")
        {
            if (addHeader) AddHeader(header);
            AddTextLine("NAME", stack.name);
            AddTextLine("VERSION", stack.version);
            AddTransform(stack, "STACK ");
            AddTextLine("NOTES", stack.notes, true);
            AddCamera(stack.projectors, true, true, "PROJECTOR");
            AddSeperator();
        }

        public void AddCamera(List<PI.ProjectionToolkit.Models.Camera> cameras, bool addHeader = false, bool addSubHeader = false, string header = "CAMERA")
        {
            foreach (var camera in cameras) AddCamera(camera, addHeader, addSubHeader, header);
        }

        public void AddCamera(PI.ProjectionToolkit.Models.Camera camera, bool addHeader = false, bool addSubHeader = false, string header = "CAMERA")
        {
            if (addHeader) AddHeader(header, addSubHeader);
            AddTextLine("NAME", camera.name);
            AddTransform(camera, "");
            AddTextLine("FIELD OF VIEW", camera.fieldOfView);
            if (camera.cameraType == Models.CameraType.Projector)
            {
                AddTextLine("FOCAL LENGTH", camera.focalLength);
                AddTextLine("SENSOR SIZE", camera.sensorSize);
                AddTextLine("LENSE SHIFT", camera.lensShift);
                switch (camera.gateFit)
                {
                    case UnityEngine.Camera.GateFitMode.Fill:
                        AddTextLine("LENSE SHIFT", "FILL");
                        break;
                    case UnityEngine.Camera.GateFitMode.Horizontal:
                        AddTextLine("LENSE SHIFT", "HORIZONTAL");
                        break;
                    case UnityEngine.Camera.GateFitMode.None:
                        AddTextLine("LENSE SHIFT", "NONE");
                        break;
                    case UnityEngine.Camera.GateFitMode.Overscan:
                        AddTextLine("LENSE SHIFT", "OVERSCAN");
                        break;
                    case UnityEngine.Camera.GateFitMode.Vertical:
                        AddTextLine("LENSE SHIFT", "VIRTICAL");
                        break;
                }
            }
            AddTextLine("CLIPPING PLANE - NEAR", camera.nearClipPlane);
            AddTextLine("CLIPPING PLANE - FAR", camera.nearClipPlane);
        }

        public void AddHeader(string header, bool subHeader = false)
        {
            var prefab = subHeader ? prefabUiSubHeader : prefabUiHeader;
            var projectGameObject = Instantiate(prefab, objList.transform);
            var u = projectGameObject.GetComponent<Header>();
            u.SetData(header.ToUpper());
        }

        public TextLine AddTextLine(string title, object obj)
        {
            return AddTextLine(title, obj.ToString());
        }

        public TextLine AddTextLine(string title, PI.Common.Vector2 vector2)
        {
            return AddTextLine(title, vector2.x.ToString() + "," + vector2.y.ToString());
        }

        public TextLine AddTextLine(string title, PI.Common.Vector3 vector3)
        {
            return AddTextLine(title, vector3.x.ToString() + "," + vector3.y.ToString() + "," + vector3.z.ToString());
        }

        public TextLine AddTextLine(string title, PI.Common.Quaternion quaternion)
        {
            return AddTextLine(title, quaternion.x.ToString() + "," + quaternion.y.ToString() + "," + quaternion.z.ToString() + "," + quaternion.w.ToString());
        }

        public TextLine AddTextLine(string title, string value, bool multi = false, bool alert = false, int height = 0)
        {
            var obj = multi ? prefabUiTextMultiLine : prefabUiTextLine;
            value = multi ? value : value.ToUpper();
            var projectGameObject = Instantiate(obj, objList.transform);
            var u = projectGameObject.GetComponent<TextLine>();
            if (alert)
            {
                u.SetData(title.ToUpper(), value, colorAlert, height);
            } else
            {
                u.SetData(title.ToUpper(), value, height);
            }
            return u;
        }

        public TextLine AddEditTextLine(string title, string value, bool required = true, bool multi = false)
        {
            var obj = multi ? prefabUiTextMultiLine : prefabUiEditTextLine;
            value = multi ? value : value.ToUpper();
            var projectGameObject = Instantiate(obj, objList.transform);
            var u = projectGameObject.GetComponent<TextLine>();
            u.SetData(title.ToUpper(), value);
            return u;
        }


        public TextLineButton AddTextLineButton(string title, string value, Sprite icon, bool valueUpper = true)
        {
            if (valueUpper) value = value.ToUpper();
            var projectGameObject = Instantiate(prefabUiTextLineButton, objList.transform);
            var u = projectGameObject.GetComponent<TextLineButton>();
            if (u != null) u.SetData(title.ToUpper(), value, icon);
            return u;
        }

        public void AddSeperator()
        {
            Instantiate(prefabSeperator, objList.transform);
        }

        public SwitchLine AddSwitch(string title, bool isOn)
        {
            var gameObject = Instantiate(prefabUiSwitch, objList.transform);
            SwitchLine switchLine = gameObject.GetComponentInChildren<SwitchLine>();
            switchLine.SetData(title, isOn);
            return switchLine;
        }
    }
}