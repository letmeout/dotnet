using System;
using System.Linq;
using System.Reflection;

namespace SampleConsole
{
    public static class Math
    {
         // Returns true if str1 is smaller than str2. 
        public static bool isSmaller(this string str1, string str2)
        {
            // Calculate lengths of both string 
            int n1 = str1.Length, n2 = str2.Length;
            if (n1 < n2)
                return true;
            if (n2 < n1)
                return false;

            for (int i = 0; i < n1; i++)
                if (str1[i] < str2[i])
                    return true;
                else if (str1[i] > str2[i])
                    return false;

            return false;
        }

        // Function for find difference of larger numbers 
        public static string findDiff(this string str1, string str2)
        {
            // Before proceeding further, make sure str1 
            // is not smaller 
            if (isSmaller(str1, str2))
            {
                string t = str1;
                str1 = str2;
                str2 = t;
            }

            // Take an empty string for storing result 
            string str = "";

            // Calculate length of both string 
            int n1 = str1.Length, n2 = str2.Length;

            // Reverse both of strings 
            char[] ch1 = str1.ToCharArray();
            Array.Reverse(ch1);
            str1 = new string(ch1);
            char[] ch2 = str2.ToCharArray();
            Array.Reverse(ch2);
            str2 = new string(ch2);

            int carry = 0;

            // Run loop till small string length 
            // and subtract digit of str1 to str2 
            for (int i = 0; i < n2; i++)
            {
                // Do school mathematics, compute difference of 
                // current digits 
                int sub = ((int)(str1[i] - '0') -
                        (int)(str2[i] - '0') - carry);

                // If subtraction is less then zero 
                // we add then we add 10 into sub and 
                // take carry as 1 for calculating next step 
                if (sub < 0)
                {
                    sub = sub + 10;
                    carry = 1;
                }
                else
                    carry = 0;

                str += (char)(sub + '0');
            }

            // subtract remaining digits of larger number 
            for (int i = n2; i < n1; i++)
            {
                int sub = ((int)(str1[i] - '0') - carry);

                // if the sub value is -ve, then make it positive 
                if (sub < 0)
                {
                    sub = sub + 10;
                    carry = 1;
                }
                else
                    carry = 0;

                str += (char)(sub + '0');
            }

            // reverse resultant string 
            char[] ch3 = str.ToCharArray();
            Array.Reverse(ch3);
            string result = new string(ch3);
            return result.TrimStart('0');
        }

        // Function for finding sum of larger numbers  
        public static string findSum(this string str1, string str2)
        {
            // Before proceeding further, make sure length  
            // of str2 is larger.  
            if (str1.Length > str2.Length)
            {
                string t = str1;
                str1 = str2;
                str2 = t;
            }

            // Take an empty string for storing result  
            string str = "";

            // Calculate length of both string  
            int n1 = str1.Length, n2 = str2.Length;

            // Reverse both of strings 
            char[] ch = str1.ToCharArray();
            Array.Reverse(ch);
            str1 = new string(ch);
            char[] ch1 = str2.ToCharArray();
            Array.Reverse(ch1);
            str2 = new string(ch1);

            int carry = 0;
            for (int i = 0; i < n1; i++)
            {
                // Do school mathematics, compute sum of  
                // current digits and carry  
                int sum = ((int)(str1[i] - '0') +
                        (int)(str2[i] - '0') + carry);
                str += (char)(sum % 10 + '0');

                // Calculate carry for next step  
                carry = sum / 10;
            }

            // Add remaining digits of larger number  
            for (int i = n1; i < n2; i++)
            {
                int sum = ((int)(str2[i] - '0') + carry);
                str += (char)(sum % 10 + '0');
                carry = sum / 10;
            }

            // Add remaining carry  
            if (carry > 0)
                str += (char)(carry + '0');

            // reverse resultant string 
            char[] ch2 = str.ToCharArray();
            Array.Reverse(ch2);
            str = new string(ch2);

            return str;
        }

        // Multiplies str1 and str2, and prints result.  
        public static string multiply(this string num1, string num2)
        {
            int len1 = num1.Length;
            int len2 = num2.Length;
            if (len1 == 0 || len2 == 0)
                return "0";

            // will keep the result number in vector  
            // in reverse order  
            int[] result = new int[len1 + len2];

            // Below two indexes are used to  
            // find positions in result.  
            int i_n1 = 0;
            int i_n2 = 0;
            int i;

            // Go from right to left in num1  
            for (i = len1 - 1; i >= 0; i--)
            {
                int carry = 0;
                int n1 = num1[i] - '0';

                // To shift position to left after every  
                // multipliccharAtion of a digit in num2  
                i_n2 = 0;

                // Go from right to left in num2              
                for (int j = len2 - 1; j >= 0; j--)
                {
                    // Take current digit of second number  
                    int n2 = num2[j] - '0';

                    // Multiply with current digit of first number  
                    // and add result to previously stored result  
                    // charAt current position.  
                    int sum = n1 * n2 + result[i_n1 + i_n2] + carry;

                    // Carry for next itercharAtion  
                    carry = sum / 10;

                    // Store result  
                    result[i_n1 + i_n2] = sum % 10;

                    i_n2++;
                }

                // store carry in next cell  
                if (carry > 0)
                    result[i_n1 + i_n2] += carry;

                // To shift position to left after every  
                // multipliccharAtion of a digit in num1.  
                i_n1++;
            }

            // ignore '0's from the right  
            i = result.Length - 1;
            while (i >= 0 && result[i] == 0)
                i--;

            // If all were '0's - means either both  
            // or one of num1 or num2 were '0'  
            if (i == -1)
                return "0";

            // genercharAte the result String  
            String s = "";

            while (i >= 0)
                s += (result[i--]);

            return s;
        }

        public static string longDivision(this string number, int divisor)
        {
            // As result can be very large store it in string 
            string ans = "";

            // Find prefix of number that is larger 
            // than divisor. 
            int idx = 0;
            int temp = (int)(number[idx] - '0');
            while (temp < divisor)
            {
                temp = temp * 10 + (int)(number[idx + 1] - '0');
                idx++;
            }
            ++idx;

            // Repeatedly divide divisor with temp. After 
            // every division, update temp to include one 
            // more digit. 
            while (number.Length > idx)
            {
                // Store result in answer i.e. temp / divisor 
                ans += (char)(temp / divisor + '0');

                // Take next digit of number 
                temp = (temp % divisor) * 10 + (int)(number[idx] - '0');
                idx++;
            }
            ans += (char)(temp / divisor + '0');

            // If divisor is greater than number 
            if (ans.Length == 0)
                return "0";

            // else return ans 
            return ans;
        }

        public static string Factorial(string number)
        {
            if (number.isSmaller("1"))
            {
                return "0";
            }
            else if (number.Equals("1"))
            {
                return "1";
            }
            else
            {
                return number.multiply(Factorial(number.findDiff("1")));
            }
        }

    }
}