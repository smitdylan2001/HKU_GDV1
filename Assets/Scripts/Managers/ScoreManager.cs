public static class ScoreManager
{
	public static int _score = 0;

	public static void UpdateScore(int scoreToAdd)
	{
		_score += scoreToAdd;
		var val = UIManager._uiTextElements["scoreText"];
		UIManager.UpdateUITextElement(val, _score.ToString());
	}
}
