using System;
using System.IO;
string directorio;
string nombre;
string[] carpetas;
string[] archivos;
double tamañoKB;
string reporte;
string fila;
DateTime fechaModificacion;

do
{
    Console.WriteLine("Ingrese la direccion de un directorio valido para analizar");
    directorio=Console.ReadLine();

    if (!Directory.Exists(directorio))
    {
    Console.WriteLine("El directorio ingresado no existe");
    }
} while (!Directory.Exists(directorio));

carpetas= Directory.GetDirectories(directorio);

foreach (string carpeta in carpetas)
{
    nombre=Path.GetFileName(carpeta);
    Console.WriteLine($"{nombre}/"); //para solo mostrar el nombre de la carpeta
}

archivos= Directory.GetFiles(directorio);

foreach (string ruta in archivos)
{
    FileInfo info = new FileInfo(ruta);
    tamañoKB = info.Length / 1024.0; //info.Length tamaño en Bytes y lo transformo a KB
    nombre=info.Name; //nombre del archivo
    Console.WriteLine($"- {nombre} ({tamañoKB:F2} KB)");
}

reporte= Path.Combine(directorio,"reporte_archivos.csv");
DirectoryInfo carp = new DirectoryInfo(directorio);

StreamWriter escritura = new StreamWriter(reporte,false, System.Text.Encoding.UTF8); //para crear csv y escribir. false es para reemplazar el archivo y el encoding es por si hay acentos o ñ
using (escritura)
{
    escritura.WriteLine("Nombre,Tamaño (KB),Ultima modificacion");
    foreach(FileInfo info in carp.GetFiles())
    {
        if (info.Name == "reporte_archivos.csv") continue; //por si encuentra el csv que acaba de crear
        tamañoKB = info.Length / 1024.0; 
        nombre=info.Name; 
        fechaModificacion = info.LastWriteTime; //para la ultima fecha de modificacion con la hora en formato segun la computadora actual
        fila=$"{nombre},{tamañoKB:F2},{fechaModificacion}";
        escritura.WriteLine(fila);
    }
}

Console.WriteLine("CSV creado con exito");

