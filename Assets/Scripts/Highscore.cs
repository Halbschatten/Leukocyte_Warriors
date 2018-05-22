using System.Collections;
using System.Collections.Generic;

public class Highscore
{
    string player1Name, player2Name;
    int score;

    public Highscore()
    {
        player1Name = "";
        player2Name = "";
        score = 0;
    }
    public string GetPlayer1Name()
    {
        return player1Name;
    }
    public string GetPlayer2Name()
    {
        return player2Name;
    }
    public int GetScore()
    {
        return score;
    }
    public Highscore(string player1Name, string player2Name, int score)
    {
        this.player1Name = player1Name;
        this.player2Name = player2Name;
        this.score = score;
    }
}
