using Logic.interfaces;
using Repository.interfaces;
using System;
using System.Collections.Generic;

namespace Logic.implementations
{
    public class AnagramFinderLogic : IAnagramFinderLogic
    {
        private readonly IWordRepository _wordRepository;

        public AnagramFinderLogic(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public List<string> GetAnagramsByWord(string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new NoSearchTermException("Word to search for is not provided.");
            }

            if(searchTerm.Length > 5)
            {
                throw new SearchTermTooLongException("Sought word is too long.");
            }

            var words = (List<string>)_wordRepository.GetAll();

            var anagrams = words.FindAll(word =>
            {
                var isLengthSame = word.Length == searchTerm.Length;

                if(!isLengthSame || !IsAnagram(word, searchTerm))
                {
                    return false;
                }

                return true;
            });

            if(anagrams.Count == 0)
            {
                throw new NoMatchException("No match for the search criteria.");
            }

            return anagrams;
        }

        private bool IsAnagram(string word, string searchTerm)
        {
            return CreateSortedWord(word) == CreateSortedWord(searchTerm);
        }

        private string CreateSortedWord(string word)
        {
            char[] chars = word.ToCharArray();
            Array.Sort(chars);

            return (new string(chars));
        }
    }
}

public class NoMatchException : Exception
{
    public NoMatchException(string message): base(message)
    {

    }
}

public class NoSearchTermException : Exception
{
    public NoSearchTermException(string message) : base(message)
    {

    }
}

public class SearchTermTooLongException : Exception
{
    public SearchTermTooLongException(string message) : base(message)
    {

    }
}