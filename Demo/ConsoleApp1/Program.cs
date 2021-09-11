using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        private static int Flag = 0;

        static void Main(string[] args)
        {
            #region 算法测试
            //Console.WriteLine("请输入任意数量数字，以逗号分隔（回车结束）:");
            //var inputIntArray = Console.ReadLine().Trim().Split(',').Select(item=>Convert.ToInt32(item)).ToArray();
            //var result = ReturnIntArray(inputIntArray);

            //Console.WriteLine(result.Any()? string.Join(",", result):"没有匹配的数");
            //Console.WriteLine($"时间复杂度:O({Flag})");
            //Console.ReadLine();
            #endregion

            //var linkedList = new LinkedList<int>();
            //linkedList.AddLast(1);
            //linkedList.AddLast(2);
            //foreach (var item in linkedList)
            //{
            //    if (item == 1)
            //    {
            //        linkedList.Remove(item);
            //    }
            //}
            //var list = new List<int> { 1, 2 };
            //while (list.Count > 0)
            //{
            //    if (list.First() == 1)
            //    {
            //        list.RemoveAt(0);
            //    }
            //    break;
            //}
            //Console.WriteLine($"执行完成:{list[0]}");
            //Console.ReadLine();

            //double? g = null;
            //Console.WriteLine(g.Value);
            //Console.ReadLine();

            var intArray = new int[] { 3, 2, 1, 0, 2, 3 };
            var loopCount=0;
            var result = GetAnyRepeatNumber(0,ref loopCount,intArray);
            Console.WriteLine(result==null?"未找到重复数组":$"重复数字（包含但不限于）:{result},循环次数:{loopCount}");
            Console.ReadLine();
        }

        /// <summary>
        /// 算法-一位数组
        //题目:找出一维数组中重复的任一数字。一维数组长度为n，包含0~n-1的数字
        //思路1：将每个数和其他数进行比较。时间复杂度O(n*n),空间复杂度O(1)
        //思路2：利用hash表进行判断，不存在则加进hash表，存在则返回存在的数。时间复杂度O(n),空间复杂度O(n)。因为这里除了数组本身之外，还用到了一个hash表，空间复杂度为O(n)
        //思路3：利用数组本身的特点，构思算法思路，不用将当前数与每个数进行比较，每次循环确定一个数。时间复杂度O(2n)=O(n)+O(n-1)=O(n),空间复杂度O(1)
        /// </summary>
        /// <param name="loopIndex"></param>
        /// <param name="loopCount"></param>
        /// <param name="intArray"></param>
        /// <returns></returns>
        public static int? GetAnyRepeatNumber(int loopIndex, ref int loopCount, int[] intArray)
        {
            #region 校验输入数组
            if (intArray == null || !intArray.Any())
                return null;
            if (intArray.Any(item => item < 0 || item >= intArray.Length))
                return null;
            #endregion

            for (; loopIndex < intArray.Length; loopIndex++)
            {
                loopCount++;

                var loopItem = intArray[loopIndex];

                if (loopItem == loopIndex)//如果当前数字等于其索引，则继续执行下一循环
                    continue;

                if (loopItem == intArray[loopItem])//如果相等，则此数字为重复数字，直接返回
                    return loopItem;

                //如果都不满足，则做交换，将此数字换到应该在的位置
                intArray[loopIndex] = intArray[loopItem];//先将另外一个数换过来
                intArray[loopItem] = loopItem;//再将当前数放对的位置
                return GetAnyRepeatNumber(loopIndex, ref loopCount, intArray);//递归调用，传入索引点
            }

            return null;
        }

        /// <summary>
        /// 找出大于前面所有数，小于后面所有数的数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> ReturnIntArray(int[] input)
        {
            var result = new List<int>();

            for (int i = 1; i < input.Length-1; i++)
            {
                //var beforeMax = input.Take(i).Max();
                //var afterMin = input.Skip(i+1).Take(input.Length-(i+1)).Min();

                Flag += 1;
                var beforeMax = BeforeMax(input,i+1);
                var afterMin = AfterMin(input,i+1);
                if (input[i] >= beforeMax && input[i] < afterMin)
                    result.Add(input[i]);
            }

            return result;
        }

        private static int BeforeMax(int[] input,int beforeNum)
        {
            var max = input[0];

            for (int i = 0; i < beforeNum; i++)
            {
                Flag += 1;

                if (input[i] > max)
                    max = input[i];
            }

            return max;
        }

        private static int AfterMin(int[] input,int afterNumber)
        {
            var min = input[afterNumber];

            for (int i = afterNumber; i < input.Length; i++)
            {
                Flag += 1;

                if (input[i] < min)
                    min = input[i];
            }

            return min;
        }
    }

    

    public enum Enum1
    {
        Source1=1,
        Source2=2,
        Source3=3
    }

    public class Test1
    {
        public string Name { get; set; }

        public Test2 Test2 { get; set; }

        /// <summary>
        /// 浅拷贝（引用类型，复制引用）
        /// </summary>
        /// <returns></returns>
        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// 深拷贝（通过反射，拿到所有属性的值）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T DeepCopy<T>(T obj)
        {
            //值类型或者string类型直接返回
            if (obj is string || obj.GetType().IsValueType)
                return obj;

            //引用类型
            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            foreach (var field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopy(field.GetValue(obj)));//递归调用
                }
                catch
                {
                }
            }

            return (T)retval;
        }
    }

    public class Test2
    {
        public string Name { get; set; }
    }
}
