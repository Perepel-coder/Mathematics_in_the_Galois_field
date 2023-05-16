namespace Mathematics
{
    public static class Math
    {
        /// <summary>
        /// Расширенный алгоритм Эвклида
        /// </summary>
        /// <param name="a">число</param>
        /// <param name="b">число</param>
        /// <param name="x">коэф перед 1 числом</param>
        /// <param name="y">коэф перед 2 числом</param>
        /// <returns></returns>
        public static int GCD(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            int x1, y1;
            int d = GCD(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }

        public static int NOD(int num1, int num2)
        {
            if (num1 == num2)
            {
                return num1;
            }
            if (num1 > num2)
            {
                return NOD(num1 - num2, num2);
            }
            return NOD(num2 - num1, num1);
        }

        public static bool IsSimple(int num)
        {
            if (1 < num && num <= 3)
            {
                return true;
            }
            if (num == 1 || num % 2 == 0 || num % 3 == 0)
            {
                return false;
            }
            for (int i = 5; i * i <= num; i += 6)
            {
                if (num % i == 0 || num % (i + 2) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static int GetMutuallySimple(int fn)
        {
            Random random = new();
            while (true)
            {
                int e = NextSimple(random, 2, fn - 1);
                if (Mathematics.Math.NOD(e, fn) == 1)
                {
                    return e;
                }
            }
        }
        public static int NextSimple(Random random, int min, int max)
        {
            int num;
            while (true)
            {
                num = random.Next(min, max);
                if (Mathematics.Math.IsSimple(num))
                {
                    break;
                }
            }
            return num;
        }
    }
}