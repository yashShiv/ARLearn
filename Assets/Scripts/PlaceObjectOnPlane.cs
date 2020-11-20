using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class PlaceObjectOnPlane : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
    bool isModelSet = false;
    bool isModelPlaced = false;
    
    GameObject m_ObjectToPlace;
    GameObject gameObj;
    
    Vector3 originalScale;

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    Quaternion rotate;
    Quaternion current;


    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnload;
    }

    private void OnSceneUnload(Scene scene)
    {
        isModelPlaced = false;
        isModelSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isModelSet)
            return;

        if (Input.touchCount > 0)
        {

            if(!isModelPlaced)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                    {
                        Pose hitPose = s_Hits[0].pose;
                        float distance = s_Hits[0].distance;
                        GameObject model = m_ObjectToPlace.transform.GetChild(0).gameObject;
                        if (distance < 3f)
                            model.transform.localScale = new Vector3(distance / 2, distance / 2, distance / 2);
                        gameObj = Instantiate(m_ObjectToPlace, hitPose.position, hitPose.rotation);
                        originalScale = gameObj.transform.localScale;
                        isModelPlaced = true;
                    }
                }
            }

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerUp = touch.position;
                    fingerDown = touch.position;
                }

                //Detects Swipe while finger is still moving
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!detectSwipeOnlyAfterRelease)
                    {
                        fingerDown = touch.position;
                        checkSwipe();
                    }
                }

                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }
        }
    }

    void checkSwipe()
    {
        //Check if Horizontal swipe
        if (horizontalValMove() > SWIPE_THRESHOLD)
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    void OnSwipeLeft()
    {
        rotate = Quaternion.Euler(0, Mathf.PI*horizontalValMove(), 0);
        current = gameObj.transform.localRotation;
        gameObj.transform.localRotation = Quaternion.Slerp(current, current * rotate, Time.deltaTime);
    }

    void OnSwipeRight()
    {
        rotate = Quaternion.Euler(0, -Mathf.PI*horizontalValMove(), 0);
        current = gameObj.transform.localRotation;
        gameObj.transform.localRotation = Quaternion.Slerp(current, current * rotate, Time.deltaTime);
    }

    public void SetObject(GameObject obj)
    {
        isModelSet = true;
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
