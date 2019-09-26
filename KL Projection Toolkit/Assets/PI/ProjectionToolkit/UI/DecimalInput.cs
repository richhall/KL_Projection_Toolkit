using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PI.ProjectionToolkit.UI
{
    public class DecimalInput : MonoBehaviour
    {
        public InputField txtValue;
        public decimal step = 0.1m;
        public bool clamp = false;
        public float minimum = 0;
        public float maximum = 0;
        public delegate void floatItemDelegate(decimal value);
        public event floatItemDelegate OnValueChanged;

        public void SetData(float val)
        {
            txtValue.text = val.ToString();
        }

        public void SetData(decimal val)
        {
            txtValue.text = val.ToString();
        }

        public decimal GetData()
        {
            return decimal.Parse(txtValue.text);
        }

        public void Increase()
        {
            decimal val = GetData() + step;
            if (clamp && val > (decimal)maximum) val = (decimal)maximum;
            SetData(val);
            if (OnValueChanged != null) OnValueChanged(val);
        }

        public void Decrease()
        {
            decimal val = GetData() - step;
            if (clamp && val < (decimal)minimum) val = (decimal)maximum;
            SetData(val);
            if (OnValueChanged != null) OnValueChanged(val);
        }
    }
}