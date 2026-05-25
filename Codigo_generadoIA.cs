using System;
using System.IO;

namespace BibliotecaIA
{
    struct Libro
    {
        public string codigo;
        public string titulo;
        public string autor;
        public string editorial;
        public int anio;
        public string categoria;
        public int cantidad;
    }

    struct Usuario
    {
        public string carne;
        public string nombre;
        public string carrera;
        public string correo;
        public string telefono;
        public string estado;
    }

    struct Prestamo
    {
        public string carneUsuario;
        public string codigoLibro;
        public string fechaPrestamo;
        public string fechaDevolucion;
        public string estado;
    }

    class Program
    {
        static Libro[] libros = new Libro[10];
        static Usuario[] usuarios = new Usuario[5];
        static Prestamo[] prestamos = new Prestamo[10];

        static int contadorLibros = 0;
        static int contadorUsuarios = 0;
        static int contadorPrestamos = 0;

        static int[,] matrizEstadisticas = new int[3, 3];

        static void Main(string[] args)
        {
            CrearCarpeta();
            CargarLibros();
            CargarUsuarios();
            CargarPrestamos();

            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("===== SISTEMA DE BIBLIOTECA UNIVERSITARIA =====");
                Console.WriteLine("1. Gestion de Libros");
                Console.WriteLine("2. Gestion de Usuarios");
                Console.WriteLine("3. Gestion de Prestamos");
                Console.WriteLine("4. Reportes");
                Console.WriteLine("5. Guardar Datos");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opcion: ");

                while (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Ingrese un numero valido");
                }

                switch (opcion)
                {
                    case 1:
                        MenuLibros();
                        break;

                    case 2:
                        MenuUsuarios();
                        break;

                    case 3:
                        MenuPrestamos();
                        break;

                    case 4:
                        MenuReportes();
                        break;

                    case 5:
                        GuardarLibros();
                        GuardarUsuarios();
                        GuardarPrestamos();
                        Console.WriteLine("Datos guardados correctamente");
                        break;

                    case 6:
                        GuardarLibros();
                        GuardarUsuarios();
                        GuardarPrestamos();
                        Console.WriteLine("Sistema finalizado");
                        break;

                    default:
                        Console.WriteLine("Opcion invalida");
                        break;
                }

                Console.ReadKey();

            } while (opcion != 6);
        }

        static void CrearCarpeta()
        {
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }
        }

        // ==================== MENU LIBROS ====================

        static void MenuLibros()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("===== GESTION DE LIBROS =====");
                Console.WriteLine("1. Registrar libro");
                Console.WriteLine("2. Buscar libro");
                Console.WriteLine("3. Mostrar libros");
                Console.WriteLine("4. Eliminar libro");
                Console.WriteLine("5. Regresar");
                Console.Write("Seleccione una opcion: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        RegistrarLibro();
                        break;

                    case 2:
                        BuscarLibro();
                        break;

                    case 3:
                        MostrarLibros();
                        break;

                    case 4:
                        EliminarLibro();
                        break;
                }

                Console.ReadKey();

            } while (opcion != 5);
        }

        static void RegistrarLibro()
        {
            Console.Clear();

            if (contadorLibros >= libros.Length)
            {
                Console.WriteLine("No hay espacio disponible");
                return;
            }

            Libro nuevo = new Libro();

            do
            {
                Console.Write("Codigo del libro: ");
                nuevo.codigo = Console.ReadLine();

            } while (nuevo.codigo.Length != 8 || !nuevo.codigo.StartsWith("LIB"));

            bool repetido = false;

            for (int i = 0; i < contadorLibros; i++)
            {
                if (libros[i].codigo == nuevo.codigo)
                {
                    repetido = true;
                }
            }

            if (repetido)
            {
                Console.WriteLine("Codigo ya registrado");
                return;
            }

            do
            {
                Console.Write("Titulo: ");
                nuevo.titulo = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(nuevo.titulo));

            do
            {
                Console.Write("Autor: ");
                nuevo.autor = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(nuevo.autor));

            do
            {
                Console.Write("Editorial: ");
                nuevo.editorial = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(nuevo.editorial));

            do
            {
                Console.Write("Año: ");

            } while (!int.TryParse(Console.ReadLine(), out nuevo.anio) || nuevo.anio < 1900 || nuevo.anio > DateTime.Now.Year);

            do
            {
                Console.Write("Categoria: ");
                nuevo.categoria = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(nuevo.categoria));

            do
            {
                Console.Write("Cantidad: ");

            } while (!int.TryParse(Console.ReadLine(), out nuevo.cantidad) || nuevo.cantidad < 0);

            libros[contadorLibros] = nuevo;
            contadorLibros++;

            Console.WriteLine("Libro registrado correctamente");
        }

        static void BuscarLibro()
        {
            Console.Clear();

            Console.Write("Ingrese codigo del libro: ");
            string codigo = Console.ReadLine();

            bool encontrado = false;

            for (int i = 0; i < contadorLibros; i++)
            {
                if (libros[i].codigo == codigo)
                {
                    Console.WriteLine("Libro encontrado");
                    Console.WriteLine("Titulo: " + libros[i].titulo);
                    Console.WriteLine("Autor: " + libros[i].autor);
                    Console.WriteLine("Cantidad: " + libros[i].cantidad);

                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Libro no encontrado");
            }
        }

        static void MostrarLibros()
        {
            Console.Clear();

            if (contadorLibros == 0)
            {
                Console.WriteLine("No existen libros registrados");
                return;
            }

            for (int i = 0; i < contadorLibros; i++)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("Codigo: " + libros[i].codigo);
                Console.WriteLine("Titulo: " + libros[i].titulo);
                Console.WriteLine("Autor: " + libros[i].autor);
                Console.WriteLine("Editorial: " + libros[i].editorial);
                Console.WriteLine("Año: " + libros[i].anio);
                Console.WriteLine("Categoria: " + libros[i].categoria);
                Console.WriteLine("Cantidad: " + libros[i].cantidad);
            }
        }

        static void EliminarLibro()
        {
            Console.Clear();

            Console.Write("Ingrese codigo del libro: ");
            string codigo = Console.ReadLine();

            bool eliminado = false;

            for (int i = 0; i < contadorLibros; i++)
            {
                if (libros[i].codigo == codigo)
                {
                    for (int j = i; j < contadorLibros - 1; j++)
                    {
                        libros[j] = libros[j + 1];
                    }

                    contadorLibros--;

                    eliminado = true;

                    Console.WriteLine("Libro eliminado");
                }
            }

            if (!eliminado)
            {
                Console.WriteLine("Libro no encontrado");
            }
        }

        // ==================== MENU USUARIOS ====================

        static void MenuUsuarios()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("===== GESTION DE USUARIOS =====");
                Console.WriteLine("1. Registrar usuario");
                Console.WriteLine("2. Buscar usuario");
                Console.WriteLine("3. Mostrar usuarios");
                Console.WriteLine("4. Regresar");

                Console.Write("Seleccione una opcion: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        RegistrarUsuario();
                        break;

                    case 2:
                        BuscarUsuario();
                        break;

                    case 3:
                        MostrarUsuarios();
                        break;
                }

                Console.ReadKey();

            } while (opcion != 4);
        }

        static void RegistrarUsuario()
        {
            Console.Clear();

            if (contadorUsuarios >= usuarios.Length)
            {
                Console.WriteLine("No hay espacio disponible");
                return;
            }

            Usuario nuevo = new Usuario();

            do
            {
                Console.Write("Carnet: ");
                nuevo.carne = Console.ReadLine();

            } while (nuevo.carne.Length != 8 || !long.TryParse(nuevo.carne, out _));

            do
            {
                Console.Write("Nombre: ");
                nuevo.nombre = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(nuevo.nombre));

            do
            {
                Console.Write("Carrera: ");
                nuevo.carrera = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(nuevo.carrera));

            do
            {
                Console.Write("Correo: ");
                nuevo.correo = Console.ReadLine();

            } while (!nuevo.correo.Contains("@") || !nuevo.correo.Contains("."));

            do
            {
                Console.Write("Telefono: ");
                nuevo.telefono = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(nuevo.telefono));

            do
            {
                Console.Write("Estado (Activo/Inactivo): ");
                nuevo.estado = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(nuevo.estado));

            usuarios[contadorUsuarios] = nuevo;
            contadorUsuarios++;

            Console.WriteLine("Usuario registrado correctamente");
        }

        static void BuscarUsuario()
        {
            Console.Clear();

            Console.Write("Ingrese nombre o carnet: ");
            string busqueda = Console.ReadLine().ToLower();

            bool encontrado = false;

            for (int i = 0; i < contadorUsuarios; i++)
            {
                if (usuarios[i].carne == busqueda || usuarios[i].nombre.ToLower().Contains(busqueda))
                {
                    Console.WriteLine("Usuario encontrado");
                    Console.WriteLine("Nombre: " + usuarios[i].nombre);
                    Console.WriteLine("Carrera: " + usuarios[i].carrera);
                    Console.WriteLine("Correo: " + usuarios[i].correo);

                    encontrado = true;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Usuario no encontrado");
            }
        }

        static void MostrarUsuarios()
        {
            Console.Clear();

            for (int i = 0; i < contadorUsuarios; i++)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("Carnet: " + usuarios[i].carne);
                Console.WriteLine("Nombre: " + usuarios[i].nombre);
                Console.WriteLine("Carrera: " + usuarios[i].carrera);
                Console.WriteLine("Correo: " + usuarios[i].correo);
                Console.WriteLine("Telefono: " + usuarios[i].telefono);
                Console.WriteLine("Estado: " + usuarios[i].estado);
            }
        }

        // ==================== MENU PRESTAMOS ====================

        static void MenuPrestamos()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("===== GESTION DE PRESTAMOS =====");
                Console.WriteLine("1. Registrar prestamo");
                Console.WriteLine("2. Registrar devolucion");
                Console.WriteLine("3. Mostrar prestamos");
                Console.WriteLine("4. Regresar");

                Console.Write("Seleccione una opcion: ");

                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        RegistrarPrestamo();
                        break;

                    case 2:
                        RegistrarDevolucion();
                        break;

                    case 3:
                        MostrarPrestamos();
                        break;
                }

                Console.ReadKey();

            } while (opcion != 4);
        }

        static void RegistrarPrestamo()
        {
            Console.Clear();

            if (contadorPrestamos >= prestamos.Length)
            {
                Console.WriteLine("No hay espacio");
                return;
            }

            Prestamo nuevo = new Prestamo();

            Console.Write("Carnet usuario: ");
            nuevo.carneUsuario = Console.ReadLine();

            Console.Write("Codigo libro: ");
            nuevo.codigoLibro = Console.ReadLine();

            bool disponible = false;

            for (int i = 0; i < contadorLibros; i++)
            {
                if (libros[i].codigo == nuevo.codigoLibro)
                {
                    if (libros[i].cantidad > 0)
                    {
                        libros[i].cantidad--;
                        disponible = true;
                    }
                }
            }

            if (!disponible)
            {
                Console.WriteLine("Libro no disponible");
                return;
            }

            do
            {
                Console.Write("Fecha prestamo (dd/mm/yyyy): ");
                nuevo.fechaPrestamo = Console.ReadLine();

            } while (nuevo.fechaPrestamo.Length != 10);

            do
            {
                Console.Write("Fecha devolucion (dd/mm/yyyy): ");
                nuevo.fechaDevolucion = Console.ReadLine();

            } while (nuevo.fechaDevolucion.Length != 10);

            nuevo.estado = "Activo";

            prestamos[contadorPrestamos] = nuevo;
            contadorPrestamos++;

            Console.WriteLine("Prestamo registrado correctamente");
        }

        static void RegistrarDevolucion()
        {
            Console.Clear();

            Console.Write("Codigo libro: ");
            string codigo = Console.ReadLine();

            bool encontrado = false;

            for (int i = 0; i < contadorPrestamos; i++)
            {
                if (prestamos[i].codigoLibro == codigo && prestamos[i].estado == "Activo")
                {
                    prestamos[i].estado = "Devuelto";

                    for (int j = 0; j < contadorLibros; j++)
                    {
                        if (libros[j].codigo == codigo)
                        {
                            libros[j].cantidad++;
                        }
                    }

                    encontrado = true;

                    Console.WriteLine("Devolucion registrada");
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Prestamo no encontrado");
            }
        }

        static void MostrarPrestamos()
        {
            Console.Clear();

            for (int i = 0; i < contadorPrestamos; i++)
            {
                Console.WriteLine("-----------------------");
                Console.WriteLine("Carnet: " + prestamos[i].carneUsuario);
                Console.WriteLine("Libro: " + prestamos[i].codigoLibro);
                Console.WriteLine("Prestamo: " + prestamos[i].fechaPrestamo);
                Console.WriteLine("Devolucion: " + prestamos[i].fechaDevolucion);
                Console.WriteLine("Estado: " + prestamos[i].estado);
            }
        }

        // ==================== REPORTES ====================

        static void MenuReportes()
        {
            Console.Clear();

            matrizEstadisticas[0, 0] = contadorLibros;
            matrizEstadisticas[1, 1] = contadorUsuarios;
            matrizEstadisticas[2, 2] = contadorPrestamos;

            Console.WriteLine("===== REPORTES =====");

            Console.WriteLine("Total libros: " + matrizEstadisticas[0, 0]);
            Console.WriteLine("Total usuarios: " + matrizEstadisticas[1, 1]);
            Console.WriteLine("Total prestamos: " + matrizEstadisticas[2, 2]);

            ExportarReporte();
        }

        static void ExportarReporte()
        {
            StreamWriter writer = new StreamWriter("Data/reporte.txt");

            writer.WriteLine("===== REPORTE GENERAL =====");
            writer.WriteLine("Total libros: " + contadorLibros);
            writer.WriteLine("Total usuarios: " + contadorUsuarios);
            writer.WriteLine("Total prestamos: " + contadorPrestamos);

            writer.Close();

            Console.WriteLine("Reporte exportado");
        }

        // ==================== ARCHIVOS ====================

        static void GuardarLibros()
        {
            StreamWriter writer = new StreamWriter("Data/libros.csv");

            for (int i = 0; i < contadorLibros; i++)
            {
                writer.WriteLine(
                    libros[i].codigo + "," +
                    libros[i].titulo + "," +
                    libros[i].autor + "," +
                    libros[i].editorial + "," +
                    libros[i].anio + "," +
                    libros[i].categoria + "," +
                    libros[i].cantidad
                );
            }

            writer.Close();
        }

        static void CargarLibros()
        {
            if (File.Exists("Data/libros.csv"))
            {
                StreamReader reader = new StreamReader("Data/libros.csv");

                string linea;

                while ((linea = reader.ReadLine()) != null)
                {
                    string[] datos = linea.Split(',');

                    Libro libro = new Libro();

                    libro.codigo = datos[0];
                    libro.titulo = datos[1];
                    libro.autor = datos[2];
                    libro.editorial = datos[3];
                    libro.anio = int.Parse(datos[4]);
                    libro.categoria = datos[5];
                    libro.cantidad = int.Parse(datos[6]);

                    libros[contadorLibros] = libro;
                    contadorLibros++;
                }

                reader.Close();
            }
        }

        static void GuardarUsuarios()
        {
            StreamWriter writer = new StreamWriter("Data/usuarios.txt");

            for (int i = 0; i < contadorUsuarios; i++)
            {
                writer.WriteLine(
                    usuarios[i].carne + ";" +
                    usuarios[i].nombre + ";" +
                    usuarios[i].carrera + ";" +
                    usuarios[i].correo + ";" +
                    usuarios[i].telefono + ";" +
                    usuarios[i].estado
                );
            }

            writer.Close();
        }

        static void CargarUsuarios()
        {
            if (File.Exists("Data/usuarios.txt"))
            {
                StreamReader reader = new StreamReader("Data/usuarios.txt");

                string linea;

                while ((linea = reader.ReadLine()) != null)
                {
                    string[] datos = linea.Split(';');

                    Usuario usuario = new Usuario();

                    usuario.carne = datos[0];
                    usuario.nombre = datos[1];
                    usuario.carrera = datos[2];
                    usuario.correo = datos[3];
                    usuario.telefono = datos[4];
                    usuario.estado = datos[5];

                    usuarios[contadorUsuarios] = usuario;
                    contadorUsuarios++;
                }

                reader.Close();
            }
        }

        static void GuardarPrestamos()
        {
            StreamWriter writer = new StreamWriter("Data/prestamos.txt");

            for (int i = 0; i < contadorPrestamos; i++)
            {
                writer.WriteLine(
                    prestamos[i].carneUsuario + ";" +
                    prestamos[i].codigoLibro + ";" +
                    prestamos[i].fechaPrestamo + ";" +
                    prestamos[i].fechaDevolucion + ";" +
                    prestamos[i].estado
                );
            }

            writer.Close();
        }

        static void CargarPrestamos()
        {
            if (File.Exists("Data/prestamos.txt"))
            {
                StreamReader reader = new StreamReader("Data/prestamos.txt");

                string linea;

                while ((linea = reader.ReadLine()) != null)
                {
                    string[] datos = linea.Split(';');

                    Prestamo prestamo = new Prestamo();

                    prestamo.carneUsuario = datos[0];
                    prestamo.codigoLibro = datos[1];
                    prestamo.fechaPrestamo = datos[2];
                    prestamo.fechaDevolucion = datos[3];
                    prestamo.estado = datos[4];

                    prestamos[contadorPrestamos] = prestamo;
                    contadorPrestamos++;
                }

                reader.Close();
            }
        }
    }
}
