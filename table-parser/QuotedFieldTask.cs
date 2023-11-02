using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]


    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]

        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }


    }

    class QuotedFieldTask
    {
        private static bool NewCharIsEcran(StringBuilder str, char currentChar, bool flag)
        {
            if (flag)
            {
                str.Append(currentChar);
                return false;
            }
            return true;
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            var str = new StringBuilder();
            var flag = false;
            var currentIndex = startIndex;
            var charFirst = line[currentIndex++];

            while (currentIndex < line.Length)
            {
                var currentChar = line[currentIndex++];

                if (currentChar == '\\')
                {
                    if (flag)
                    {
                        str.Append(currentChar);
                        flag = false;
                    }
                    else
                        flag = true;
                }
                else if (currentChar == charFirst)
                {
                    if (!flag)
                    {
                        break;
                    }

                    if (flag)
                    {
                        str.Append(currentChar);
                        flag = false;
                    }
                    else
                        flag = true;
                }
                else str.Append(currentChar);
            }
            return new Token(str.ToString(), startIndex, currentIndex - startIndex);
        }
    }
}
