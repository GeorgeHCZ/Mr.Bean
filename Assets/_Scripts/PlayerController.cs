using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveForce = 365f;//运动力大小
    public float maxSpeed = 5f;//角色最大速度
    public float jumpForce = 1000f;//角色跳跃时力的大小

    private Transform groundCheck;//检测角色是否在地面上
    private bool grounded = false;//默认为false
    private Animator anim;//角色对象上的animator组件

    [HideInInspector]//在inspector中隐藏该属性
    public bool facingRight = true;//角色是否朝右
    [HideInInspector]
    public bool jump = false;//角色是否在跳跃
	// Use this for initialization
	void Start () {
        groundCheck = transform.Find("groundCheck");//获取transform组件所属游戏对象上的名称为“groundCheck”的子对象
        anim = GetComponent<Animator>();//获取当前脚本所绑定的游戏对象上animator类型的组件
	}
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
	}

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(h));//求绝对值
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (h * rb.velocity.x < maxSpeed)
            rb.AddForce(Vector2.right * h * moveForce);
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        //Mathf.Sign(参数） 获取指定参数的符号值；如果为正，返回1；如果为负，返回-1；
        //当用户按下方向键左即h>0并且小豆人方向向右时，
        if (h > 0 && !facingRight)//当用户按下方向键右键即h>0并且小豆人朝左时
            Flip();//自定义函数。用于实现小豆人反转
        else if (h < 0 && facingRight)
            Flip();
        if(jump)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
