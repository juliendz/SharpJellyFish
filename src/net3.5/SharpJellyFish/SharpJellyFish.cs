using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpJellyFish
{
    public class SharpJellyFish
    {
        private string Normalize(string s)
        {
            return s.Normalize(NormalizationForm.FormKD);
        }

        public string SoundEx(string input)
        {

            List<string[]> _replacements = new List<string[]>()
            {
                new string [2] {"BFPV", "1"},
                new string [2] {"CGJKQSXZ", "2"},
                new string [2] {"DT", "3"},
                new string [2] {"L", "4"},
                new string [2] {"MN", "5"},
                new string [2] {"R", "6"},
            };


            if (string.IsNullOrEmpty(input))
            {
                return "";
            }

            var s = Normalize(input);
            s = s.ToUpper();

            var result = new List<char>();
            result.Add(s[0]);
            var count = 1;

            var last = "";
            // find would-be replacement for first character
            foreach (var replacementStr in _replacements)
            {
                var lset = replacementStr[0];
                var sub = replacementStr[1];
                if (lset.Contains(s[0]))
                {
                    last = sub;
                    break;
                }
            }

            foreach (var letter in s.ToList().GetRange(1, s.Count() - 1))
            {
                var isLetterExistInReplacments = false;
                foreach (var replacementStr in _replacements)
                {
                    var lset = replacementStr[0];
                    var sub = replacementStr[1];
                    if (lset.Contains(letter))
                    {
                        if (sub != last)
                        {
                            result.Add(sub.ToCharArray()[0]);
                            count++;
                        }
                        last = sub;
                        isLetterExistInReplacments = true;
                        break;
                    }
                }

                if (!isLetterExistInReplacments)
                {
                    if (letter != 'H' && letter != 'W')
                    {
                        last = "";
                    }
                }

                if (count == 4)
                    break;
            }

            var finalResult = new string(result.ToArray());
            //Pad the string with extra zeros to make it max of 4 letters
            for (var i = 0; i < (4 - count); i++)
            {
                finalResult += "0";
            }

            return finalResult;
        }
    }
}
