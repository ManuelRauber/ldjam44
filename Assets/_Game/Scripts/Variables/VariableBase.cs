using UnityEngine;

namespace LdJam44.Variables
{
    public abstract class VariableBase<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        public T InitialValue;
        public T Value;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Value = InitialValue;
        }

        public static implicit operator T(VariableBase<T> variable)
        {
            return variable.Value;
        }   
    }
}
