using System;
using System.IO;
using System.Linq;

class Temperature
{
    public string City { get; }
    private readonly double[] temperatures = new double[7];

    // Konstruktor domyślny
    public Temperature() : this("Białystok") { }

    // Konstruktor z nazwą miasta
    public Temperature(string city)
    {
        City = city;
        temperatures = new double[7]; // automatycznie ustawia 0
    }

    // Konstruktor z nazwą miasta i temperaturami
    public Temperature(string city, double[] temps)
    {
        City = city;
        Array.Copy(temps, temperatures, Math.Min(temps.Length, 7));
    }

    // Odczyt temperatury w Kelwinach, Celsjuszach i Fahrenheitach
    public double GetKelvin(int day) => temperatures[day];
    public double GetCelsius(int day) => temperatures[day] - 273.15;
    public double GetFahrenheit(int day) => (temperatures[day] - 273.15) * 9 / 5 + 32;

    // Ustawianie temperatury w Kelwinach, Celsjuszach i Fahrenheitach
    public void SetKelvin(int day, double kelvin) => temperatures[day] = kelvin;
    public void SetCelsius(int day, double celsius) => temperatures[day] = celsius + 273.15;
    public void SetFahrenheit(int day, double fahrenheit) => temperatures[day] = (fahrenheit - 32) * 5 / 9 + 273.15;

    // Metody analizy temperatur
    public double GetAverageTemperatureKelvin() => temperatures.Average();
    public double GetMinTemperatureKelvin() => temperatures.Min();
    public double GetMaxTemperatureKelvin() => temperatures.Max();

    // Nadpisanie ToString() dla czytelnego wyświetlania danych
    public override string ToString() => $"{City}, {string.Join(", ", temperatures)}";
}

class Program
{
    static Temperature[] cities = new Temperature[15];
    static int cityCount = 0;
    static string filePath = "cities.txt";

    // Załaduj dane z pliku
    static void LoadFromFile()
    {
        if (!File.Exists(filePath)) return;

        var lines = File.ReadAllLines(filePath).Skip(1);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var cityName = parts[0];
            var temps = parts.Skip(1)
                             .Select(t => double.TryParse(t, out var temp) ? temp : 0)
                             .ToArray();

            cities[cityCount++] = new Temperature(cityName, temps);
        }
    }

    // Zapis danych do pliku
    static void SaveToFile()
    {
        File.WriteAllLines(filePath, new[] { cityCount.ToString() }.Concat(cities.Take(cityCount).Select(c => c.ToString())));
        Console.WriteLine("Dane zapisane do pliku.");
    }

    // Wyświetlenie temperatur dla wszystkich miast
    static void DisplayAllCities()
    {
        for (int i = 0; i < cityCount; i++)
        {
            Console.WriteLine(cities[i]);
        }
    }

    // Wyświetlenie danych dla konkretnego miasta
    static void DisplayCity()
    {
        Console.Write("Podaj nazwę miasta: ");
        var cityName = Console.ReadLine();
        var city = cities.FirstOrDefault(c => c.City.Equals(cityName, StringComparison.OrdinalIgnoreCase));
        if (city != null)
        {
            Console.WriteLine(city);
        }
        else
        {
            Console.WriteLine("Miasto nie znalezione.");
        }
    }

    // Zmiana temperatury
    static void ModifyTemperature()
    {
        Console.Write("Podaj nazwę miasta: ");
        var cityName = Console.ReadLine();
        var city = cities.FirstOrDefault(c => c.City.Equals(cityName, StringComparison.OrdinalIgnoreCase));
        if (city == null)
        {
            Console.WriteLine("Miasto nie znalezione.");
            return;
        }

        Console.Write("Podaj dzień (1-7): ");
        if (!int.TryParse(Console.ReadLine(), out int day) || day < 1 || day > 7)
        {
            Console.WriteLine("Nieprawidłowy dzień.");
            return;
        }

        Console.Write("Podaj temperaturę i jednostkę (np. 273 K, 0 C, 32 F): ");
        var input = Console.ReadLine().Split(' ');
        if (input.Length != 2 || !double.TryParse(input[0], out double value))
        {
            Console.WriteLine("Nieprawidłowy format.");
            return;
        }

        string unit = input[1].ToUpper();
        if (unit == "K") city.SetKelvin(day - 1, value);
        else if (unit == "C") city.SetCelsius(day - 1, value);
        else if (unit == "F") city.SetFahrenheit(day - 1, value);
        else
        {
            Console.WriteLine("Nieznana jednostka.");
        }
    }

    // Dodanie nowego miasta
    static void AddCity()
    {
        Console.Write("Podaj nazwę miasta: ");
        string city = Console.ReadLine();
        double[] temps = new double[7];
        for (int i = 0; i < 7; i++)
        {
            Console.Write($"Podaj temperaturę w dniu {i + 1} (K): ");
            temps[i] = double.TryParse(Console.ReadLine(), out double t) ? t : 0;
        }

        cities[cityCount++] = new Temperature(city, temps);
    }

    // Obsługa menu
    static void Main()
    {
        LoadFromFile();
        while (true)
        {
            Console.WriteLine("1. Wyświetl temperatury wszystkich miast");
            Console.WriteLine("2. Wyświetl temperatury wybranego miasta");
            Console.WriteLine("3. Zmień temperaturę w danym dniu");
            Console.WriteLine("4. Dodaj nowe miasto");
            Console.WriteLine("5. Zapisz dane do pliku");
            Console.WriteLine("6. Wyjście");
            Console.Write("Wybierz opcję: ");

            switch (Console.ReadLine())
            {
                case "1": DisplayAllCities(); break;
                case "2": DisplayCity(); break;
                case "3": ModifyTemperature(); break;
                case "4": AddCity(); break;
                case "5": SaveToFile(); break;
                case "6": return;
                default: Console.WriteLine("Nieprawidłowa opcja."); break;
            }
        }
    }
}
