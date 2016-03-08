namespace RideTheHorse
{
    using System;
    using System.Collections.Generic;

    public class RideTheHorse
    {
        private const int startValue = 1;

        private static int[,] matrix;
        private static Queue<Horse> possibleMoves;

        public static void Main()
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            int rowOfStartPosition = int.Parse(Console.ReadLine());
            int colOfStartPosition = int.Parse(Console.ReadLine());

            matrix = new int[rows, cols];
            possibleMoves = new Queue<Horse>();

            var horse = new Horse(rowOfStartPosition, colOfStartPosition, startValue);

            possibleMoves.Enqueue(horse);

            MakeMove();

            int colToPrint = cols / 2;

            Console.WriteLine();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (col == colToPrint)
                    {
                        Console.WriteLine(matrix[row, col]);
                    }
                }
            }
        }

        private static void MakeMove()
        {
            while (possibleMoves.Count > 0)
            {
                var horse = possibleMoves.Dequeue();
                matrix[horse.X, horse.Y] = horse.Value;

                TryMove(horse, -2, 1);
                TryMove(horse, -1, 2);
                TryMove(horse, 1, 2);
                TryMove(horse, 2, 1);
                TryMove(horse, 2, -1);
                TryMove(horse, 1, -2);
                TryMove(horse, -1, -2);
                TryMove(horse, -2, -1);
            }
        }

        private static void TryMove(Horse horse, int deltaX, int deltaY)
        {
            int newX = horse.X + deltaX;
            int newY = horse.Y + deltaY;

            bool isInMatrix = newX >= 0 &&
                newX < matrix.GetLength(0) &&
                newY >= 0 &&
                newY < matrix.GetLength(1);

            if (isInMatrix)
            {
                bool isCellFree = matrix[newX, newY] == 0;

                if (isCellFree)
                {
                    possibleMoves.Enqueue(new Horse(newX, newY, horse.Value + 1));
                }
            }
        }
    }
}
