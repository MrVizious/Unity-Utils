#if ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
using Unity.Netcode;
using UnityEditor;

[CustomEditor(typeof(NetworkBehaviour), true)]
public class OdinNetworkBehaviourEditor : OdinEditor { }
#endif