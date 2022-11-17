using Enums;
using UnityEngine;

namespace Controllers
{
    public class SoldierAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator animator;

        #endregion
        
        #endregion

        public void SetAnim(SoldierAnimTypes types)
        {
            animator.SetTrigger(types.ToString());
        }
        public void SetBoolAnim(SoldierAnimTypes types,bool value)
        {
            animator.SetBool(types.ToString(),value);
        }
    }
}