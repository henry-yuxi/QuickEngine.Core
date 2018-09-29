namespace QuickEngine.Extensions
{
    public static class ComputerSizingExtensions
    {
        private const int INT_OneKB = 1024;

        public static int KB(this int value)
        {
            return value * INT_OneKB;
        }

        public static int MB(this int value)
        {
            return value * INT_OneKB * INT_OneKB;
        }

        public static int GB(this int value)
        {
            return value * INT_OneKB * INT_OneKB * INT_OneKB;
        }

        public static int TB(this int value)
        {
            return value * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB;
        }

        public static int PB(this int value)
        {
            return value * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB;
        }

        public static int EB(this int value)
        {
            return value * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB;
        }

        public static int ZB(this int value)
        {
            return value * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB;
        }

        public static int YB(this int value)
        {
            return value * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB;
        }
    }
}