using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment7.BL
{
    public class Question1BL
    {
        /// <summary>
        /// Business Logic for "if-else"
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public string FindAgeGroup(int age)
        {
            if (age >= 0)
            {
                if (age <= 18)
                    return "Child";
                else if (age <= 60)
                    return "Adult";
                else
                    return "Senior Citizen";
            }
            return "Invalid age!!!";
        }

        /// <summary>
        /// Business logic for "switch-case"
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string FindDayofWeek(int day)
        {
            switch (day)
            {
                case 1:
                    return "Sunday";
                case 2:
                    return "Monday";
                case 3:
                    return "Tuesday";
                case 4:
                    return "Wednesday";
                case 5:
                    return "Thursday";
                case 6:
                    return "Friday";
                case 7:
                    return "Saturday";
                default:
                    return "Invalid day!!!";
            }
        }

        /// <summary>
        /// Business logic for "for-loop"
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public long FindFactorial(int num)
        {
            if (num >= 0)
            {
                long fact = 1;
                if (num > 0)
                {
                    for (int i = 1; i <= num; i++)
                    {
                        fact *= i;
                    }
                }
                return fact;
            }
            else
                return 0;
        }

        /// <summary>
        /// Business logic for "foreach-loop"
        /// </summary>
        /// <param name="numberList"></param>
        /// <returns></returns>
        public long FindSumofEven(int[] numberList)
        {
            long sum = 0;
            if (numberList.Length > 0)
            {
                foreach (int n in numberList)
                {
                    if (n % 2 == 0)
                        sum += n;
                }
            }
            return sum;
        }

        /// <summary>
        /// Business logic for "while-loop"
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool CheckPalindrome(int num)
        {
            int x = num;
            int p = 0,d;
            while(num>0)
            {
                d = num % 10;
                p = p * 10 + d;
                num /= 10;
            }
            if (p == x)
                return true;
            else
                return false;
        }
    }
}
