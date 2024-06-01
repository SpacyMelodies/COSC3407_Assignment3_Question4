
internal class Program
{
    public static List<States> philosophers = Enumerable.Repeat(States.THINKING, 5).ToList();
    private static void Main(string[] args)
    {
        while (true)
        {
            
            for (int i = 0; i < philosophers.Count; i++)
            {
                if (philosophers[i] != States.EATING)
                {
                    Pickup(i);
                }
                else
                {
                    Putdown(i);
                    
                }
                Task.Delay(2000).Wait();
            }
        }

    }
    public static void Pickup(int i)
    {
        philosophers[i] = States.HUNGRY;

        if (philosophers[i] == States.HUNGRY && CanEat(i))
        {
            philosophers[i] = States.EATING;
            Console.WriteLine($"Philosopher {i} eating delicious rice");
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
        Console.WriteLine($"Philospher {i} is thinking");
        Task.Delay(1000).Wait();
        Pickup((i + 1) % 5);
        Task.Delay(1000).Wait();
        Pickup((i + 4) % 5);
    }

    public static bool CanEat(int i)
    {
        if(LeftForkAvailable(i))
        {
            if (RightForkAvailable(i))
            {
                return true;
            }
            Console.WriteLine($"Philospher {i} put down left fork");
            return false;
        }
        return false;
    }

    private static bool LeftForkAvailable(int i)
    {
        if(philosophers[(i + 4) % 5] != States.EATING)
        {
            Console.WriteLine($"Philosopher {i} picked up the left fork");
            return true;
        }
        Console.WriteLine($"Left fork not available for philosopher {i}");
        return false;
    }
    private static bool RightForkAvailable(int i)
    {
        if (philosophers[(i + 1) % 5] != States.EATING)
        {
            Console.WriteLine($"Philosopher {i} picked up the right fork");
            return true;
        }
        Console.WriteLine($"Right fork not available for philosopher {i}");
        return false;
    }

    public enum States
    {
        EATING,
        HUNGRY,
        THINKING
    }
}
