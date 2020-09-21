public static class ScoreManager
{
	public static int SCORE = 0;

	public static void UpdateScore(int scoreToAdd)
	{
		SCORE += scoreToAdd;
		var val = UIManager.UI_TEXT_ELEMENTS["scoreText"];
		UIManager.UpdateUITextElement(val, SCORE.ToString());
	}
}
