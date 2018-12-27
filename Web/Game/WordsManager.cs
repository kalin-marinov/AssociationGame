

using System;
using System.Collections.Generic;

public class WordsManager
{
    private IReadOnlyCollection<string> allWords;
    private HashSet<string> roundWords;
    private Random rng;

    public int RemainingWords => roundWords.Count;

    public WordsManager(IReadOnlyCollection<string> words)
    {
        this.allWords = words;
        this.rng = new Random();
        this.ResetRoundWords();
    }

    public void ResetRoundWords()
    {
        this.roundWords = new HashSet<string>(allWords);
    }

    public string GetRandomWord()
    {
        return this.roundWords.GetRandomElement(rng);
    }

    public void RemoveFromRound(string word)
    {
        this.roundWords.Remove(word);
    }
}