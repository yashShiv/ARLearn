using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlaceObjectOnOrigin : MonoBehaviour
{
    bool isModelSet = false;
    GameObject m_ObjectToPlace;
    GameObject gameObj;
    Vector3 originalScale;

    // Start is called before the first frame update
    private void Update()
    {
        if (isModelSet)
            return;
        gameObj = Instantiate(m_ObjectToPlace, new Vector3(0, 0, 0), new Quaternion());
        originalScale = gameObj.transform.localScale;
        isModelSet = true;
    }

    public void SetObject(GameObject obj)
    {
        m_ObjectToPlace = obj;
        Debug.Log("Object Placed " + m_ObjectToPlace.name);
    }

    public void ChangeSize(float m)
    {
        Debug.Log("Called ChangeSize m = " + m);
        gameObj.transform.localScale = new Vector3(m * originalScale.x, 
                                                   m * originalScale.y, 
                                                   m * originalScale.z);
        //Undo.RegisterCreatedObjectUndo(gameObj, "Created instance");
    }
}
