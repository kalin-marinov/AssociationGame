

using System;
using System.Collections.Generic;

public class WordsManager
{
    private IReadOnlyCollection<string> allWords;
    private Random rng;

    private List<string> roundWords;
    private Stack<string> removedFromFround;

    public int RemainingWords => roundWords.Count;

    public WordsManager(IReadOnlyCollection<string> words)
    {
        this.allWords = words;
        this.rng = new Random();
        this.ResetRoundWords();
    }

    public void ResetRoundWords()
    {
        this.roundWords = new List<string>(allWords);
        this.removedFromFround = new Stack<string>(allWords);
    }

    public string GetRandomWord()
    {
        return this.roundWords.GetRandomListItem(rng);
    }

    public void RemoveFromRound(string word)
    {
        this.roundWords.Remove(word);
        this.removedFromFround.Push(word);
    }

    public string RevertRemove()
    {
        var word = this.removedFromFround.Pop();
        this.roundWords.Add(word);
        return word;
    }
}