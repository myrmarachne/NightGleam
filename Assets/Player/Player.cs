public class Player {

    public int Lifes;
	public JumpType Jump;

    public Player() {
        Reset();
    }

    public void Reset() {
        Lifes = 3;
		this.Jump = JumpType.NoJump;
    }
}
