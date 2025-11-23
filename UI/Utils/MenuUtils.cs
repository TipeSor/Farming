using TipeUtils;
using static Farming.Program;
namespace Farming.UI
{
    public static class Utils
    {
        public static T Read<T>(Predicate<T>? predicate = null)
            where T : notnull, new()
        {
            Console.WriteLine();
            while (true)
            {
                Reset();
                Result<T> result;

                if ((result = In.Read<T>()).IsError)
                {
                    continue;
                }

                if (predicate != null && !predicate.Invoke(result.Value))
                    continue;

                return result.Value;
            }

            static void Reset()
            {
                Console.Write("\u001b[1A\u001b[2K\r>>> ");
                In.Clear();
            }
        }
    }
}
