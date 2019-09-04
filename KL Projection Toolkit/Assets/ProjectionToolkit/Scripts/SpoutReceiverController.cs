using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Klak.Spout;

[RequireComponent(typeof(SpoutReceiver))]
public class SpoutReceiverController : MonoBehaviour
{
    public InputField inputField;
    private SpoutReceiver _spoutReceiver;

    private void OnEnable()
    {
        _spoutReceiver = GetComponent<SpoutReceiver>();
        inputField.text = _spoutReceiver.nameFilter;
    }

    public void ChangeReceiver()
    {
        // Coroutine example
        _spoutReceiver.nameFilter = inputField.text;
    }
}
