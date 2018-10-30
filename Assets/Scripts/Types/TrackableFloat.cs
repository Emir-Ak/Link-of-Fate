using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackableFloat : ScriptableObject {

    public delegate void FloatChangedEvent(float value);
    public event FloatChangedEvent OnValueChanged;

    private float _value;
    public float Value
    {
        get
        {
            return _value;
        }

        set
        {
            if (value != _value)
            {
                _value = value;
                OnValueChanged(value);
            }
        }
    }
}
