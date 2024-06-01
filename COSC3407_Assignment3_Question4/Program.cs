
internal class Program
{
    public static List<States> philosophers = Enumerable.Repeat(States.THINKING, 5).ToList();
    private static void Main(string[] args)
    {
        while (true)
        {
            Random random = new Random();
            int i = random.Next(0, 4); 
            //for (int i = 0; i < philosophers.Count; i++)
            //{
                if (philosophers[i] != States.EATING)
                {
                    Pickup(i);
                }
                else
                {
                    Putdown(i);
                    
                }
                Task.Delay(2000).Wait();
            //}
        }

    }
    public static void Pickup(int i)
    {
        philosophers[i] = States.HUNGRY;

        if (philosophers[i] == States.HUNGRY && CanEat(i))
        {
            philosophers[i] = States.EATING;
            Console.WriteLine($"Philosopher {i} has both chopsticks and is eating delicious rice");
        }
        else
        {
            Console.WriteLine($"Philospher {i} is hungry");
            return;
        }
    }
    private static void Putdown(int i)
    {
        philosophers[i] = States.THINKING;
        Console.WriteLine($"Philospher {i} puts down both chopsticks and is now thinking");
        Task.Delay(1000).Wait();
        Pickup((i + 1) % 5);
        Task.Delay(1000).Wait();
        Pickup((i + 4) % 5);
    }

    public static bool CanEat(int i)
    {
        if(LeftChopstickAvailable(i))
        {
            if (RightChopstickAvailable(i))
            {
                return true;
            }
            Console.WriteLine($"Philospher {i} put down left chopstick");
            return false;
        }
        return false;
    }

    private static bool LeftChopstickAvailable(int i)
    {
        if(philosophers[(i + 1) % 5] != States.EATING)
        {
            Console.WriteLine($"Philosopher {i} picked up the left chopstick");
            return true;
        }
        Console.WriteLine($"Left chopstick not available for philosopher {i}");
        return false;
    }
    private static bool RightChopstickAvailable(int i)
    {
        if (philosophers[(i + 4) % 5] != States.EATING)
        {
            Console.WriteLine($"Philosopher {i} picked up the right chopstick");
            return true;
        }
        Console.WriteLine($"Right chopstick not available for philosopher {i}");
        return false;
    }

    public enum States
    {
        EATING,
        HUNGRY,
        THINKING
    }
}
