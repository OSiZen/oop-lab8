using System;
using System.IO;
using System.Text;

namespace lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            ////Task 1
            string path_d = "D:\\OOP_lab08";

            void Copy(string path1, string path2)
            {
                try
                {
                    if ((!Directory.Exists(path1) || !Directory.Exists(path2)) || (!Directory.Exists(path1) && !Directory.Exists(path2)))
                    {
                        Console.WriteLine($"Не iснує потрiбних каиалогів!!!");
                    }
                    else
                    {
                        DirectoryInfo dirinf1 = new DirectoryInfo(path1);
                        DirectoryInfo dirinf2 = new DirectoryInfo(path2);
                        string name = dirinf1.Name;
                        Directory.CreateDirectory(path2 + $@"\{name}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            void Moving(string path1, string path2)
            {
                try
                {
                    if (!Directory.Exists(path1))
                    {
                        Console.WriteLine($"Не iснує потрiбних каиалогів!!!");
                    }
                    else
                    {
                        DirectoryInfo dirinf = new DirectoryInfo(path2);
                        if (dirinf.Exists)
                        {
                            dirinf.Delete(true);
                        }
                        new DirectoryInfo(path1).MoveTo(path2);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            try
            {
                Directory.CreateDirectory(path_d);
                Directory.CreateDirectory(path_d + "\\KNms1-B21");
                Directory.CreateDirectory(path_d + "\\Shevchuk");
                Directory.CreateDirectory(path_d + "\\Sources");
                Directory.CreateDirectory(path_d + "\\Reports");
                Directory.CreateDirectory(path_d + "\\Texts");
                Copy(path_d + "\\Texts", path_d + "\\Shevchuk");
                Copy(path_d + "\\Sources", path_d + "\\Shevchuk");
                Copy(path_d + "\\Reports", path_d + "\\Shevchuk");
                Moving(path_d + "\\Shevchuk", path_d + "\\KNms1-B21");
                if (!File.Exists(path_d + "\\Texts\\dirinfo.txt"))
                {
                    File.Create(path_d + "\\Texts\\dirinfo.txt");
                }
                if (File.Exists(path_d + "\\Texts\\dirinfo.txt") == true)
                {
                    string dirName = "path_d" + "\\Texts";
                    DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                    string text = $"Назва каталогу: {dirInfo.Name}\nРозташування каталогу: {dirInfo.FullName}\nЧас створення каталогу: {dirInfo.CreationTimeUtc}\nКореневий каталог: {dirInfo.Root}";
                    using (FileStream fstream = new FileStream(path_d + "\\Texts\\dirinfo.txt", FileMode.OpenOrCreate))
                    {
                        byte[] array = Encoding.Default.GetBytes(text);
                        fstream.Write(array, 0, array.Length);
                    }
                    using (FileStream fstream = File.OpenRead(path_d + "\\Texts\\dirinfo.txt"))
                    {
                        byte[] array = new byte[fstream.Length];
                        fstream.Read(array, 0, array.Length);
                        text = Encoding.Default.GetString(array);
                    }
                    Console.WriteLine($"У файлi dirinfo.txt записано таке:\n{text}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Task 2
            string file1 = "1.txt";
            string file2 = "2.txt";
            string file3 = "3.txt";
            string file4 = "4.txt";
            string file5 = "5.txt";

            void Exchange(string path1, string path2)
            {
                try
                {
                    if ((!File.Exists(path1) || !File.Exists(file2)) || !File.Exists(path1) && !File.Exists(file2))
                    {
                        Console.WriteLine($"Не iснує усiх потрiбних файлiв!!!");
                    }
                    else
                    {
                        string text = "2";
                        using (FileStream fstream = File.OpenRead(path1))
                        {
                            byte[] array = new byte[fstream.Length];
                            fstream.Read(array, 0, array.Length);
                            text = Encoding.Default.GetString(array);
                        }
                        using (FileStream fstream = new FileStream(path2, FileMode.Create))
                        {
                            byte[] array = Encoding.Default.GetBytes(text);
                            fstream.Write(array, 0, array.Length);
                            Console.WriteLine($"Здiйснено обмiн мiж {path1} та {path2}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            try
            {
                Exchange(file1, file3);
                Exchange(file2, file4);
                Exchange(file3, file5);
                Exchange(file4, file2);
                Exchange(file5, file1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string GetValue(string path)
            {
                string text = "";
                using (FileStream fstream = File.OpenRead(path))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    text = Encoding.Default.GetString(array);
                }
                return text;
            }

            Console.WriteLine();

            if (GetValue(file1) == GetValue(file5))
            {
                Console.WriteLine($"Обмiн мiж {file5} й {file1} здiйснено успiшно");
            }
            if (GetValue(file2) == GetValue(file4))
            {
                Console.WriteLine($"Обмiн мiж {file4} й {file2} здiйснено успiшно");
            }

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
