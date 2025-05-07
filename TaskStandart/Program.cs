using System;
using System.Text;
using System.IO;

namespace TaskStandart
{
    class Program
    {
        static void Main()
        {
            String[] path = { @".\log1.txt", @".\log2.txt" };
            String line;

            for(int i = 0; i < path.Length; i++)
            {

                if (!File.Exists(path[i])) continue;
                StreamReader reader = new StreamReader(path[i]);
                try
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (StandartLog(line) == 1)
                        {
                            StreamWriter writer = new StreamWriter(@".\problems.txt", true);
                            try
                            {
                                writer.WriteLine(line);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Ошибка: " + e.Message);
                            }
                            finally
                            {
                                writer.Close();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        static int StandartLog(string str)
        {
            String[] parts = str.Split(" |".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            StringBuilder result = new StringBuilder();
            if (!DateTime.TryParse(parts[0] + ' ' + parts[1], out DateTime date) || parts.Length < 4)
            {
                return 1;
            }
            result.Append(date.ToString("dd-MM-yyyy"));
            result.Append('\t');
            result.Append(parts[1]);
            result.Append('\t');

            string[] level = { "INFO", "WARN", "ERROR", "DEBUG", "INFORMATION", "WARNING"};

            int flag = 0;
            for(int i = 0; i < level.Length && flag == 0; i++)
            {
                if (parts[2].Equals(level[i], StringComparison.Ordinal))
                {
                    if (i < 4) result.Append(level[i]);
                    else if(i == 4) result.Append(level[0]);
                    else result.Append(level[1]);
                    result.Append('\t');
                    flag = 1;
                }

            }

            if(flag != 1)
            {
                return 1;
            }

            int format = 0, def = 0;
            if(str.Contains('|')) //|11| ?
            {
                format = 1;
            }

            if (parts[3 + format].Contains('.'))
            {
                result.Append(parts[3 + format]);
            }
            else
            {
                result.Append("DEFAULT");
                def = 1;
            }

            result.Append('\t');

            for(int i = 4 + format - def; i < parts.Length; i++)
            {
                result.Append(parts[i]);
                if(i != parts.Length - 1) result.Append(' ');
            }
            
           
            StreamWriter writer = new StreamWriter(@".\newLog.txt", true);
            try
            {
                writer.WriteLine(result.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            finally
            {
                writer.Close();
            }

            return 0;
        }
    }
}
