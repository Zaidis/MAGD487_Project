using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Bow : MonoBehaviour
{
    public Vector2 input;
    Animator anim;
    bool coolDown = false;
    float timer = 0;
    bool controller = false;
    [SerializeField] float shotCoolDown = 1;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform firePoint;
    [SerializeField] Camera cam;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (coolDown)
        {
            timer += Time.deltaTime;
            if(timer >= shotCoolDown)
            {
                timer = 0;
                coolDown = false;
            }
        }
        if(input.magnitude != 0)
        {
            if (controller)
            {
                Look(input);
            }
            else
            {
                Vector3 target = cam.ScreenToWorldPoint(input) - this.transform.position;
                Look(target);
            }
        }
    }

    void Look(Vector2 input)
    {
        var angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public void Fire(InputAction.CallbackContext callbackContext)
    {
        if (this.gameObject.activeInHierarchy && !coolDown)
        {
            anim.SetTrigger("Fire");
            coolDown = true;
            GameObject g = Instantiate(arrow, firePoint.position, Quaternion.identity);
            g.transform.rotation = this.transform.rotation;
        }
    }
    public void LookAtMouse(InputAction.CallbackContext callbackContext)
    {
         controller = false;
         input = callbackContext.ReadValue<Vector2>();
    }
    public void LookAtController(InputAction.CallbackContext callbackContext)
    {
        controller = true;
        input = callbackContext.ReadValue<Vector2>();
    }
}
