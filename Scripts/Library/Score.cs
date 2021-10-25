using System.Diagnostics;

public class Score
{
    public static int totalNotes = 1;
    public static float P()
    {
        return 900000f / totalNotes;
    }
    public static float C()
    {
        return 100000f / (totalNotes * (totalNotes - 1) / 2);
    }
    public static float B(int combo)
    {
        return (combo) * C();
    }
    public static float GetGatheredScore(int currentCombo, NoteState noteState)
    {
        float scoreGathered = 0;
        switch (noteState)
        {
            case NoteState.Perfect:
                scoreGathered = P() + B(currentCombo);
                break;
            case NoteState.Good:
                scoreGathered = 0.7f * P() + B(currentCombo);
                break;
            case NoteState.Bad:
                scoreGathered = 0.3f * P();
                break;
            case NoteState.Miss:
                scoreGathered = 0;
                break;
        }
        return scoreGathered;
    }
}