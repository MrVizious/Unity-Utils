#if ODIN_INSPECTOR
#if UNITY_NETCODE || UNITY_NETCODE_GAMEOBJECTS
using Sirenix.OdinInspector.Editor;
using Unity.Netcode;
using UnityEditor;

[CustomEditor(typeof(NetworkBehaviour), true)]
public class OdinNetworkBehaviourEditor : OdinEditor { }
#endif
#endif