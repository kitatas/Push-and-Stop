namespace RollingBall.Common.Utility
{
    public static class ArrayExtension
    {
        public static int GetLastIndex<T>(this T[] array)
        {
            return array.Length - 1;
        }

        public static T GetLastParam<T>(this T[] array)
        {
            return array[array.GetLastIndex()];
        }

        public static bool IsOutOfIndexRange<T>(this T[] array, int index)
        {
            return array == null || index < 0 || index > array.GetLastIndex();
        }

        public static bool IsOutOfIndexRangeOrNull<T>(this T[] array, int index)
        {
            return array.IsOutOfIndexRange(index) || array[index] == null;
        }

        public static bool TryGetValue<T>(this T[] array, int index, out T value)
        {
            if (array.IsOutOfIndexRangeOrNull(index))
            {
                value = default;
                return false;
            }

            value = array[index];
            return true;
        }
    }
}