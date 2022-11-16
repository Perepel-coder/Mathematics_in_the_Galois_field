namespace Mathematics
{
    public static class MathematicsGF
    {
        /// <summary>
        /// Деление полинома на полином в поле Галуа принимает два полинома в десятичном виде
        /// </summary>
        /// <param name="divisible"> полином-делимое </param>
        /// <param name="divisor"> полином-делитель </param>
        /// <returns>string{целое, остаток}</returns>
        public static int[] DivOfPolyGF(int divisible, int divisor)
        {
            if (Convert.ToString(divisible, 2).Length < Convert.ToString(divisor, 2).Length) { return new int[2] { 0, divisible }; }
            if (divisor == 0) { throw new Exception("Деление на 0"); }
            if (divisor == 1) { return new int[2] { divisible, 0 }; }
            string strDivisible = Convert.ToString(divisible, 2);
            string strDivisor = Convert.ToString(divisor, 2);

            string quotient = string.Empty;
            string remainder = strDivisible.Substring(0, strDivisor.Length);

            int counter = strDivisor.Length;
            int divisibleLenght = strDivisible.Length;

            while (true)
            {
                remainder = Convert.ToString(Convert.ToInt32(remainder, 2) ^ Convert.ToInt32(strDivisor, 2), 2);
                quotient += "1";
                while (remainder.Length != strDivisor.Length)
                {
                    if (counter < divisibleLenght)
                    {
                        remainder += strDivisible[counter]; counter++;
                        remainder = Convert.ToString(Convert.ToInt32(remainder, 2), 2);
                        if (remainder.Length != strDivisor.Length) { quotient += "0"; }
                    }
                    else
                    {
                        return new int[2] { Convert.ToInt32(quotient, 2), Convert.ToInt32(remainder, 2) };
                    }
                }
            }
        }

        /// <summary>
        /// Деление полинома на полином в поле Галуа принимает два полинома в десятичном виде
        /// </summary>
        /// <param name="divisible"> полином-делимое </param>
        /// <param name="divisor"> полином-делитель </param>
        /// <returns>string{целое, остаток}</returns>
        public static long[] DivOfPolyGF(long divisible, long divisor)
        {
            if (Convert.ToString(divisible, 2).Length < Convert.ToString(divisor, 2).Length) { return new long[2] { 0, divisible }; }
            if (divisor == 0) { throw new Exception("Деление на 0"); }
            if (divisor == 1) { return new long[2] { divisible, 0 }; }
            string strDivisible = Convert.ToString(divisible, 2);
            string strDivisor = Convert.ToString(divisor, 2);

            string quotient = string.Empty;
            string remainder = strDivisible.Substring(0, strDivisor.Length);

            int counter = strDivisor.Length;
            int divisibleLenght = strDivisible.Length;

            while (true)
            {
                remainder = Convert.ToString((long)(Convert.ToUInt64(remainder, 2) ^ Convert.ToUInt64(strDivisor, 2)), 2);
                quotient += "1";
                while (remainder.Length != strDivisor.Length)
                {
                    if (counter < divisibleLenght)
                    {
                        remainder += strDivisible[counter]; counter++;
                        remainder = Convert.ToString((long)Convert.ToUInt64(remainder, 2), 2);
                        if (remainder.Length != strDivisor.Length) { quotient += "0"; }
                    }
                    else
                    {
                        return new long[2] { (long)Convert.ToUInt64(quotient, 2), (long)Convert.ToUInt64(remainder, 2) };
                    }
                }
            }
        }

        /// <summary>
        /// Умножение полиномов в поле Галуа
        /// </summary>
        /// <param name="poly1"></param>
        /// <param name="poly2"></param>
        /// <param name="modPoly"> неприводимый полином</param>
        /// <returns> int[1] = полином-произведение, int[0] = целое от деления на modPoly </returns>
        public static int MultOfPolyGF(int poly1, int poly2, int modPoly)
        {
            int result = 0;
            if (poly1 < poly2) { int cup = poly1; poly1 = poly2; poly2 = cup; }

            while (true)
            {
                if (poly2 == 0) { break; }
                int currentBit = poly2 & 0x01;
                if (currentBit == 1) { result ^= poly1; }
                poly1 <<= 1; poly2 >>= 1;
            }
            return DivOfPolyGF(result, modPoly)[1];
        }

        /// <summary>
        /// Умножение полиномов в поле Галуа
        /// </summary>
        /// <param name="poly1"></param>
        /// <param name="poly2"></param>
        /// <param name="modPoly"> неприводимый полином</param>
        /// <returns>long[1] = полином-произведение, long[0] = целое от деления на modPoly</returns>
        public static long MultOfPolyGF(long poly1, long poly2, long modPoly)
        {
            long result = 0;
            if (poly1 < poly2) { long cup = poly1; poly1 = poly2; poly2 = cup; }

            while (poly2 != 0)
            {
                if ((poly2 & 0x01) == 1) { result ^= poly1; }
                poly1 <<= 1; poly2 >>= 1;
            }
            return DivOfPolyGF(result, modPoly)[1];
        }

        /// <summary>
        /// Нахождение инверсного сомножителя в поле Галуа
        /// </summary>
        /// <param name="poly"></param>
        /// <param name="modPoly">неприводимый полином</param>
        /// <returns>мульти-инверсия полинома</returns>
        public static int InversionPolynomialGF(int poly, int modPoly)
        {
            if (poly == 0) { return 0; }
            if (poly == 1) { return 1; }
            int divisible = modPoly;
            int divisor = poly;
            int remainderD = 0;
            int remainderB = 0;
            int counter = 0;

            while (true)
            {
                counter++;
                int[] arr = DivOfPolyGF(divisible, divisor);
                int quotient = arr[0];
                int remainder = arr[1];

                divisible = divisor;
                divisor = remainder;

                if (counter == 1)
                {
                    remainderD = quotient;
                    if (remainder == 1) { return remainderD; }
                    continue;
                }
                if (counter == 2)
                {
                    remainderB = 1 ^ MultOfPolyGF(remainderD, quotient, modPoly);
                    if (remainder == 1) { return remainderB; }
                    continue;
                }

                int cup = remainderB;
                remainderB = remainderD ^ MultOfPolyGF(quotient, remainderB, modPoly);
                remainderD = cup;

                if (remainder == 1 || remainder == 0) { return remainderB; }
            }
        }

        /// <summary>
        /// Нахождение инверсного сомножителя в поле Галуа
        /// </summary>
        /// <param name="poly"></param>
        /// <param name="modPoly">неприводимый полином</param>
        /// <returns>мульти-инверсия полинома</returns>
        public static long InversionPolynomialGF(long poly, long modPoly)
        {
            if (poly == 0) { return 0; }
            if (poly == 1) { return 1; }
            long divisible = modPoly;
            long divisor = poly;
            long remainderD = 0;
            long remainderB = 0;
            long counter = 0;

            while (true)
            {
                counter++;
                long[] arr = DivOfPolyGF(divisible, divisor);
                long quotient = arr[0];
                long remainder = arr[1];

                divisible = divisor;
                divisor = remainder;

                if (counter == 1)
                {
                    remainderD = quotient;
                    if (remainder == 1) { return remainderD; }
                    continue;
                }
                if (counter == 2)
                {
                    remainderB = 1 ^ MultOfPolyGF(remainderD, quotient, modPoly);
                    if (remainder == 1) { return remainderB; }
                    continue;
                }

                long cup = remainderB;
                remainderB = remainderD ^ MultOfPolyGF(quotient, remainderB, modPoly);
                remainderD = cup;

                if (remainder == 1 || remainder == 0) { return remainderB; }
            }
        }

        /// <summary>
        /// Умножение матриц в поле Галуа
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <param name="modPoly"> образующий полином </param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int[,] MultOfMatrixGF(int[,] matrix1, int[,] matrix2, int modPoly)
        {
            if (matrix1.GetLength(1) != matrix2.GetLength(0)) { throw new Exception("Ошибочные параметры"); }
            int[,] result = new int[matrix1.GetLength(0), matrix2.GetLength(1)];

            for (int i = 0; i < matrix1.GetLength(0); i++)          // пройтись по всем строкам
            {
                int[] rowMatrix1 = Enumerable.Range(0, matrix1.GetLength(1))    // взять строку
                .Select(x => matrix1[i, x])
                .ToArray();

                for (int j = 0; j < matrix2.GetLength(1); j++)                  // пройтись по всем столбцам
                {
                    int[] columnMatrix2 = Enumerable.Range(0, matrix2.GetLength(0))     // взять столбец
                    .Select(x => matrix2[x, j])
                    .ToArray();

                    for (int x = 0; x < rowMatrix1.Length; x++)
                    {
                        var cup = MathematicsGF.MultOfPolyGF(rowMatrix1[x], columnMatrix2[x], modPoly);
                        result[i, j] ^= MathematicsGF.MultOfPolyGF(rowMatrix1[x], columnMatrix2[x], modPoly);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Сложение матриц в поле Галуа
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int[,] XorMatrix(int[,] matrix1, int[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(0) ||
                matrix1.GetLength(1) != matrix2.GetLength(1))
            { throw new Exception("Ошибочные параметры"); }

            int[,] result = new int[matrix1.GetLength(0), matrix1.GetLength(1)];

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    result[i, j] = matrix1[i, j] ^ matrix2[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Создание матрицы из полинома
        /// </summary>
        /// <param name="poly"></param>
        /// <returns></returns>
        public static int[,] CreatBITMatrixFromPoly(int poly, int size)
        {
            string strPoly = Convert.ToString(poly, 2);
            int[,] result = new int[size, 1];
            for (int i = strPoly.Length - 1; i >= 0; i--)
            {
                result[i, 0] = int.Parse(strPoly[strPoly.Length - i - 1].ToString());
            }
            return result;
        }

        /// <summary>
        /// Создание полинома из матрицы
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int CreatPolyFromBITMatrix(int[,] matrix)
        {
            string result = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result = matrix[i, j] + result;
                }
            }
            return Convert.ToInt32(result, 2);
        }
    }
}