using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTestProject1
{
    /// <summary>
    /// 字节算法题：寻找大于等于左面所有且小于右边所有的数
    /// 例如：1，2，4，3，5，6
    /// 结果：5
    /// 解决方案可参考文档：https://blog.csdn.net/u013677156/article/details/37904319
    /// </summary>
    [TestClass]
    public class CaculateMiddleNumber
    {
        static int _DEBUGCOUNT = 0;
        [TestMethod]
        public void Execute()
        {
            var sources = new int[][]
            {
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 8, 3 },//空
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 8, 3 },//空
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 8, 6 },//5
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 1, 4 },//空
                new int[] { 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1 },//空
                new int[] { 1, 4, 2, 3, 5, 6, 7, 8, 9, 10},//5,6,7,8,9
                new int[] { 1, 4, 2, 3, 5, 4, 6, 7, 8, 9 },//空
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },//2,3,4,5,6,7,8,9
                new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 },//空
            };

            Trace.WriteLine("---以双链表消除法实现");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = GetResult(source);

                Trace.WriteLine($"总共执行了 {_DEBUGCOUNT} 次循环 \t 计算结果：{string.Join(",", result)}, ");
            }

            Trace.WriteLine("---以分段计算法实现");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = ReturnIntArray(source);

                Trace.WriteLine($"总共执行了 {_DEBUGCOUNT} 次循环 \t 计算结果：{string.Join(",", result)}, ");
            }

            Trace.WriteLine("---以分段计算法实现V2");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = ReturnIntArrayV2(source);

                Trace.WriteLine($"总共执行了 {_DEBUGCOUNT} 次循环 \t 计算结果：{string.Join(",", result)}, ");
            }

            Trace.WriteLine("---以分段计算法实现V3");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = ReturnIntArrayV3(source);

                Trace.WriteLine($"总共执行了 {_DEBUGCOUNT} 次循环 \t 计算结果：{string.Join(",", result)}, ");
            }

            Trace.WriteLine("---双层循环嵌套实现LHP");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = GetResultLHP(source);

                Trace.WriteLine($"总共执行了 {_DEBUGCOUNT} 次循环 \t 计算结果：{string.Join(",", result)}, ");
            }
        }

        #region 双链表消除法 wangdong
        public IEnumerable<int> GetResult(int[] source)
        {
            LinkedList<int> result = new LinkedList<int>();

            bool preEnd = false;
            for (int i = 1, minIdex = 0, maxIndex = source.Length - 1; i < source.Length; i++)
            {
                _DEBUGCOUNT++;

                if (i >= maxIndex)
                    break;

                int min = source[minIdex];
                int current = source[i];
                int max = source[maxIndex];

                if (min >= max)
                {
                    RemoveLast(result, max);
                    break;
                }

                if (min < current && current < max && !preEnd)
                {
                    result.AddLast(current);
                    minIdex = i;
                }
                else if (current <= min)
                {
                    RemoveLast(result, current);
                }
                else if (current >= max)
                {
                    preEnd = true;
                }
                else
                {
                    RemoveLast(result, min);
                }
            }

            return result;
        }

        private void RemoveLast(LinkedList<int> result, int min)
        {
            while (result.Count > 0 && result.Last.Value >= min)
            {
                _DEBUGCOUNT++;
                result.RemoveLast();
            }
        }
        #endregion

        #region 以分段计算法实现 tianlinqing
        /// <summary>
        /// 实现思路：循环里套循环，每次拿值，都去与所有前面的数和所有后面的数比较。
        /// 缺陷：时间复杂度高 O(n*n)
        /// 优点：思路直观
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> ReturnIntArray(int[] input)
        {
            var result = new List<int>();

            for (int i = 1; i < input.Length - 1; i++)
            {
                _DEBUGCOUNT++;

                var beforeMax = BeforeMax(input, i + 1);
                var afterMin = AfterMin(input, i + 1);
                if (input[i] >= beforeMax && input[i] < afterMin)
                    result.Add(input[i]);
            }

            return result;
        }

        private static int BeforeMax(int[] input, int beforeNum)
        {
            var max = input[0];

            for (int i = 0; i < beforeNum; i++)
            {
                _DEBUGCOUNT++;

                if (input[i] > max)
                    max = input[i];
            }

            return max;
        }

        private static int AfterMin(int[] input, int afterNumber)
        {
            var min = input[afterNumber];

            for (int i = afterNumber; i < input.Length; i++)
            {
                _DEBUGCOUNT++;

                if (input[i] < min)
                    min = input[i];
            }

            return min;
        }
        #endregion

        #region 以分段计算法实现V2  tianlinqing
        /// <summary>
        /// 思路：在外层循环的时候，拿到当前循环值和最初的默认最大值比较，得出当前值前面的最大值，减少了为了拿到beforeMax的循环。
        /// 缺陷：为了得到AfterMin，依然会有很多次循环，而且这种循环和外层循环是一个乘积关系，时间复杂度较高
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> ReturnIntArrayV2(int[] input)
        {
            var result = new List<int>();

            var beforeMax = input[0];
            var afterMin = AfterMin(input,2);
            for (int i = 1; i < input.Length - 1; i++)
            {
                _DEBUGCOUNT++;
 
                if (input[i] >= beforeMax && input[i] < afterMin)
                {
                    result.Add(input[i]);
                }

                //提前缓存最大和最小，随着索引移位，更新这两个值
                if (input[i] >= beforeMax)
                {
                    beforeMax = input[i];
                }
                if (i!=input.Length-2&&input[i+1] == afterMin)
                {
                    afterMin = AfterMin(input, i + 2);
                }
            }

            return result;
        }
        #endregion

        #region 以分段计算法实现V3 tianlinqing
        /// <summary>
        /// 思路：beforeMax的确认还是沿用V2版本中思路，AfterMin的确定思路采用默认的方式，通过条件判断，如果后面出现的值小于默认的min，则按条件移除之前加入的值
        /// 优点：时间复杂度低
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> ReturnIntArrayV3(int[] input)
        {
            var result = new List<int>();

            var beforeMax = input[0];
            var afterMin = input[2];
            if (beforeMax > afterMin)
                return result;
            for (int i = 1; i < input.Length - 1; i++)
            {
                _DEBUGCOUNT++;

                var removeFlag = false;

                if (input[i] >= beforeMax && input[i] < afterMin)
                {
                    result.Add(input[i]);
                }

                if (input[i] >= beforeMax)
                {
                    beforeMax = input[i];
                }
                if (i != input.Length - 2)
                {
                    removeFlag = input[i + 2] < afterMin;
                    afterMin = input[i + 2];
                }

                if (removeFlag)
                {
                    Remove(result,afterMin);
                }
            }

            return result;
        }

        private static void Remove(List<int> input,int compareValue)
        {
            while (input.Count > 0)
            {
                _DEBUGCOUNT++;

                if (input[input.Count-1] >= compareValue)
                {
                    input.RemoveAt(input.Count-1);
                    continue;
                }

                break;
            }
        }
        #endregion

        #region 循环嵌套 wangdong
        public List<int> GetResultLHP(int[] arr)
        {
            List<int> arry = new List<int>();

            for (int i = 1; i < arr.Length - 1; i++)
            {
                _DEBUGCOUNT++;

                var isBreak = false;
                for (int j = 0; j < arr.Length; j++)
                {
                    _DEBUGCOUNT++;

                    if (j != i)
                    {
                        if (j < i)
                        {
                            if (arr[i] <= arr[j])
                            {
                                isBreak = true;
                                break;
                            }
                        }
                        else
                        {
                            if (arr[i] >= arr[j])
                            {
                                isBreak = true;
                                break;
                            }
                        }
                    }
                }
                if (!isBreak)
                {
                    arry.Add(arr[i]);
                }
            }

            return arry;
        }
        #endregion
    }
}
