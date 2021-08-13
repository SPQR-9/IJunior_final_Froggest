using UnityEngine;

public class PlayerDisable : StateMachineBehaviour
{ 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player player = animator.gameObject.GetComponent<Player>();
        player.Disable();
    }
}
