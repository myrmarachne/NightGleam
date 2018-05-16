public class Game {
    private static Game game = null;

    public GameState State;
	public int Level;
    private Player player;

    public Player Player {
        get {
            return player;
        }
    }

    public static Game GetInstance() {
        if (game == null) {
            game = new Game();
        }
        return game;
    }

    private Game() {
        Reset();
    }

    public void Reset() {
        State = GameState.Playing;
        player = new Player();
		Level = 1;
    }
}
