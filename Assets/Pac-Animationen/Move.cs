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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dest = Vector2.right * size.x*velm;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dest = Vector2.left * size.x * velm;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dest = Vector2.up * size.y * velm;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dest = Vector2.down * size.y * velm;
        }
        if(!invalid(dest))transform.Translate(dest);
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dest.x);
        GetComponent<Animator>().SetFloat("DirY", dest.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            field.NewField();
        }
    }

    bool invalid(Vector2 dir)
    {
        int signx = System.Math.Sign(dir.x);
        int signy = System.Math.Sign(dir.y);
        //print(dir);
        Vector2 s = new Vector2(signx * 13, signy * 13);
        Vector2 lr = Vector2.zero;
        Vector2 rr = Vector2.zero;
        if (signx == 1)
        {
            lr = new Vector2(12.19238815543f, 12.19238815543f);
            rr = new Vector2(12.19238815543f, -12.19238815543f);
        }
        else if (signx == -1)
        {
            lr = new Vector2(-12.19238815543f, 12.19238815543f);
            rr = new Vector2(-12.19238815543f, -12.19238815543f);
        }
        if (signy == 1)
        {
            lr = new Vector2(12.19238815543f, 12.19238815543f);
            rr = new Vector2(-12.19238815543f, 12.19238815543f);
        }
        else if (signy == -1)
        {
            lr = new Vector2(12.19238815543f, -12.19238815543f);
            rr = new Vector2(-12.19238815543f, -12.19238815543f);
        }
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + s, pos);
        RaycastHit2D hit1 = Physics2D.Linecast(pos + lr, pos);
        RaycastHit2D hit2 = Physics2D.Linecast(pos + rr, pos);
        Debug.DrawRay(transform.position,s);
        Debug.DrawRay(transform.position, lr);
        Debug.DrawRay(transform.position, rr);
        //print(hit.collider.name!="Pac-Man" || hit1.collider.name != "Pac-Man" || hit2.collider.name != "Pac-Man");
        return hit.collider.name != "Pac-Man" || hit1.collider.name != "Pac-Man" || hit2.collider.name != "Pac-Man";
    }
}
