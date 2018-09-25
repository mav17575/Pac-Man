using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {
    //Muss nicht gesetzt werden. Richtet sich nach Tilegroesse.
    Vector2 dest = Vector2.zero;
    public float velm;
    public Field field;
    Animator anim;
    SpriteRenderer sr;
    Vector3 size;
    Rigidbody2D rb;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(LateStart(0.001f));
        rb = GetComponent<Rigidbody2D>();
      //  dest = transform.position;
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        sr = GetComponent<SpriteRenderer>();
        size = sr.bounds.size;
    }

    void Update()
    {
        //Vector2 p = Vector2.MoveTowards(transform.position, dest, 1);
        //rb.MovePosition(p);
        // rb.MovePosition((Vector2)transform.position+(Vector2.right * size.x*velm));
        Vector3 nvel = Vector2.zero;
        bool set = false;
        anim.speed = 1;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            set = true;
            nvel = Vector2.right * size.x*velm;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            set = true;
            nvel = Vector2.left * size.x * velm;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            set = true;
            nvel = Vector2.up * size.y * velm;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            set = true;
            nvel = Vector2.down * size.y * velm;
        }

        if (set && valid(nvel))
        {
            dest = nvel;
            transform.Translate(dest);
        }
        else if (valid(dest))
        {
            transform.Translate(dest);
        }
        else
        {
            //Animation darf nicht bei geschlossenem Mund halten
            anim.speed = 0;
        }

        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dest.x);
        GetComponent<Animator>().SetFloat("DirY", dest.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            field.NewField();
        }
    }

    bool valid(Vector2 dir)
    {
        //Strahlt 2 Linien aus
        //Schaut, ob sich der Anfang oder das Ende von Pacman in Bloecken befinden.(mit etwas Versatz)
        float x = System.Math.Sign(dir.x);
        float y = System.Math.Sign(dir.y);
        Vector3 pos = transform.localPosition;
        Vector3 l = new Vector3(pos.x + ( (size.x / 2)+1.5f - (3f * Mathf.Abs(y))) *(x+y), pos.y +( (size.y / 2)+1.5f - (3f * Mathf.Abs(x))) *(x+y));
        Vector3 r = new Vector3(pos.x + ( (size.x / 2)+1.5f - (3f * Mathf.Abs(y))) * (x-y), pos.y - ( (size.y / 2)+1.5f - (3f * Mathf.Abs(x))) * (x-y));
        Debug.DrawLine(pos,l);
        Debug.DrawLine(pos, r);
        return field.IsValid(l) && field.IsValid(r);
    }
}
