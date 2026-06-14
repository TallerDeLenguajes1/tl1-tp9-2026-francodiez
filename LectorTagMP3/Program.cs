using System;
using System.IO;
using tag;
string archivo;
byte[] buffer = new byte[128];

do
{
    Console.WriteLine("Ingrese la direccion de un archivo mp3");
    archivo=Console.ReadLine();

    if (!File.Exists(archivo))
    {
        Console.WriteLine("El archivo no existe");
    }
} while (!File.Exists(archivo));

FileStream mp3= new FileStream(archivo,FileMode.Open);
ID3V1TAG instancia= new ID3V1TAG();

using (mp3)
{
    mp3.Seek(-128,SeekOrigin.End);
    mp3.Read(buffer,0,3);
    instancia.Header=System.Text.Encoding.Default.GetString(buffer,0,3);
    if (instancia.Header != "TAG")
    {
        Console.WriteLine("Este archivo MP3 no contiene etiquetas ID3V1 validas.");
        return; //paro el programa
    }
    mp3.Read(buffer,0,30);
    instancia.Titulo=System.Text.Encoding.Default.GetString(buffer,0,30);
    mp3.Read(buffer,0,30);
    instancia.Artista=System.Text.Encoding.Default.GetString(buffer,0,30);
    mp3.Read(buffer,0,30);
    instancia.Album=System.Text.Encoding.Default.GetString(buffer,0,30);
    mp3.Read(buffer,0,4);
    instancia.Año= System.Text.Encoding.Default.GetString(buffer,0,4);
    mp3.Read(buffer,0,30);
    instancia.Comentario=System.Text.Encoding.Default.GetString(buffer,0,30);
    instancia.Genero= (byte)mp3.ReadByte();
}

Console.WriteLine("Datos de la TAG del archivo mp3:");
Console.WriteLine($"Titulo: {instancia.Titulo}");
Console.WriteLine($"Artista: {instancia.Artista}");
Console.WriteLine($"Album: {instancia.Album}");
Console.WriteLine($"Año: {instancia.Año}");