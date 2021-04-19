using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTestProject1
{
    /// <summary>
    /// �ֽ��㷨�⣺Ѱ�Ҵ��ڵ�������������С���ұ����е���
    /// ���磺1��2��4��3��5��6
    /// �����5
    /// ��������ɲο��ĵ���https://blog.csdn.net/u013677156/article/details/37904319
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
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 8, 3 },//��
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 8, 3 },//��
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 8, 6 },//5
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 1, 4 },//��
                new int[] { 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1 },//��
                new int[] { 1, 4, 2, 3, 5, 6, 7, 8, 9, 10},//5,6,7,8,9
                new int[] { 1, 4, 2, 3, 5, 4, 6, 7, 8, 9 },//��
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },//2,3,4,5,6,7,8,9
                new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 },//��
            };

            Trace.WriteLine("---��˫����������ʵ��");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = GetResult(source);

                Trace.WriteLine($"�ܹ�ִ���� {_DEBUGCOUNT} ��ѭ�� \t ��������{string.Join(",", result)}, ");
            }

            Trace.WriteLine("---�Էֶμ��㷨ʵ��");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = ReturnIntArray(source);

                Trace.WriteLine($"�ܹ�ִ���� {_DEBUGCOUNT} ��ѭ�� \t ��������{string.Join(",", result)}, ");
            }

            Trace.WriteLine("---�Էֶμ��㷨ʵ��V2");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = ReturnIntArrayV2(source);

                Trace.WriteLine($"�ܹ�ִ���� {_DEBUGCOUNT} ��ѭ�� \t ��������{string.Join(",", result)}, ");
            }

            Trace.WriteLine("---�Էֶμ��㷨ʵ��V3");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = ReturnIntArrayV3(source);

                Trace.WriteLine($"�ܹ�ִ���� {_DEBUGCOUNT} ��ѭ�� \t ��������{string.Join(",", result)}, ");
            }

            Trace.WriteLine("---˫��ѭ��Ƕ��ʵ��LHP");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = GetResultLHP(source);

                Trace.WriteLine($"�ܹ�ִ���� {_DEBUGCOUNT} ��ѭ�� \t ��������{string.Join(",", result)}, ");
            }
        }

        #region ˫���������� wangdong
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

        #region �Էֶμ��㷨ʵ�� tianlinqing
        /// <summary>
        /// ʵ��˼·��ѭ������ѭ����ÿ����ֵ����ȥ������ǰ����������к�������Ƚϡ�
        /// ȱ�ݣ�ʱ�临�Ӷȸ� O(n*n)
        /// �ŵ㣺˼·ֱ��
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

        #region �Էֶμ��㷨ʵ��V2  tianlinqing
        /// <summary>
        /// ˼·�������ѭ����ʱ���õ���ǰѭ��ֵ�������Ĭ�����ֵ�Ƚϣ��ó���ǰֵǰ������ֵ��������Ϊ���õ�beforeMax��ѭ����
        /// ȱ�ݣ�Ϊ�˵õ�AfterMin����Ȼ���кܶ��ѭ������������ѭ�������ѭ����һ���˻���ϵ��ʱ�临�ӶȽϸ�
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

                //��ǰ����������С������������λ������������ֵ
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

        #region �Էֶμ��㷨ʵ��V3 tianlinqing
        /// <summary>
        /// ˼·��beforeMax��ȷ�ϻ�������V2�汾��˼·��AfterMin��ȷ��˼·����Ĭ�ϵķ�ʽ��ͨ�������жϣ����������ֵ�ֵС��Ĭ�ϵ�min���������Ƴ�֮ǰ�����ֵ
        /// �ŵ㣺ʱ�临�Ӷȵ�
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

        #region ѭ��Ƕ�� wangdong
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
