using System;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var test1 = new Test1
            {
                Name = "test1",
                Test2 = new Test2
                {
                    Name = "test2"
                }
            };

            var test2 = (Test1)test1.ShallowCopy();
            var test3 = test1.DeepCopy(test1);

            test1.Name = "test12";
            test1.Test2.Name = "test22";

            Console.WriteLine(test2.Test2.Name);
            Console.WriteLine(test3.Test2.Name);
            Console.ReadLine();
        }
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
