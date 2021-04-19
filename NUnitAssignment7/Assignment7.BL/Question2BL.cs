using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.BL
{
    public class Question2BL
    {
        /// <summary>
        /// Business logic for asyc and divide by zero exception
        /// </summary>
        /// <param name="no1"></param>
        /// <param name="no2"></param>
        /// <returns></returns>
        public async Task<double> Division(int no1, int no2)
        {
            if (no2 == 0)
                throw new DivideByZeroException();
            else
            {
                await Task.Delay(1000);
                return no1 / no2;
            }
        }

        /// <summary>
        /// Business logic for index out of range exception
        /// </summary>
        /// <param name="intArray"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetValuebyIndex(int[] intArray, int index)
        {
            if (index <0 || index > intArray.Length - 1)
                throw new IndexOutOfRangeException();
            else
                return intArray[index];
        }

        /// <summary>
        /// Business logic for asyn method to find area of circle
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        public async Task<double> AreaofCircle(int radius)
        {
            double area;
            if(radius>=0)
            {
                await Task.Delay(1000);
                area = 3.14 * radius * radius;
                return area;
            }
            return 0;
        }

        /// <summary>
        /// Business logic for asyn method to find area of square
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public async Task<double> AreaofSquare(int length)
        {
            double area;
            if (length >= 0)
            {
                await Task.Delay(1000);
                area = length*length;
                return area;
            }
            return 0;
        }

        /// <summary>
        /// Business logic for asyn method to find perimeter of square
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public async Task<double> PerimeterofSquare(int length)
        {
            double perimeter;
            if (length >= 0)
            {
                await Task.Delay(1000);
                perimeter = 4 * length;
                return perimeter;
            }
            return 0;
        }


    }
}
