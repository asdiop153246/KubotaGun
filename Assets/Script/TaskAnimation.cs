using UnityEngine;
using UnityEngine.Animations;
public class TaskAnimation : MonoBehaviour
{
    public bool _isOpen;
    public Animator _TaskAnim;
    public void PlayTaskanimation()
    {
        if (!_isOpen)
        {
            _TaskAnim.Play("Task");
            _isOpen = true;
        }
        else
        {
            _TaskAnim.Play("TaskCLosed");
            _isOpen = false;
        }
    }
}
