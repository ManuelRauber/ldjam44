using LdJam44.Managers.Lanes;
using UnityEngine;

namespace LdJam44.Variables
{
    [CreateAssetMenu(menuName = "LD44/Variables/Lanes")]
    public class LanesVariable : VariableBase<Lane[]>
    {
        public static implicit operator Lane[](LanesVariable variable)
        {
            return variable.Value;
        }
    }
}
