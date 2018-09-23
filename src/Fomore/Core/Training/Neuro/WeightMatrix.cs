using System;
using System.Collections.Generic;

namespace Core.Training.Neuro
{
    [Serializable]
    public struct WeightMatrix
    {
        private double[,] Weights { get; }

        /// <summary>
        ///     Number of columns depends on the from layer.
        /// </summary>
        public int ColumnCount { get; }

        /// <summary>
        ///     Number of rows depends on the to layer.
        /// </summary>
        public int RowCount { get; }

        public double this[int column, int row] => Weights[column, row];

        public WeightMatrix(double[,] weights)
        {
            Weights = weights;
            ColumnCount = weights.GetLength(0);
            RowCount = weights.GetLength(1);
        }

        public double[,] GetClonedWeights()
        {
            var weights = new double [Weights.GetLength(0), Weights.GetLength(1)];
            for (int i = 0; i < weights.GetLength(0); i++)
            {
                for (int j = 0; j < weights.GetLength(1); j++)
                {
                    weights[i, j] = Weights[i, j];
                }
            }

            return weights;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is WeightMatrix)) return false;

            var matrix = (WeightMatrix)obj;
            return EqualityComparer<double[,]>.Default.Equals(Weights, matrix.Weights) &&
                   ColumnCount == matrix.ColumnCount &&
                   RowCount == matrix.RowCount;
        }

        public override int GetHashCode()
        {
            int hashCode = -1291767658;
            hashCode = hashCode * -1521134295 + EqualityComparer<double[,]>.Default.GetHashCode(Weights);
            hashCode = hashCode * -1521134295 + ColumnCount.GetHashCode();
            hashCode = hashCode * -1521134295 + RowCount.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(WeightMatrix matrix1, WeightMatrix matrix2) => matrix1.Equals(matrix2);

        public static bool operator !=(WeightMatrix matrix1, WeightMatrix matrix2) => !(matrix1 == matrix2);

        public static WeightMatrix operator *(double scalar, WeightMatrix matrix)
        {
            var weights = new double[matrix.ColumnCount, matrix.RowCount];
            for (int i = 0; i < matrix.ColumnCount; i++)
            {
                for (int j = 0; j < matrix.RowCount; j++) weights[i, j] = matrix[i, j] * scalar;
            }

            return new WeightMatrix(weights);
        }

        public static WeightMatrix operator *(WeightMatrix matrix, double scalar)
        {
            var weights = new double[matrix.ColumnCount, matrix.RowCount];
            for (int i = 0; i < matrix.ColumnCount; i++)
            {
                for (int j = 0; j < matrix.RowCount; j++) weights[i, j] = matrix[i, j] * scalar;
            }

            return new WeightMatrix(weights);
        }

        public static double[] operator *(double[] values, WeightMatrix m)
        {
            var newWeights = new double[m.RowCount];

            for (int i = 0; i < m.RowCount; i++)
            {
                for (int j = 0; j < values.Length; j++) newWeights[i] += values[j] * m[j, i];
            }

            return newWeights;
        }
    }
}