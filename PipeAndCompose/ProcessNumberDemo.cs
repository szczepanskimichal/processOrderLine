namespace ConsoleApp1;

public class ProcessNumberDemo
{
    public static void Run()
    {
        while (true)
        {
            var numberStr = Console.ReadLine();
            var number = int.Parse(numberStr!);

            //imperativ versjon
            //var a = number + 7;
            //var b = a * 3;
            //var c = Math.Clamp(b, 40, 100);
            //Console.WriteLine($"{number}=>1{a}=>{c}=>{b}");

            //curried versjoner av operasjonen
            Func<int, Func<int, int>> Add = numberA => numberB => numberA + numberB;
            Func<int, Func<int, int>> Multiply = numberA => numberB => numberA * numberB;
            Func<int, int, Func<int, int>> Clamp = (int min, int max) => number => Math.Clamp(number, min, max);
            //funksjonell version
            var c = Utils.Pipe(
                number,
                Add(7),
                Multiply(3),
                Clamp(40, 100));
            Console.WriteLine($"{number} => {c}");
        }
    }
}