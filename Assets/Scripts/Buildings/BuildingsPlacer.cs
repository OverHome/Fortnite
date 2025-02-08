using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsPlacer : MonoBehaviour
{
    public BuildingObject PlaceObject { get; private set; }

    public void StartPlacing(BuildingObject placeObject, Vector3 pos)
    {
        PlaceObject = placeObject;
        SetObjectPos(pos);
        PlaceObject.gameObject.SetActive(true);
    }

    public void EndPlacing()
    {
        PlaceObject.gameObject.SetActive(false);
        PlaceObject = null;
    }

    public void SetObjectPos(Vector3 pos)
    {
        PlaceObject.transform.position = pos - PlaceObject.BottomPoint;
    }
}
