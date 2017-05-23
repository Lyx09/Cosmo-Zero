using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Lock_m : MonoBehaviour
{
    //The code needs to be simplified
    //NOTE: There exist a dead center when locking a target it is located at the exact oposite of the target.

    [Tooltip("Put the player's camera here")]
    public Camera camera;
    public Transform target;
    public Text LockHP;
    public Text LockName;
    public RectTransform LockHPRect;
    public RectTransform LockNameRect;

    public RectTransform arrowTransform;
    public RectTransform lockTransform;
    private Vector2 velocity = Vector2.zero;
    public Transform XhairShip;
    public RectTransform XhairUI;
    public bool circle = false;
    public float radius = 200;

    [Tooltip("Take the closest enemy to the mouse position or closest to crosshair position")]
    public bool closestMouse = false;

    [Tooltip("Distance between the arrow and the edge of the screen when target is off-screen")]
    public int arwDistFromEdge = 10;
    //Change target icon color when locking

    private Vector2 middleScreen = new Vector2(Screen.width / 2F, Screen.height / 2F);

    void Start()
    {
        if (LockHP == null)
        {
            LockHP = GameObject.Find("LockHP").GetComponent<Text>();
        }

        target = null;
        LockHP.alignment = TextAnchor.MiddleCenter;
        LockName.alignment = TextAnchor.MiddleCenter;
        LockHPRect.anchorMin = Vector2.zero;
        LockHPRect.anchorMax = Vector2.zero;
        LockNameRect.anchorMin = Vector2.zero;
        LockNameRect.anchorMax = Vector2.zero;
        lockTransform.anchorMin = Vector2.zero;
        lockTransform.anchorMax = Vector2.zero;
        lockTransform.anchoredPosition = camera.WorldToScreenPoint(XhairShip.position);
        arrowTransform.anchorMin = Vector2.zero;
        arrowTransform.anchorMax = Vector2.zero;
        XhairUI.anchorMax = Vector2.zero;
        XhairUI.anchorMin = Vector2.zero;
    }

    void Update()
    {
        XhairUI.anchoredPosition = (Vector2)camera.WorldToScreenPoint(XhairShip.position) - XhairUI.sizeDelta / 2;

        if (target == null)
        {
            //Smooth 
            lockTransform.anchoredPosition = Vector2.SmoothDamp(lockTransform.anchoredPosition, XhairUI.anchoredPosition - lockTransform.sizeDelta / 2, ref velocity, 0.3F);
            arrowTransform.anchoredPosition = new Vector2(-10, -10);
            LockHP.text = "";
            LockName.text = "";
        }
        else
        {
            Vector3 ScreenTargetPos = camera.WorldToScreenPoint(target.position);
            if (ScreenTargetPos.z > 0)
            {
                lockTransform.anchoredPosition = (Vector2)ScreenTargetPos - lockTransform.sizeDelta / 2;
                LockHPRect.anchoredPosition = (Vector2)ScreenTargetPos - new Vector2(0, lockTransform.sizeDelta.x  / 2 + 5);
                LockNameRect.anchoredPosition = (Vector2) ScreenTargetPos + new Vector2(0,lockTransform.sizeDelta.x/2 + 5);
                if (target.GetComponent<State>() != null)
                    LockHP.text = target.GetComponent<State>().life.ToString();
                else if (target.GetComponent<EnemyScript>() != null)
                    LockHP.text = target.GetComponent<EnemyScript>().life.ToString();
            }
            
            float posArrowX;
            float posArrowY;
            float left = 1;
            if (circle)
            {
                if (ScreenTargetPos.z < 0)
                {
                    ScreenTargetPos *= -1;
                }
                Vector2 ScreenMiddlePos = (Vector2)ScreenTargetPos - middleScreen;

                if (ScreenMiddlePos.magnitude > radius)
                {
                    if (ScreenTargetPos.x > Screen.width / 2F)
                    {
                        left = -1;
                    }
                    arrowTransform.anchoredPosition = (ScreenMiddlePos.normalized * radius) + middleScreen;
                    arrowTransform.rotation = Quaternion.Euler(0, 0, left * Vector2.Angle((Vector2)ScreenTargetPos - middleScreen, Vector2.up));
                }
                else
                {
                    arrowTransform.anchoredPosition = new Vector2(-10, -10); //hides the arrow
                }
            }
            else
            {
                if (ScreenTargetPos.z < 0)
                {
                    ScreenTargetPos *= -1;
                }

                if (ScreenTargetPos.x < 0 || ScreenTargetPos.x > Screen.width || ScreenTargetPos.y < 0 || ScreenTargetPos.y > Screen.height)
                {
                    if (ScreenTargetPos.x > Screen.width / 2F)
                    {
                        left = -1;
                    }

                    if (ScreenTargetPos.x < arwDistFromEdge)
                    {
                        posArrowX = arwDistFromEdge;
                    }
                    else if (ScreenTargetPos.x > Screen.width - arwDistFromEdge)
                    {
                        posArrowX = Screen.width - arwDistFromEdge;
                    }
                    else
                    {
                        posArrowX = ScreenTargetPos.x;
                    }


                    if (ScreenTargetPos.y < arwDistFromEdge)
                    {
                        posArrowY = arwDistFromEdge;
                    }
                    else if (ScreenTargetPos.y > Screen.height - arwDistFromEdge)
                    {
                        posArrowY = Screen.height - arwDistFromEdge;
                    }
                    else
                    {
                        posArrowY = ScreenTargetPos.y;
                    }

                    arrowTransform.rotation = Quaternion.Euler(0, 0, left * Vector2.Angle((Vector2)ScreenTargetPos - middleScreen, Vector2.up));
                    //only works on half the screen
                    arrowTransform.anchoredPosition = new Vector2(posArrowX, posArrowY);
                }
                else
                {
                    arrowTransform.anchoredPosition = new Vector2(-10, -10); //hides the arrow
                }
            }

        }

        if (Input.GetButtonDown("Lock"))
        {

            //Closest from mouse or closest from screencenter ?
            //https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html

            //Searches all enemies visible on screen
            List<GameObject> visibleEnemies = new List<GameObject>();

            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");

            //Check enemies
            foreach (GameObject enemy in Enemies)
            {
                if (enemy.GetComponent<Renderer>().isVisible)
                {
                    visibleEnemies.Add(enemy);
                }
            }

            //check other Players
            //Should check friendly
            foreach (GameObject player in Players)
            {
                if (player.GetComponent<Renderer>().isVisible && this.name != player.name) //last condition prevents from locking the player itself
                {
                    visibleEnemies.Add(player);
                }
            }
            
            GameObject closestEnemy = null;
            float minDist = 1000; //take a big value

            //test closestMouse
            Vector2 landMark = closestMouse ? Input.mousePosition : camera.WorldToScreenPoint(XhairShip.position);
            
            foreach (GameObject visibleEnemy in visibleEnemies)
            {
                float Dist = ((Vector2) camera.WorldToScreenPoint(visibleEnemy.transform.position) - landMark).magnitude;
                if ( Dist < minDist)
                {
                    minDist = Dist;
                    closestEnemy = visibleEnemy;
                }
            }

            if (closestEnemy != null)
            {
                target = closestEnemy.transform;
                if (closestEnemy.tag == "Enemy" || closestEnemy.tag == "Player")
                {
                    LockHP.text = closestEnemy.GetComponent<State>().life.ToString();
                    LockName.text = closestEnemy.name;
                }
                else
                {
                    
                }
                
            }

            //Maybe use a raycast to see in object is bloking the view ?

        }

        if (Input.GetButtonDown("Unlock"))
        {
            target = null;
        }
    }
}
