using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IUserInput : MonoBehaviour
{
    [Header("===== Output signals =====")]
    public float Dup;
    public float Dright;
    public float jup;
    public float jright;
    public float Dmag;
    public Vector3 Dvec;
    public float cmrfov;


    [Header("===== Others =====")]
    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;

    public bool inputEnabled = true;
    public bool run;
    public bool onRun = false;
    public bool defense;
    public bool attack;
    protected bool lastattack;
    public bool action;
    protected bool lastaction;
    public bool jump;
    protected bool lastjump;
    public bool lockon;
    public bool lastlockon;
    public bool rt;
    public bool lastrt;
    public bool lt;
    public bool lastlt;
    public bool roll;
    protected bool lastroll;
    public bool jab;
    protected bool lastjab;

    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }

    protected void UpdateDmagDvec(float _Dup, float _Dright)
    {

        Dmag = Mathf.Sqrt(_Dup * Dup) + (_Dright * _Dright);
        Dvec = _Dright * transform.right + _Dup * transform.forward;

    }
}
