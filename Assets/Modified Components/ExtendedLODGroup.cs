using UltEvents;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks.Triggers;
using Cysharp.Threading.Tasks;
using ExtensionMethods;
using Sirenix.OdinInspector;
using UnityEditor;


[RequireComponent(typeof(LODGroup))]
public class ExtendedLODGroup : MonoBehaviour
{

    [SerializeField]
    public const int NUMBER_OF_LODS = 3;

    [SerializeField]
    private LODController[] _lodControllers;
    public LODController[] lodControllers
    {
        get
        {
            if (_lodControllers == null
            || _lodControllers.Length <= 0
            || Array.IndexOf(_lodControllers, null) >= 0)
            {
                PrepareLODs();
            }
            return _lodControllers;
        }
        private set
        {
            _lodControllers = value;
        }
    }

    private LODGroup _lodGroup;
    public LODGroup lodGroup
    {
        get
        {
            if (_lodGroup == null) TryGetComponent<LODGroup>(out _lodGroup);
            return _lodGroup;
        }
    }

    [Button]
    public void PrepareLODs()
    {
        LODController[] childrenLODControllers = GetComponentsInChildren<LODController>();
        if (childrenLODControllers.Length < NUMBER_OF_LODS)
        {
            CreateLODs();
        }
        else
        {
            lodControllers = childrenLODControllers;
        }
        LOD[] lods = new LOD[3];

        for (int i = 0; i < lodControllers.Length; i++)
        {
            LOD newLOD = new LOD(1f / (i * 2 + 3), lodControllers[i].GetComponents<MeshRenderer>());
            lods[i] = newLOD;
        }
        lodGroup.SetLODs(lods);
    }



    private void CreateLODs()
    {
        LODController[] newLODControllers = new LODController[3];
        for (int i = 0; i < 3; i++)
        {
            newLODControllers[i] = new GameObject("LOD_" + i).AddComponent<LODController>();
            newLODControllers[i].transform.position = transform.position;
            newLODControllers[i].transform.SetParent(transform);
        }
        lodControllers = newLODControllers;
    }

}
