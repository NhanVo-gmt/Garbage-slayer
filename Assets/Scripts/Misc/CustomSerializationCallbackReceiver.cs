using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomSerializationCallbackReceiver : ISerializationCallbackReceiver
{
    public void OnAfterDeserialize()
    {
        throw new System.NotImplementedException();
    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }
}
