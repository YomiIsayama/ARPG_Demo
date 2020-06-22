using SingleInstance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public IUserInput pi;
    //public Image lockDot;
    public float horizontalSpeed = 100f;
    public float verticalSpeed = 100f;
    public Image lockDot;
    public GameObject bag;
    public GameObject EscPanel;
    public GameObject commandsGroup;

    public bool lockState;
    public bool isAI = false;

    private GameObject PlayerHandle;
    private GameObject CameraHandle;
    private GameObject model;
    private GameObject camera;
    private LockTarget lockTarget;
    private float tempEulerx;


    void Awake()
    {
        EscPanel = GameObject.Find("Canvas/EscPanel");
        bag = GameObject.Find("Canvas/Bag");
        commandsGroup = GameObject.Find("commandsGroup");
    }
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        CameraHandle = transform.parent.gameObject;
        PlayerHandle = CameraHandle.transform.parent.gameObject;
        tempEulerx = 20;
        ActorController ac = PlayerHandle.GetComponent<ActorController>();
        model = ac.model;
        pi = ac.pi;
        lockState = false;
        if (!isAI)
        {
            camera = Camera.main.gameObject;
            lockDot.enabled = false;
        }
    }
    void FixedUpdate()
    {

        if (lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            //左右
            PlayerHandle.transform.Rotate(Vector3.up, pi.jright * horizontalSpeed * Time.fixedDeltaTime);


            //CameraHandle.transform.Rotate(Vector3.right, pi.jup * (-verticalSpeed) * Time.deltaTime);
            //tempEulerx = CameraHandle.transform.eulerAngles.x;
            //控制镜头上下方向角度限制。
            tempEulerx -= pi.jup * verticalSpeed*Time.fixedDeltaTime;
            tempEulerx = Mathf.Clamp(tempEulerx, -60, 60);

            CameraHandle.transform.localEulerAngles = new Vector3
            (
                tempEulerx, 0, 0
            );
            model.transform.eulerAngles = tempModelEuler;


        }
        else
        //lock target == true 锁死视角
        {

            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            PlayerHandle.transform.forward = tempForward;

            //锁定后,看向目标"中下";
            CameraHandle.transform.LookAt(lockTarget.obj.transform);
        }

        if (!isAI)
        {
            //camera 延迟追踪
            camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 0.1f);
            //camera.transform.eulerAngles = transform.eulerAngles;
            //pi.cmrfov = Mathf.Clamp(pi.cmrfov, -1, 1);
            float fovoffset = Camera.main.fieldOfView;
            fovoffset += pi.cmrfov;
            fovoffset = Mathf.Clamp(fovoffset, 30, 70);
            Camera.main.fieldOfView = fovoffset;
            camera.transform.LookAt(CameraHandle.transform);
        }


    }
    // Update is called once per frame
    void Update()
    {
        if (!isAI)
        {
            //Hide mouse
            if (bag.activeInHierarchy == true || EscPanel.activeInHierarchy == true)
            {
                Cursor.lockState = CursorLockMode.None;
                pi.jup = 0;
                pi.jright = 0;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (lockTarget != null)
        {
            if (!isAI)
            {
                //WorldToScreenPoint  世界坐标转成屏幕坐标 , 屏幕坐标原点在左下角;
                lockDot.rectTransform.position =  Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));
            }


            if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f)
            {
                //                lockTarget = null;
                //                lockDot.enabled = false;
                //                lockState = false;

                LockProcessA(null, false, false, isAI);
            }

        }
    }

    private void LockProcessA(LockTarget _lockTarget, bool _lockDotEnable, bool _lockState, bool _isAI)
    {
        lockTarget = _lockTarget;
        lockState = _lockState;

        if (!_isAI)
        {
            lockDot.enabled = _lockDotEnable;
        }
    }


    //锁定目标
    public void LockUnlock()
    {
        //        if (lockTarget == null)
        //        {


        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask(isAI ? "Player" : "Enemy"));

        if (cols.Length == 0)
        {
            //                lockTarget = null;
            //                lockDot.enabled = false;
            //                lockState = false;
            LockProcessA(null, false, false, isAI);
        }
        else
        {
            foreach (var col in cols)
            {
                if (lockTarget != null && lockTarget.obj == col.gameObject)
                {
                    //                        lockTarget = null;
                    //                        lockDot.enabled = false;
                    //                        lockState = false;
                    LockProcessA(null, false, false, isAI);
                    break;

                }

                //                    lockTarget  = new LockTarget( col.gameObject , col.bounds.extents.y);
                //                    lockDot.enabled = true;
                //                    lockState = true;
                LockProcessA(new LockTarget(col.gameObject, col.bounds.extents.y), true, true, isAI);
                break;

            }
        }

        //}
        //        else
        //        {
        //            lockTarget = null;
        //        }
    }   
    private class LockTarget
    {
        public GameObject obj;
        public ActorManager am;
        public float halfHeight;

        public LockTarget(GameObject _obj,float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
            am = _obj.GetComponent<ActorManager>();
        }
    }
}
