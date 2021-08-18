using UnityEngine;

public class PlayerDisabler : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player player = animator.gameObject.GetComponent<Player>();
        player.Disable();
    }
}
