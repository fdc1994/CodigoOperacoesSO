﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace CodigoOperacoesSO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dir = @"C:\Teste"; //Diretório
            string dir2 = @"C:\Teste5"; //Novo nome
            string dirDestino = @"C:\Teste3"; //Diretório de destino
            string fich = @"fich2.txt"; //Ficheiro a ser verificado
            string dirOrigem = @"C:\Teste"; //Diretório de origem
            //VerificarSeDiretorioExiste(dir);
            // CriarDiretorio(dir);
            // VerificarSeDiretorioExiste(dir);
            // EliminarDiretorio(dir);
            // VerificarSeDiretorioExiste(dir);
            // MoverDiretorio(dir, dirDestino);
            // VerificarSeFicheiroExiste(fich);
            // CriarFicheiro(dir, fich);
            // EliminarFicheiro(fich);
            // DeslocarFicheiros(dirDestino, fich, dirOrigem);
            // CopiarFicheiros(dirDestino, fich, dirOrigem);
            // CopiarDiretorios(dirDestino, dirOrigem);
            //RenomearFicheiros(dir, fich);
            //RenomearDiretorios(dir, dir2);
            //String so = VerificarSO();
            // VerificarSoftwareInstalado();
            // VerificarDrives();
            // VerificarProcessadorEMemoria();
            // VerificarProcessos();
            // IniciarProcesso();
            // TerminarProcesso();
            // ObterCulturaSistema();

             CriarNovoFicheiro();
             EscreverFicheiro();
             LerFicheiro();
            DeleteFile();
            MoverFicheiro();
            //CopiarFicheiroNovaLocalização();
            //ApagarFicheiro();
            //VerificarSeFicheiroExiste();
            //VerificarSePastaExiste();


            VerificarSeOfficeEstaInstalado();
            VerificarHardware();
            //IniciarETerminarProcessoNotepad();
            //IniciarProcessoComPrivilegios();
            //ControlarProcessosACorrer();


            //Mantém a consola aberta
            Console.WriteLine("O Programa terminou");
            Console.Read();
        }

        private static void ControlarProcessosACorrer()
        {
            Process[] processes = Process.GetProcessesByName("notepad");

            foreach (Process process in processes)
            {
                // Send a key press to the process
                process.StandardInput.Write("Hello, world!");

                // Get the process's main window handle
                IntPtr hwnd = process.MainWindowHandle;

                // Set the process's main window to be the foreground window
                ProgramHelpers.SetForegroundWindow(hwnd);
            }
        }


        private static void IniciarProcessoComPrivilegios()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe");
            startInfo.Verb = "runas"; // run as administrator
            Process process = Process.Start(startInfo);

            // Wait for the process to finish
            process.WaitForExit();

            // Kill the process if it is still running
            if (!process.HasExited)
            {
                process.Kill();
            }
        }

        private static void VerificarHardware()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (ManagementObject obj in searcher.Get())
            {
                Console.WriteLine("CPU Name: " + obj["Name"]);
                Console.WriteLine("Number of Cores: " + obj["NumberOfCores"]);
            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");

            foreach (ManagementObject obj in searcher.Get())
            {
                Console.WriteLine("Memory Capacity: " + obj["Capacity"]);
                
                Console.WriteLine("Memory Speed: " + obj["Speed"]);
            }
        }

        private static void VerificarSeOfficeEstaInstalado()
        {
            string softwareName = "Microsoft Office";
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey);

            foreach (string subKeyName in rk.GetSubKeyNames())
            {
                RegistryKey subKey = rk.OpenSubKey(subKeyName);
                string displayName = subKey.GetValue("DisplayName") as string;
                if (displayName != null && displayName.Contains(softwareName))
                {
                    Console.WriteLine("Software found: " + displayName);
                }
            }
        }

        private static void VerificarSePastaExiste()
        {
            string folderPath = @"C:\Users\Utilizador\example.txt";
            bool folderExists = Directory.Exists(folderPath);
            Console.WriteLine(folderExists);
        }

        private static void VerificarSeFicheiroExiste()
        {
            string filePath = @"C:\Users\Utilizador\example.txt";
            bool fileExists = File.Exists(filePath);
            Console.WriteLine(fileExists);
        }

        private static void ApagarFicheiro()
        {
            string folderPath = @"C:\Users\Utilizador\example.txt";
            Directory.Delete(folderPath);
        }

        private static void CopiarFicheiroNovaLocalização()
        {
            string sourceFilePath = @"C:\Users\Utilizador\example.txt";
            string destinationFilePath = @"C:\Users\Utilizador\desktop\exampleCopy.txt";
            if(File.Exists(destinationFilePath))
            {
                Console.WriteLine("Erro a copiar: Já existe um ficheiro com esse nome");
            } else
            {
                File.Copy(sourceFilePath, destinationFilePath);
            }
        }

        private static void MoverFicheiro()
        {
            string sourceFilePath = @"C:\Users\Utilizador\example.txt";
            string destinationFilePath = @"C:\Users\Utilizador\desktop\example.txt";
            if (File.Exists(destinationFilePath))
            {
                Console.WriteLine("Erro a mover: Já existe um ficheiro com esse nome");
            }
            else
            {
                File.Move(sourceFilePath, destinationFilePath);
            }
          
        }

        private static void DeleteFile()
        {
            string filePath = @"C:\Users\Utilizador\example.txt";
            File.Delete(filePath);
        }

        private static void CriarNovoFicheiro()
        {
            string filePath = @"C:\Users\Utilizador\example.txt";
            if (File.Exists(filePath))
            {
                Console.WriteLine("Erro a criar: Já existe um ficheiro com esse nome");
            }
            else
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("This is an example text file.");
                }
            }
        }

        private static void LerFicheiro()
        {

            string filePath = @"C:\Users\Utilizador\example.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Erro a Ler: O ficheiro não existe");
            }
            else
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string fileContents = sr.ReadToEnd();
                    Console.WriteLine(fileContents);
                }
            }
           
        }

        private static void EscreverFicheiro()
        {

            string filePath = @"C:\Users\Utilizador\example.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Erro a Escrever: O ficheiro não existe");
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine("This is another line added to the file.");
                }
            }
            



        }

        private static void ObterCulturaSistema()
        {
            System.Globalization.CultureInfo cultura =
          System.Threading.Thread.CurrentThread.CurrentCulture;
            Console.WriteLine(cultura.Name); //C ultura em vigor
                                             //Alteração para francês do Canadá
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-CA");
            Console.ReadLine();
        }

        private static void IniciarETerminarProcessoNotepad()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("notepad.exe");
            Process process = Process.Start(startInfo);

            // Wait for the process to finish
            process.WaitForExit();

            // Kill the process if it is still running
            if (!process.HasExited)
            {
                process.Kill();
            }
        }

        private static void IniciarProcesso()
        {
            Process[] lista = Process.GetProcesses();
            foreach (Process processo in lista)
            {
                //Obtenção da lista de processos (ID + Nome)
                Console.WriteLine(
                    "Processo: " + processo.ProcessName + " (ID=" + processo.Id + ")");
            }
            Console.ReadLine();
        }

        private static void VerificarProcessos()
        {
            Process[] lista = Process.GetProcesses();
            foreach (Process processo in lista)
            {
                //Obtenção da lista de processos (ID + Nome)
                Console.WriteLine(
                    "Processo: " + processo.ProcessName + " (ID=" + processo.Id + ")");
            }
            Console.ReadLine();
        }

        private static void VerificarProcessadorEMemoria()
        {
            //Processador
            System.Management.ManagementObjectSearcher mos = new
            System.Management.ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (System.Management.ManagementObject mo in mos.Get())
            {
                Console.WriteLine(mo["Name"]);
            }
            //Utilização de CPU (em percentagem)
            System.Management.ManagementObject processador = new
            System.Management.ManagementObject("Win32_PerfFormattedData_PerfOS_Processor.Name='_Total'");
            processador.Get();
            Console.WriteLine(double.Parse(processador.Properties["PercentProcessorTime"].Value.ToString()));

            //Memória
            double memDisponivel;
            System.Diagnostics.PerformanceCounter perf = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");
            memDisponivel = (double)perf.NextValue();
            Console.WriteLine(memDisponivel);
            Console.ReadLine();
        }

        private static void VerificarDrives()
        {
            //Chave de registo 
            string softwareKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            //Subchave de registo 
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(softwareKey);
            //Subchaves 
            foreach (string skName in rk.GetSubKeyNames())
            {
                Microsoft.Win32.RegistryKey sk = rk.OpenSubKey(skName);
                //Se a chave contém um valor ..
                if (!(sk.GetValue("DisplayName") == null))
                {
                    //Apresentação de resultados (name + localização do software)
                    Console.WriteLine(sk.GetValue("DisplayName") + " - " +
                    sk.GetValue("InstallLocation"));
                }
            }
            Console.ReadLine();
        }

        private static void VerificarSoftwareInstalado()
        {
            OperatingSystem os = Environment.OSVersion; //Sistema operativO 
            Version ver = os.Version; //Versão 
            string resultado = "";
            int majorVer = ver.Major; //Versão superior 
            int minorVer = ver.Minor; //Versão inferior 
            switch (majorVer)
            {
                case 5:
                    resultado = "XP";
                    break;
                case 6:
                    if (minorVer == 0) { resultado = "Vista"; }
                    if (minorVer == 1) { resultado = "7"; }
                    if (minorVer == 2) { resultado = "8 ou superior"; }
                    break;
            }
            Console.WriteLine("Sistema Operativo: Windows  " + resultado);
            Console.WriteLine("Build: " + ver.Build); //Build 
            string sp = os.ServicePack; //Service Pack aplicado 
            if (sp != "")
            {
                Console.WriteLine("SP: " + sp);
            }
            else
            {
                Console.WriteLine("SP: Nenhum");
            }
            Console.ReadLine();
        }

        private static string VerificarSO()
        {
            OperatingSystem os = Environment.OSVersion; //Sistema operativO 
            Version ver = os.Version; //Versão 
            string resultado = "";
            int majorVer = ver.Major; //Versão superior 
            int minorVer = ver.Minor; //Versão inferior 
            switch (majorVer)
            {
                case 5:
                    resultado = "XP";
                    break;
                case 6:
                    if (minorVer == 0) { resultado = "Vista"; }
                    if (minorVer == 1) { resultado = "7"; }
                    if (minorVer == 2) { resultado = "8 ou superior"; }
                    break;
            }
            Console.WriteLine("Sistema Operativo: Windows  " + resultado);
            Console.WriteLine("Build: " + ver.Build); //Build 
            string sp = os.ServicePack; //Service Pack aplicado 
            if (sp != "")
            {
                Console.WriteLine("SP: " + sp);
            }
            else
            {
                Console.WriteLine("SP: Nenhum");
            }
            Console.ReadLine();
            return resultado;
        }

        private static void RenomearDiretorios(string dir, string dir2)
        {
            if (Directory.Exists(dir))
            {
                //Renomeação
                Directory.Move(dir, dir2);
                Console.WriteLine("O diretório foi renomeado.");
            }
            else
            {
                Console.WriteLine("Diretorio nao renomeado, pois não existe.");
            }
            Console.ReadLine();
        }

        private static void RenomearFicheiros(string dir, string fich)
        {
            if (File.Exists(dir + @"\" + fich))
            {
                //Renomeação
                File.Move(dir + @"\" + fich, dir + @"\" + "fich2.txt");
                Console.WriteLine("O ficheiro foi renomeado.");
            }
            else
            {
                Console.WriteLine("Ficheiro não renomeado, pois não existe.");
            }
            Console.ReadLine();
        }

        private static void CopiarDiretorios(string dirDestino, string dirOrigem)
        {
            /**
             * Como o método GetFiles, da classe Directory, devolve cadeias de carateres alfanuméricos
             * correspondentes aos caminhos completos até aos ficheiros contidos no diretório especificado, 
             * recorre­-se habitualmente a classe Path para se extrair apenas os nomes dos ficheiros.
             **/
            if (Directory.Exists(dirOrigem))
            {
                if (!Directory.Exists(dirDestino))
                {
                    Directory.CreateDirectory(dirDestino);
                }
                foreach (string fich in Directory.GetFiles(dirOrigem))
                {
                    //Cópia
                    File.Copy(fich, dirDestino + @"/" + Path.GetFileName(fich));
                }
                Console.WriteLine("O diretório foi copiado.");
            }
            else
            {
                Console.WriteLine("Diretório não copiado, pois não existe.");
            }
            Console.ReadLine();
        }

        private static void CopiarFicheiros(string dirDestino, string fich, string dirOrigem)
        {
            if (File.Exists(dirOrigem + @"\" + fich))
            {
                if (!Directory.Exists(dirDestino))
                {
                    Directory.CreateDirectory(dirDestino);
                }
                if (!File.Exists(dirDestino + @"\" + fich))
                {
                    File.Copy(dirOrigem + @"\" + fich, dirDestino + @"\" + fich); //C6pia
                    Console.WriteLine("O ficheiro foi copiado.");
                }
                else
                {
                    Console.WriteLine("Ficheiro não copiado.");
                }
            }
            else
            {
                Console.WriteLine("O ficheiro não foi copiado, pois não existe.");
            }
            Console.ReadLine();
        }

        private static void DeslocarFicheiros(string dirDestino, string fich, string dirOrigem)
        {
            if (File.Exists(dirOrigem + @"\" + fich))
            {
                if (!Directory.Exists(dirDestino))
                {
                    Directory.CreateDirectory(dirDestino);
                }
                File.Move(dirOrigem + @"\" + fich, dirDestino + @"/" + fich);
                Console.WriteLine("O ficheiro foi movido.");
            }
            else
            {
                Console.WriteLine("O ficheiro não foi movido, pois não existe.");
            }
            Console.ReadLine();
        }

        private static void EliminarFicheiro(string fich)
        {
            if (File.Exists(fich))
            {
                File.Delete(fich); //Eliminação do ficheiro
                Console.WriteLine("O ficheiro foi eliminado.");
            }
            else
            {
                Console.WriteLine("Ficheiro não foi eliminado, pois não existe.");
            }
            Console.ReadLine();
        }

        private static void CriarFicheiro(string dir, string fich)
        {
            if (!File.Exists(dir + "/" + fich))
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir); //Criação do diretório
                }
                File.CreateText(dir + "/" + fich); //Criação do ficheiro
                Console.WriteLine("O ficheiro foi criado.");
            }
            else
            {
                Console.WriteLine("O ficheiro não foi criado, pois já existe.");
            }
            Console.ReadLine();
        }

        private static void VerificarSeFicheiroExiste(string fich)
        {
            if (!File.Exists(fich))
            {
                Console.WriteLine("O ficheiro não existe.");
            }
            else
            {
                Console.WriteLine("O ficheiro existe.");
            }
            Console.ReadLine();
        }

        private static void MoverDiretorio(string dirOrigem, string dirDestino)
        {
            /**
             * No exemplo apresentado verificou-se a existência de ambos os diretórios. 
             * Como é óbvio, o diretório de origem apenas pode ser movido caso exista. 
             * Relativamente ao diretório de destino, se já existir, procede-se a sua eliminação, através do método Delete, da classe Directory; 
             * em aplicações profissionais poderá (e deverá) alertar o utilizador para este facto, dando-lhe a possibilidade de cancelar a operação de substituição de um diretório já existente por outro prestes a ser movido. 
             * **/
            if (!Directory.Exists(dirOrigem))
            {
                Console.WriteLine("O diretorio nao foi movido, pois não existe.");
            }
            else
            {
                if (!Directory.Exists(dirDestino))
                {
                    Directory.Move(dirOrigem, dirDestino); //Deslocação
                    Console.WriteLine("O diretório foi movido.");
                }
                else
                {
                    Directory.Delete(dirDestino, true);
                    Directory.Move(dirOrigem, dirDestino); //Deslocação
                    Console.WriteLine("O diretório foi movido.");

                }
            }
            Console.ReadLine();
        }

        private static void EliminarDiretorio(string dir)
        /**
         * O valor true, especificado no segundo argumento do método Delete, permite a sua aplicação recursiva, isto é, todo o seu conteúdo (ficheiros e/ou subdiretórios) será também eliminado. 
         * Se este argumento não for especificado, apenas poderá eliminar diretórios vazios.
         * **/
        {
            if (!Directory.Exists(dir)) //Verificação
            {
                Console.WriteLine("Diretório não eliminado, pois não existe.");
            }
            else
            {
                Directory.Delete(dir, true); //Eliminação
                Console.WriteLine("O diretório foi eliminado.");
            }
            Console.ReadLine();
        }

        private static void CriarDiretorio(string dir)
        {
            /**
             * Para criar um diretório recorremos ao método CreateDirectory, da classe Directory. 
             */
            //Diretório a ser criado
            if (!Directory.Exists(dir)) //Verificação
            {
                Directory.CreateDirectory(dir); //Criação
                Console.WriteLine("O diretório foi criado.");
            }
            else
            {
                Console.WriteLine("O diretório não foi criado, pois já existe.");
                Console.ReadLine();
            }
        }

        private static void VerificarSeDiretorioExiste(string dir)
        {
            /**
             * A utilização da arroba (@) imediatamente antes da string referente ao caminho completo do diretório permite ao compilador não entender o backslash (\) 
             * como uma sequência de escape. Como alternativa, poderá utilizar o slash (/) sem arroba (@) para dividir a estrutura de diretórios; 
             * por exemplo, string dir = "C:/Teste"
             * **/
            if (Directory.Exists(dir)) //Verificacao 
            {
                Console.WriteLine("O diretório existe.");
            }
            else
            {
                Console.WriteLine("O diretório não existe.");
            }
            Console.ReadLine();
        }
    }
}
