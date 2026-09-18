namespace Lab02;

public static class Task2
{
    public static void Run()
    {
    int n = int.Parse(Console.ReadLine()!);
    int[] prices = new int[n];
    for (int i = 0; i < n; ++i)
    {
        prices[i] = int.Parse(Console.ReadLine()!);
    }

    // Зберігаємо початковий стан
    string before = string.Join(" ", prices);

    // Сортування бульбашкою
    for (int i = 0; i < n - 1; i++)
    {
        for (int j = 0; j < n - i - 1; j++)
        {
            if (prices[j] > prices[j + 1])
            {
                int temp = prices[j];
                prices[j] = prices[j + 1];
                prices[j + 1] = temp;
            }
        }
    }

    // Вивід одним рядком із переносами \n
    Console.WriteLine($"Черга (до): {before}\nЧерга (після): {string.Join(" ", prices)}\nНайдешевший: {prices[0]} грн\nНайдорожчий: {prices[n - 1]} грн");
    }
}