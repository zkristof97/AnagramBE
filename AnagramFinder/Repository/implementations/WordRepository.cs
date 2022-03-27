using Repository.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Repository.Implementations
{
    public class WordRepository : IWordRepository
    {
        private readonly List<string> _loadedWords;

        public WordRepository()
        {
            var currentDir = Directory.GetCurrentDirectory();
            string path = $"{currentDir}\\szavak.txt";

            if (File.Exists(path))
            {
                var wordsWithLineBreak = File.ReadAllText(path);
                _loadedWords = new List<string>(wordsWithLineBreak.Split('\n'));
            } else
            {
                _loadedWords = new List<string>();
            }
        }

        public IEnumerable<string> GetAll()
        {
            return _loadedWords;
        }
    }
}
