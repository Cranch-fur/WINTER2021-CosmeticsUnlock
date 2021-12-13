using System;

namespace DBD_WINTER2021
{
    class Program
    {
        private static void IncreaseConsoleBufferSize()
        {
            Console.SetIn(new System.IO.StreamReader(Console.OpenStandardInput(),
                               Console.InputEncoding,
                               false,
                               bufferSize: 1024));
        }
        private enum HTTP_StatusCode
        {
            NOT_FOUND = 404,
            BHVRSESSION_INCORRECT = 403,
            BHVRSESSION_CORRECT = 200,
            INTERNAL_SOFTWARE_ERROR = 0,
        }

        static void Main(string[] args)
        {
            IncreaseConsoleBufferSize();
            Console.WriteLine("               ░██████╗███████╗██████╗░██╗░░░██╗███████╗██████╗░███╗░░██╗░█████╗░███╗░░░███╗███████╗");
            Console.WriteLine("               ██╔════╝██╔════╝██╔══██╗██║░░░██║██╔════╝██╔══██╗████╗░██║██╔══██╗████╗░████║██╔════╝");
            Console.WriteLine("               ╚█████╗░█████╗░░██████╔╝╚██╗░██╔╝█████╗░░██████╔╝██╔██╗██║███████║██╔████╔██║█████╗░░");
            Console.WriteLine("               ░╚═══██╗██╔══╝░░██╔══██╗░╚████╔╝░██╔══╝░░██╔══██╗██║╚████║██╔══██║██║╚██╔╝██║██╔══╝░░");
            Console.WriteLine("               ██████╔╝███████╗██║░░██║░░╚██╔╝░░███████╗██║░░██║██║░╚███║██║░░██║██║░╚═╝░██║███████╗");
            Console.WriteLine("               ╚═════╝░╚══════╝╚═╝░░╚═╝░░░╚═╝░░░╚══════╝╚═╝░░╚═╝╚═╝░░╚══╝╚═╝░░╚═╝╚═╝░░░░░╚═╝╚══════╝");
            Console.WriteLine("                         [===] WINTER 2021 - BONE CHILL EVENT COSMETICS UNLOCKER [===]");
            Console.WriteLine("                                    https://cranchpalace.info/discord.php\n");
            Console.Write("Specify your platform:\n[1] Steam\n[2] Epic Games\n[3] Microsoft Store\n> ");
            string SpecifiedPlatform = Console.ReadLine();
            if (int.TryParse(SpecifiedPlatform, out int SpecifiedPlatformInt))
            {
                switch (SpecifiedPlatformInt)
                {
                    case 1:
                        httpwrapper.HTTP_Subdomain = "steam";
                        httpwrapper.HTTP_Platform = "steam";
                        break;

                    case 2:
                        httpwrapper.HTTP_Subdomain = "brill";
                        httpwrapper.HTTP_Platform = "egs";
                        break;

                    case 3:
                        httpwrapper.HTTP_Subdomain = "grdk";
                        httpwrapper.HTTP_Platform = "grdk";
                        break;

                    default:
                        Console.WriteLine("\nPlease, specify correct platform number ( 1 | 2 | 3 )...");
                        System.Threading.Thread.Sleep(2500); System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPlease, specify the number as selected platform ( 1 | 2 | 3 )...");
                System.Threading.Thread.Sleep(2500); System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(0);
            }


            Console.Write("\nbhvrSession=");
            string SpecifiedSession = Console.ReadLine();
            if (SpecifiedSession.Length < 64 || string.IsNullOrEmpty(SpecifiedSession))
            {
                Console.WriteLine("\nPlease, specify correct bhvrSession on next launch...");
                System.Threading.Thread.Sleep(2500); System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(0);
            }


            int SessionCheckResponseCode = httpwrapper.WEBREQUEST_GET(SpecifiedSession);
            if (SessionCheckResponseCode != 200)
            {
                Console.WriteLine($"\nSomething went wrong with bhvrSession: {Enum.GetName(typeof(HTTP_StatusCode), SessionCheckResponseCode)}");
                System.Threading.Thread.Sleep(2500); System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(0);
            }
            else
                Console.WriteLine("[+] bhvrSession accepted & confirmed\n[?] Trying to obtain all event rewards...");


            for (int a = 0; a < 32; a++)
            {
                int SpecialEventUnlockResponseCode = httpwrapper.WEBREQUEST_POST(SpecifiedSession, a);
                if (SpecialEventUnlockResponseCode != 200)
                    Console.WriteLine($"[-] Progress State №{a} for {httpwrapper.HTTP_PlayerRole} not obtained, reason: {Enum.GetName(typeof(HTTP_StatusCode), SpecialEventUnlockResponseCode)}");
                else Console.WriteLine($"[+] Progress State №{a} for {httpwrapper.HTTP_PlayerRole} successfully obtained!");
            }

            httpwrapper.HTTP_PlayerRole = "killer";
            httpwrapper.HTTP_ActionKey = "killerSnowmanDestroyed";
            for (int b = 0; b < 32; b++)
            {
                int SpecialEventUnlockResponseCode = httpwrapper.WEBREQUEST_POST(SpecifiedSession, b);
                if (SpecialEventUnlockResponseCode != 200)
                    Console.WriteLine($"[-] Progress State №{b} for {httpwrapper.HTTP_PlayerRole} not obtained, reason: {Enum.GetName(typeof(HTTP_StatusCode), SpecialEventUnlockResponseCode)}");
                else Console.WriteLine($"[+] Progress State №{b} for {httpwrapper.HTTP_PlayerRole} successfully obtained!");
            }
        }
    }
}
