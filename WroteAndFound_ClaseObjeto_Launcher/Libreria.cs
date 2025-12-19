class Libreria
{

    private string Nombre { get; set; }
    private string Creador { get; set; }
    private DateOnly CreadoEl { get; set; }
    static List<Libro> Libros = new List<Libro>();


    static bool Salir = false;


    public Libreria(string nombre, string creador, DateOnly creadoEn)
    {
        this.Nombre = nombre;
        this.Creador = creador;
        this.CreadoEl = creadoEn;
    }

    public static List<Libro> ListaDeLibros()
    {
        return Libros;
    }

    public static int NumeroDeLibros()
    {
        return Libros.Count;
    }
    public static int NumeroDeResenyas()
    {
        int NumeroDeResenyas = 0;
        foreach (Libro Li in Libros)
        {
            NumeroDeResenyas += Li.Resenyas.Count;
        }
        return NumeroDeResenyas;
    }

    public void AgregarLibro(Libro Libro)
    {
        Libros.Add(Libro);
    }

    public static void AgregarResenya(int Opcion, Resenya Resenya)
    {
        Libros[Opcion - 1].Resenyas.Add(Resenya);
    }





    public static void VerLibros()
    {
        Console.Clear();
        if (Comprobaciones.HayLibros())
        {
            do
            {
                Menus.MenuListaDeLibros("LIBROS", Libros, "Escribe el número del libro que quieres ver más", 0, Libros.Count, 50);
                if (Menus.Opcion > 0 && Menus.Opcion <= Libros.Count)
                {
                    VerCaracteristicasLibro(Menus.Opcion - 1, false);

                }
                else
                {
                    Salir = true;
                }

            } while (!Salir);
        }
        Salir = false;
    }

    // Ver características del libro que se pida
    public static void VerCaracteristicasLibro(int l, bool special)
    {
        string Disponible = Comprobaciones.EstaDisponible(Libros, l);

        int i = 5;
        while (!Salir)
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"   Libro {l + 1}:");
            Console.WriteLine($"   Título: {Libros[l].Titulo}");
            Console.WriteLine($"   Autor:  {Libros[l].Autor}");
            Console.WriteLine($"   Año:    {Libros[l].Anyo}");
            Console.WriteLine($"   Género: {Libros[l].Genero}");
            Console.WriteLine("");
            Console.WriteLine($"   Estado: " + Disponible);
            i = Resenya.CaracteristicasResenyas(l, i, Libros);
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("");
            if (i != 6 && !special)
            {
                // Simplemente tiene que poner "Si" para salir volver
                string OpcionString = Comprobaciones.PedirString("Escribe \"Si\" para volver atras", 50);
                if (OpcionString == "Si")
                {
                    Salir = true;
                }
            }
            else if (i == 6 && !special)
            {
                Console.WriteLine("Presiona una tecla para volver...");
                Console.ReadKey();
                Salir = true;
            }
            else
            {
                Salir = true;
            }
        }
        Salir = false;
    }




    // Función añadir libro
    public static void AnhadirLibro()
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
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("   ¡Vamos a añadir un nuevo libro!");
                Console.WriteLine("");
                Console.WriteLine($"📘 Libro {Libros.Count + 1}:");

                // Crea una nueva lista , dentro de la lista de Libros
                string Titulo = Comprobaciones.PedirString("Título", 40);
                string Autor = Comprobaciones.PedirString("Autor", 40);
                int Anyo = Comprobaciones.PedirNumero("Año", -4000, 2025, false);
                string Genero = Comprobaciones.PedirString("Género", 40);

                Libros.Add(new Libro(Titulo, Autor, Anyo, Genero));
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("¡Libro añadido!");
                Console.WriteLine("");
                string StringSalir = Comprobaciones.PedirString("Escribe \"salir\" si no quieres seguir añadiendo", 10);
                if (StringSalir == "salir")
                {
                    Salir = true;
                }
            }
            Salir = false;
        }
    }

    public static void EliminarLibro()
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
                    Menus.MenuListaDeLibros("LIBROS", Libros, "Escribe el número del libro que quieres eliminar", 0, Libros.Count, 50);
                    if (Menus.Opcion != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("");
                        Console.Write("Escribe \"si\" para confirmar o cualquier otra cosa para cancelar: ");
                        string confirmar = Console.ReadLine() ?? "";
                        Console.WriteLine("");
                        if (confirmar == "si")
                        {
                            // Lo de abajo, es para que se borre el libros y no aparezca como prestado en un usuario.

                            foreach (var Usuario in Prestamo.DiccionarioPrestamos())
                            {
                                foreach (var Pres in Usuario.Value)
                                {
                                    if (Libros[Menus.Opcion - 1].Titulo == Pres)
                                    {
                                        foreach (var usuario in Prestamo.DiccionarioPrestamos())
                                        {
                                            usuario.Value.Remove(Libros[Menus.Opcion - 1].Titulo);
                                        }
                                        Libros.RemoveAt(Menus.Opcion - 1);
                                    }
                                }
                            }
                            Console.WriteLine("!Libro borrado del catálogo!");
                        }
                        else
                        {
                            Console.WriteLine("Has cancelado borrar un libro");
                        }

                        Console.WriteLine("");
                        Console.Write("Escribe \"si\", si quieres borrar otro");
                        string borrarOtro = Console.ReadLine() ?? "";
                        if (borrarOtro != "si")
                        {
                            Salir = true;
                        }
                    }
                    else
                    {
                        Salir = true;
                    }
                }
            }
            Salir = false;
        }
    }

    // Menu Buscar Libro por lo que sea
    public static void BuscarLibro()
    {
        do
        {

            Console.Clear();
            string[] menu = { "Buscar por Título", "Buscar por Autor", "Buscar por año", "Buscar por Genero", "Buscar por Reseñas" };
            Menus.MenuBonito("BUSCAR POR", menu, "Escribe el número de lo que quieres hacer", 0, 5, 37);
            switch (Menus.Opcion)
            {
                case 1:
                    BuscarLibroPor("Título");
                    Console.ReadKey();
                    break;
                case 2:
                    BuscarLibroPor("Autor");
                    Console.ReadKey();
                    break;
                case 3:
                    BuscarLibroPor("Anyo");
                    Console.ReadKey();
                    break;
                case 4:
                    BuscarLibroPor("Género");
                    Console.ReadKey();
                    break;
                case 5:
                    BuscarLibroPor("Reseñas");
                    Console.ReadKey();
                    break;
                case 0:
                    Salir = true;
                    break;
            }
        } while (!Salir);
        Salir = false;
    }

    // Función buscar en cada caso
    static void BuscarLibroPor(string tipo)
    {
        Console.Clear();

        // Contador de resultados
        int HayResultados = 0;

        // Cuantas estrellas tiene una reseña
        int Estrellas = -1;


        string BuscarString = "";

        int BuscarInt = 0;



        // Si no hay reseñas, sale
        if (tipo == "Reseñas" && !Comprobaciones.HayResenyas(Libros))
        {
            Console.WriteLine("No hay reseñas en ningún libro.");
            Console.WriteLine("");
            Console.WriteLine("Presiona una tecla para volver atrás");
            return;
        }

        // Si son reseñas pide int, sino un string.
        if (tipo == "Reseñas")
        {
            Estrellas = Comprobaciones.PedirNumero($"Escribe la {tipo} del libro", 1, 5, false);
        }
        else if (tipo == "Anyo")
        {
            BuscarInt = Comprobaciones.PedirNumero($"Escribe el {tipo} en el que se escribió el libro", -4000, 2025, false);
        }
        else
        {
            BuscarString = Comprobaciones.PedirString($"Escribe el {tipo} del libro", 30);
        }

        Console.WriteLine("");
        Console.WriteLine("");

        // Como se muestra si es título, Reseña, y las demas juntas.
        foreach (Libro Lib in Libros)
        {
            if (tipo == "Título" && BuscarString == Lib.Titulo)
            {
                Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                Console.WriteLine("");
                HayResultados += 1;
            }
            else if (tipo == "Autor" && BuscarString == Lib.Autor)
            {
                Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                Console.WriteLine($"       {Lib.Autor}");
                Console.WriteLine("");
                HayResultados += 1;

            }
            else if (tipo == "Anyo" && BuscarInt == Lib.Anyo)
            {
                Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                Console.WriteLine($"       {Lib.Anyo}");
                Console.WriteLine("");
                HayResultados += 1;
            }
            else if (tipo == "Género" && BuscarString == Lib.Genero)
            {
                Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                Console.WriteLine($"       {Lib.Genero}");
                Console.WriteLine("");
                HayResultados += 1;
            }
            else if (tipo == "Reseñas")
            {
                foreach (Resenya Res in Lib.Resenyas)
                {
                    if (Estrellas == Res.Calificacion.Length)
                    {
                        Console.WriteLine($"  {HayResultados + 1}. {Lib.Titulo}");
                        Console.WriteLine($"       {Res.Calificacion}");
                        Console.WriteLine("");
                        HayResultados += 1;
                    }
                }
            }
        }
        // Si hay resultados, te dice quantos.
        if (HayResultados != 0)
        {   // Uno para Reseñas y el otro para los demás
            if (tipo == "Reseñas")
            {
                Console.WriteLine($"Hay {HayResultados} resultado/s para {Estrellas} estrellas.");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine($"Hay {HayResultados} resultado/s para {BuscarString}.");
                Console.WriteLine("");
            }

        }
        else
        {
            Console.WriteLine("No hay resultados...");
            Console.WriteLine("");
            Console.WriteLine("Presiona una tecla para volver atrás");
        }
    }
}
