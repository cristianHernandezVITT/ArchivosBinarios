using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class Program
    {
        class ArchivosBinarioEmpleados
        {
            //Declaración de flujos
            BinaryReader br = null;
            BinaryWriter bw = null;

            //Campos de la clase
            string Nombre, Direccion;
            long Telefono;
            int NumEmp, DiasTrabajados;
            float SalarioDiario;
            public void CrearArchivo(string Archivo)
            {
                char resp;
                try
                {
                    //Creación de flujo para escribir datos al archivo
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));
                    //Captura de datos
                    do
                    {
                        Console.Clear();
                        Console.Write("Número del empleado: ");
                        NumEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del empleado: ");
                        Nombre = Console.ReadLine();
                        Console.Write("Dirección del empleado: ");
                        Direccion = Console.ReadLine();
                        Console.Write("Teléfono del empleado: ");
                        Telefono = Int64.Parse(Console.ReadLine());
                        Console.Write("Días trabajados del empleado: ");
                        DiasTrabajados = int.Parse(Console.ReadLine());
                        Console.Write("Salario diario del empleado: ");
                        SalarioDiario = Single.Parse(Console.ReadLine());

                        //Escribe los datos al archivo
                        bw.Write(NumEmp);
                        bw.Write(Nombre);
                        bw.Write(Direccion);
                        bw.Write(Telefono);
                        bw.Write(DiasTrabajados);
                        bw.Write(SalarioDiario);

                        Console.Write("\n\n¿Desea almacenar otro registro? (s/n)");
                        resp = char.Parse(Console.ReadLine());

                    } while ((resp == 's' || resp == 'S'));
                }
                catch(IOException e)
                {
                    Console.WriteLine("\nError:" + e.Message);
                    Console.WriteLine("\nRuta: " + e.StackTrace);
                }
                finally
                {
                    if (bw != null) bw.Close();//Cierra el flujo de escritura
                    Console.WriteLine("Presione ENTER para terminar la escritura de datos y regrresar al menú");
                    Console.ReadKey();
                }
            }
            public void MostrarArchivo(string Archivo)
            {
                try
                {
                    //Verifica que exista el archivo
                    if (File.Exists(Archivo))
                    {
                        //Creación flujo para leer archivos
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));
                        //Despliegue de datos en pantalla
                        Console.Clear();
                        do
                        {
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Direccion = br.ReadString();
                            Telefono = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();
                            //Muestra los datos
                            Console.WriteLine("Número del empleado: " + NumEmp);
                            Console.WriteLine("Nombre del empleado: " + Nombre);
                            Console.WriteLine("Dirección del empleado: " + Direccion);
                            Console.WriteLine("Teléfono del empleado: " + Telefono);
                            Console.WriteLine("Días trabajados del empleado: " + DiasTrabajados);
                            Console.WriteLine("Salario diario del empleado: {0:C}", SalarioDiario);
                            Console.WriteLine("SUELDO TOTAL del empleado: {0:C}", DiasTrabajados * SalarioDiario);
                            Console.WriteLine("\n");

                        } while (true);

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl Archivo " + Archivo + " No existe en el disco!!");
                        Console.WriteLine("Presione ENTER para continuar");
                        Console.ReadKey();
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("\nError:" + e.Message);
                    Console.WriteLine("\nRuta:" + e.StackTrace);
                }
                finally
                {
                    if (br != null) br.Close();
                    Console.WriteLine("Presione ENTER para terminar la lectura de datos y regresar al menú");
                    Console.ReadKey();
                }
            }
        }
        static void Main(string[] args)
        {
            //Declaración de variables auxiliares
            string Arch = null;
            int opcion;
            
            //Creacion del objeto
            ArchivosBinarioEmpleados Al = new ArchivosBinarioEmpleados();

            //Menú de opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n***ARCHIVOS BINARIOS EMPLEADOS***");
                Console.WriteLine("1.- Creación de un archivo");
                Console.WriteLine("2.- Lectura de un archivo");
                Console.WriteLine("3.- Salida del programa");
                Console.Write("\n¿Qué opción deseas?");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //Bloque de escritura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el nombre del archivo a crear: ");
                            Arch = Console.ReadLine();
                            //Verifica si existe el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("El archivo Existe!!, Desea sobreescribirlo (s/n)");
                                resp = Char.Parse(Console.ReadLine());

                            }
                            if (resp == 's' || resp == 'S')
                                Al.CrearArchivo(Arch);
                        }
                        catch(IOException e)
                        {
                            Console.WriteLine("\nError:" + e.Message);
                            Console.WriteLine("\nRuta:" + e.StackTrace);
                        }
                        break;
                    case 2:
                        //Bloque de lectura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el nombre del archivo que desea leer: ");
                            Arch = Console.ReadLine();
                            Al.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError:" + e.Message);
                            Console.WriteLine("\nRuta:" + e.StackTrace);
                        }
                        break;
                    case 3: 
                        Console.WriteLine("Presione ENTER para salir del programa");
                        Console.ReadKey();break;
                    default: 
                        Console.WriteLine("Esa opción no existe!!, presione ENTER para continuar...");
                        Console.ReadKey();break;
                }
            } while (opcion !=3);
        }
    }
}
