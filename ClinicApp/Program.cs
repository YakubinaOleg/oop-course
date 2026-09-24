using ClinicApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Patient p1 = new Patient("Іван", "Петренко", new DateTime(1983, 5, 14), "A+", "0501234567", "ivan@gmail.com");
Patient p2 = new Patient("Олена", "Коваль", new DateTime(1991, 11, 30), "B-", "0672345678", "");
Patient p3 = new Patient("Максим", "Бойко", new DateTime(2010, 3, 15), "0+", "0933456789", "");
Patient p4 = new Patient();
Patient p5 = new Patient("Марія", "Ткач");

Console.WriteLine(p1);
Console.WriteLine(p2);
Console.WriteLine(p3);
Console.WriteLine(p4);
Console.WriteLine(p5);