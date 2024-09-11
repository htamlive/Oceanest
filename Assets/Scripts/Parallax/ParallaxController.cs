using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Finds all of the gameObjects that have a ParallaxLayer.cs script, and moves them!*/

public class ParallaxController : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float cameraPositionChangeX, float cameraPositionChangeY);
    public ParallaxCameraDelegate onCameraMove;
    private Vector2 oldMousePosition;
    readonly List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    Vector2 mousePosition;
    void Start()
    {
        onCameraMove += MoveLayer;
        FindLayers();
        mousePosition = Input.mousePosition;
        oldMousePosition = mousePosition;
    }

    private void FixedUpdate()
    {
        mousePosition = Input.mousePosition;

        if (mousePosition.x != oldMousePosition.x || (mousePosition.y) != oldMousePosition.y)
        {
            if (onCameraMove != null)
            {
                Vector2 cameraPositionChange;
                cameraPositionChange = new Vector2(oldMousePosition.x - mousePosition.x, oldMousePosition.y - mousePosition.y);
                onCameraMove(cameraPositionChange.x, cameraPositionChange.y);
            }

            oldMousePosition = new Vector2(mousePosition.x, mousePosition.y);
        }
    }

    //Finds all the objects that have a ParallaxLayer component, and adds them to the parallaxLayers list.
    void FindLayers()
    {
        parallaxLayers.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                parallaxLayers.Add(layer);
            }
        }
    }

    //Move each layer based on each layers position. This is being used via the ParallaxLayer script
    void MoveLayer(float positionChangeX, float positionChangeY)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.MoveLayer(positionChangeX, positionChangeY);
        }
    }
}