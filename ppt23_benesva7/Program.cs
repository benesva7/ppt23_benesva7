Console.WriteLine("Ahoj, vítej ve hře");
Console.WriteLine("Pro ukonceni napis \"konec\"");
//int num = Random.Shared.Next(1,100);
int NahodneCislo() => Random.Shared.Next(1, 101);
int mysleneCislo = NahodneCislo();
//Console.WriteLine($"Myslene cislo: {mysleneCislo}");
while (true)
{
    
    Console.Write("Hadej cislo:");
    string? vstup = Console.ReadLine();
    if (vstup == "konec")
        break;
    if (!int.TryParse(vstup, out int hadaneCislo))
        {
        Console.WriteLine("Tipni si cislo:");
        continue;
    }
    // int tip = int.Parse(Console.ReadLine());
    if (hadaneCislo == mysleneCislo)
    {
        Console.WriteLine($"Uhodl jsi, myslene cislo bylo {mysleneCislo}");
        Console.WriteLine("Chces hadat znovu (A/N)?");
        string? aNn = Console.ReadLine();
        if (aNn?.ToLower() == "a")
        {
            Console.Clear();
            mysleneCislo = NahodneCislo();
            //Console.WriteLine($"Myslene cislo: {mysleneCislo}");
        }
        else
            break;
    }
    else
    {
        string vetsiNeboMensi = hadaneCislo < mysleneCislo ? "menší" : "větší";
        Console.WriteLine($"Tvoje cislo je {vetsiNeboMensi} nez myslene cislo");
    }
    
    /*
    if (tip < num)
        Console.WriteLine("Musis zvetsit cislo");
    if (tip > num)
        Console.WriteLine("Musis zmensit cislo");*/
    
}
