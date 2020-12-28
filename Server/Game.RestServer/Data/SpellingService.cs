using System.IO;
using WeCantSpell.Hunspell;

namespace Game.RestServer.Data
{
    public class SpellingService
    {
        private string currentDir = Directory.GetCurrentDirectory();

        bool IsValidWord(string word, string language)
        {
            var enDictFile = Path.Combine(currentDir, "dictionaries", $"{language}.dic");
            var dictionary = WordList.CreateFromFiles(enDictFile);
            return dictionary.Check(word);
        }
    }

}