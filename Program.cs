// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
//int num = Random.Shared.Next(1,100);
int num = NahodneCislo();
int NahodneCislo() => Random.Shared.Next(1, 100);
Console.WriteLine("Num: {0}", num);
while (true)
{
    string? vstup = Console.ReadLine();
    if (vstup == "konec")
    { break; }
    if (!int.TryParse(vstup, out int tip)
        {
        Console.WriteLine("Tipni si cislo:");
        continue;
        }
    // int tip = int.Parse(Console.ReadLine());
    
    string vetsiNeboMensi = tip < num ? tip "menší" : "větší";
    Console.WriteLine($"Tvoje cislo je {vetsiNeboMensi} nez myslene cislo");
    /*
    if (tip < num)
        Console.WriteLine("Musis zvetsit cislo");
    if (tip > num)
        Console.WriteLine("Musis zmensit cislo");*/
    if (tip == num)
    { 
        Console.WriteLine("Uhodl jsi cislo, bylo to {0}", num);
        Console.WriteLine("Chces hrat znovu? A/N);
        string? aNn = Console.ReadLine();
        if (aNn?.ToLower() == "a") {
            Console.Clear();
            num = NahodneCislo();
        }
        break;
}
Console.ReadKey();
