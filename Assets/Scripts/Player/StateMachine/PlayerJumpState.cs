using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateManager currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
    }

    public override void EnterState()
    {
        player.Anim.SetInteger("State", (int)StateEnum.EPlayerState.jump);
        player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.JumpForce);
    }
    
    public override void UpdateState()
    {
        player.CanMove();
        CheckSwitchState();
        PlayerStateManager.UpdateObjectDirX(player);
    }

    public override void CheckSwitchState()
    {
        if (Input.GetKeyDown(player.DashKey) && player.CanDash)
        {
            SwitchState(factory.Dash());
        }
        else if (Input.GetKeyDown(player.ThrowWeaponKey) && player.CanThrowWeapon)
        {
            SwitchState(factory.ThrowWeapon());
        }
        else if (player.IsSeeingGround)
        {
            Debug.Log(player.IsSeeingGround);
            SwitchState(factory.WallSlide());
        }
        else
        {
            if (player.Rb.velocity.y < .1f)
            {
                SwitchState(factory.Fall());
            }
            else if (Input.GetButtonDown("Jump"))
            {
                player.IsDoubleJump = true;
                SwitchState(factory.DoubleJump());
            }
        }
    }

    public override void ExitState()
    {

    }
}
