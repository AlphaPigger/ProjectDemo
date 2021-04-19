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

            double? g = null;
            Console.WriteLine(g.Value);
            Console.ReadLine();
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
