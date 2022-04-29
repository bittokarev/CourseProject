using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Course.Tests")]
namespace Course.Tools
{
    public class Cipher
    {
        private readonly int Power;
        private readonly Dictionary<char, int> AlphabetCharToInt = new Dictionary<char, int>();
        private readonly Dictionary<int, char> AlphabetIntToChar = new Dictionary<int, char>();

        public Cipher(char[] alphabet)
        {
            Debug.Assert(alphabet.Length > 0);

            Power = alphabet.Length;
            int i = 0;
            foreach (var c in alphabet)
            {
                AlphabetCharToInt[c] = i;
                AlphabetIntToChar[i] = c;
                i++;
            }
        }

        public string Encrypt(string input, string keyword)
        {
            Debug.Assert(input != null && keyword != null);

            input = input.ToLower();
            keyword = keyword.ToLower();

            int len = keyword.Length;
            var result = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                if (!AlphabetCharToInt.ContainsKey(input[i]))
                {
                    result.Append(input[i]);
                    input = input.Remove(i, 1);
                    --i;
                    continue;
                }

                result.Append
                (
                   AlphabetIntToChar
                   [
                       (AlphabetCharToInt[input[i]] + AlphabetCharToInt[keyword[i % len]]) % Power
                   ]
                );
            }

            return result.ToString();
        }

        public string Decrypt(string input, string keyword)
        {
            Debug.Assert(input != null && keyword != null);

            input = input.ToLower();
            keyword = keyword.ToLower();

            int len = keyword.Length;
            var result = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                if (!AlphabetCharToInt.ContainsKey(input[i]))
                {
                    result.Append(input[i]);
                    input = input.Remove(i, 1);
                    --i;
                    continue;
                }

                result.Append
                (
                    AlphabetIntToChar
                    [
                        (AlphabetCharToInt[input[i]] - AlphabetCharToInt[keyword[i % len]] + Power) % Power
                    ]
                );
            }

            return result.ToString();
        }
    }
}