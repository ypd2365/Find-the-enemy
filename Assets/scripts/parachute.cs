using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class parachute : MonoBehaviour
{
    public Text sentence;//UI text object on parachute
    public GameObject bomb;

    public float fallSpeed = 1.0f;//parachute falling speed
    //float lastTimeClick;
    const float m_doubleTime = 0.2f;//time to check for double click
    float m_doubleStart;
    GameManager gamemanager;


    private void Awake()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);//parachute falling down


#if UNITY_ANDROID
        GetMobileInput();
#endif
        

    }
    
    void OnMouseOver()//for editor
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if ((Time.time - m_doubleStart) <= m_doubleTime)
                {
                    if (hit.collider.gameObject.name == "correct")
                    {
                        gamemanager.decreaseLives();
                       
                        Destroy(hit.collider.gameObject);
                        gamemanager.countDestroyedParachutes++;
                        if (gamemanager.lives<=0)
                        {
                            gamemanager.gameOver(true);
                            return;
                        }
                        if (gamemanager.countDestroyedParachutes ==10)
                        {
                            Debug.Log("complete1");
                            gamemanager.gameComplete();
                        }


                    }
                    else if(hit.collider.gameObject.name == "incorrect")
                    {
                        
                        gamemanager.increaseScore(hit.collider.gameObject.transform.position);
                        gamemanager.countDestroyedParachutes++;
                        Destroy(hit.collider.gameObject);
                        if (gamemanager.countDestroyedParachutes == 10)
                        {
                            Debug.Log("complete2");
                            gamemanager.gameComplete();
                        }
                    }
                }
                m_doubleStart = Time.time;
                // Debug.Log(hit.collider.gameObject.name);
               

            }
        }
    }
   
   IEnumerator blastEffect(float time,Vector3 pos)
    {
        yield return new WaitForSeconds(time);
       

    }
    private void GetMobileInput()//for mobile
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if ((Time.time - m_doubleStart) <= m_doubleTime)
                    {
                        if (hit.collider.gameObject.name == "correct")
                        {
                            gamemanager.decreaseLives();

                            Destroy(hit.collider.gameObject);
                            gamemanager.countDestroyedParachutes++;

                            if (gamemanager.lives <= 0)
                            {
                                gamemanager.gameOver(true);
                                return;
                            }
                            if (gamemanager.countDestroyedParachutes == 10)
                            {
                                Debug.Log("complete1");
                                gamemanager.gameComplete();
                            }


                        }
                        else if (hit.collider.gameObject.name == "incorrect")
                        {

                            gamemanager.increaseScore(hit.collider.gameObject.transform.position);
                            gamemanager.countDestroyedParachutes++;
                            Destroy(hit.collider.gameObject);
                            if (gamemanager.countDestroyedParachutes == 10)
                            {
                                Debug.Log("complete2");
                                gamemanager.gameComplete();
                            }
                        }
                    }
                    m_doubleStart = Time.time;
                    // Debug.Log(hit.collider.gameObject.name);


                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)//check collision with ground
    {
        if(collision.gameObject.name=="ground")
        {
            
            if(gameObject.name=="incorrect")//if its wrong sentence then gameover
            {
                //gamemanager.spawn.StopAllCoroutines();
                gamemanager.gameOver(false);
            }
            else//if its correct sentence and last sentence of level then game complete
            {
                gamemanager.countDestroyedParachutes++;
                if (gamemanager.countDestroyedParachutes == 10)
                {
                    Debug.Log("complete4");
                    gamemanager.gameComplete();
                }
            }
            Destroy(gameObject);//destroys parachute after touching ground
        }
       
    }

    //public static bool IsDoubleTap()
    //{
    //    bool result = false;
    //    float MaxTimeWait = 1;
    //    float VariancePosition = 1;

    //    if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
    //    {
    //        Debug.Log("came");
    //        float DeltaTime = Input.GetTouch(0).deltaTime;
    //        float DeltaPositionLenght = Input.GetTouch(0).deltaPosition.magnitude;

    //        if (DeltaTime > 0 && DeltaTime < MaxTimeWait && DeltaPositionLenght < VariancePosition)
    //            result = true;
    //    }
    //   // Debug.Log(result);
    //    return result;
    //}

    

}
