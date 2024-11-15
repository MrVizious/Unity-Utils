#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Scaffolding
{

    public class ScaffoldingColorController : SerializedMonoBehaviour
    {
        public bool hideOnPlay = true;
        private Scaffold[] scaffolds => FindObjectsOfType<Scaffold>();
        [Button]
        public void ShowScaffolding()
        {
            foreach (Scaffold scaffolding in scaffolds)
            {
                scaffolding.showScaffold = true;
            }
        }

        [Button]
        public void HideScaffolding()
        {
            foreach (Scaffold scaffolding in scaffolds)
            {
                scaffolding.showScaffold = false;
            }
        }

        private void Start()
        {
            if (hideOnPlay) HideScaffolding();
        }
    }

}
#endif