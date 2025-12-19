class Prestamo
{
    static Dictionary<string, List<string>> Prestamos = new Dictionary<string, List<string>>();



    static bool Salir = false;




    public static Dictionary<string, List<string>> DiccionarioPrestamos()
    {
        return Prestamos;
    }

    // Coger prestado un libro
    public static void CogerPrestadoLibro(ref List<Libro> ListaDeLibros)
    {
        Console.Clear();
        if (Usuario.HayUsuarioActual == false)
        {
            Console.WriteLine("");
            Console.WriteLine("No estás iniciado como un usario, por favor crea uno o cambia a uno");
            Console.ReadKey();
        }
        else
        {
            while (!Salir)
            {
                Console.Clear();
                if (Comprobaciones.HayLibros())
                {
                    Menus.MenuListaDeLibros("LIBROS", Libreria.ListaDeLibros(), "Escribe el número del libro que quieres coger prestado", 0, Libreria.NumeroDeLibros(), 50);
                    if (Menus.Opcion != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("");


                        // Si tiene el libro
                        if (Prestamos[Usuario.UsuarioActual].Contains(ListaDeLibros[Menus.Opcion - 1].Titulo))
                        {
                            Console.Clear();
                            Console.WriteLine("Ya tienes este libro prestado...");
                            Console.ReadKey();
                        }

                        if (Prestamos[Usuario.UsuarioActual].Count >= 3)
                        {
                            Console.WriteLine("Ya tienes el máximo de 3 libros prestados. Devuelve alguno antes de coger más.");
                            Console.ReadKey();
                            Salir = true;
                        }

                        // Si lo tiene prestado otro usuario
                        else if (!ListaDeLibros[Menus.Opcion - 1].Disponible)
                        {
                            Console.Clear();
                            Console.WriteLine("Este libro lo tiene prestado otro usuario...");
                            Console.ReadKey();

                        }

                        // Si puede coger coger prestado el libro
                        else
                        {
                            Console.Clear();
                            ListaDeLibros[Menus.Opcion - 1].Disponible = false;
                            Prestamos[Usuario.UsuarioActual].Add(ListaDeLibros[Menus.Opcion - 1].Titulo);
                            Salir = true;
                            Console.WriteLine("¡Libro cogido prestado correctamente!");
                            Console.ReadKey();
                        }
                    }
                    Salir = true;
                }
            }
            Salir = false;
        }

    }

    // Función para devolver un libro prestado
    public static void DevolverLibro(ref List<Libro> ListaDeLibros)
    {
        Console.Clear();
        if (Usuario.HayUsuarioActual == false)
        {
            Console.WriteLine("");
            Console.WriteLine("No estás iniciado como un usario, por favor crea uno o cambia a uno");
            Console.ReadKey();
        }
        else
        {
            while (!Salir)
            {
                Console.Clear();
                if (Comprobaciones.HayLibros())
                {
                    // Muestra los libros que puede devolver
                    if (Prestamos[Usuario.UsuarioActual].Count > 0)
                    {
                        int Contador = 0;
                        Console.WriteLine("----------------------------------------------------");
                        foreach (var LibroPrestado in Prestamos[Usuario.UsuarioActual])
                        {
                            Contador += 1;
                            Console.WriteLine("");
                            Console.WriteLine($"   {Contador}. {LibroPrestado}");

                        }
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine("");
                        Menus.Opcion = Comprobaciones.PedirNumero("Que libro quieres devolver", 1, Prestamos[Usuario.UsuarioActual].Count, false);
                        Console.Clear();
                        Console.WriteLine("");

                        // Si decide devolver un libro
                        foreach (var Libro in ListaDeLibros)
                        {
                            if (Prestamos[Usuario.UsuarioActual].Contains(Libro.Titulo))
                            {
                                Libro.Disponible = true;
                            }
                        }
                        Prestamos[Usuario.UsuarioActual].RemoveAt(Menus.Opcion - 1);
                        Console.WriteLine("¡Muy bien, ya se ha devuelto el libro!");
                        Salir = true;
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("No tienes Libros prestados, ve y toma uno prestado...");
                        Salir = true;
                        Console.ReadKey();
                    }
                }

            }
            Salir = false;
        }
    }

    // Función para saber que libros prestados tiene el usuario actual
    public static void LibrosPrestados()
    {
        Console.Clear();
        if (Usuario.HayUsuarioActual == false)
        {
            Console.WriteLine("");
            Console.WriteLine("No estás iniciado como un usario, por favor crea uno o cambia a uno");
            Console.ReadKey();
        }
        else
        {
            while (!Salir)
            {
                Console.Clear();
                if (Comprobaciones.HayLibros())
                {
                    if (Prestamos[Usuario.UsuarioActual].Count > 0)
                    {
                        int Contador = 0;
                        Console.WriteLine("----------------------------------------------------");
                        foreach (var LibroPrestado in Prestamos[Usuario.UsuarioActual])
                        {
                            Contador += 1;
                            Console.WriteLine("");
                            Console.WriteLine($"   {Contador}. {LibroPrestado}");

                        }
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine("");
                        Console.WriteLine("Presiona una tecla para volver");
                        Salir = true;
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("No tienes Libros prestados, ve y toma uno prestado...");
                        Salir = true;
                        Console.ReadKey();
                    }
                }

            }
            Salir = false;
        }

    }

}