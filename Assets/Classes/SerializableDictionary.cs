using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<KeyType, ValueType> : Dictionary<KeyType, ValueType>, ISerializationCallbackReceiver
{
    public List<KeyType> SerializedKeys = new();
    public List<ValueType> SerializedValues = new();

    public void OnAfterDeserialize()
    {
        SynchroniseToSerializedData();
    }

    public void OnBeforeSerialize() { }

#if UNITY_EDITOR
    public void EditorOnly_Add(KeyType InKey, ValueType InValue)
    {
        SerializedKeys.Add(InKey);
        SerializedValues.Add(InValue);
    }
#endif // UNITY_EDITOR

    public void SynchroniseToSerializedData()
    {
        this.Clear();
        
        // If there is valid data, build the dictionary
        if((SerializedKeys != null) && (SerializedValues != null))
        {
            int NumElements = Mathf.Min(SerializedKeys.Count, SerializedValues.Count);
            for (int i = 0; i < NumElements; i++)
            {
                this[SerializedKeys[i]] = SerializedValues[i];
            }
        }
        else
        {
            SerializedKeys = new();
            SerializedValues = new();
        }

        // If the lists are out of sync, rebuild the lists.
        if (SerializedKeys.Count != SerializedValues.Count)
        {
            SerializedKeys = new(Keys);
            SerializedValues = new(Values);
        }
    }
}
