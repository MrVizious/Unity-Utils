using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExtendedPhysicsRaycaster : PhysicsRaycaster
{
    public float maxDistance = Mathf.Infinity;

    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        if (eventCamera == null)
            return;

        var ray = eventCamera.ScreenPointToRay(eventData.position);

        float dist = Mathf.Min(maxDistance, eventCamera.farClipPlane);

        var hits = Physics.RaycastAll(ray, dist, finalEventMask);

        if (hits.Length > 0)
        {
            System.Array.Sort(hits, (r1, r2) => r1.distance.CompareTo(r2.distance));

            for (int b = 0, bmax = hits.Length; b < bmax; b++)
            {
                var result = new RaycastResult
                {
                    gameObject = hits[b].collider.gameObject,
                    module = this,
                    distance = hits[b].distance,
                    worldPosition = hits[b].point,
                    worldNormal = hits[b].normal,
                    screenPosition = eventData.position,
                    index = resultAppendList.Count,
                    sortingLayer = 0,  // Optional: add sorting layer logic
                    sortingOrder = 0   // Optional: add sorting order logic
                };

                resultAppendList.Add(result);
            }
        }
    }
}