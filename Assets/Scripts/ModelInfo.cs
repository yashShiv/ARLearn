using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInfo : MonoBehaviour
{
    [TextArea(5, 20), SerializeField, Tooltip("Information About the Model")] private string modelInfo;

    public string getModelInfo()
    {
        return modelInfo;
    }
}
