using administracion.Handler;
using administracion.Models;
using System.Diagnostics;
using System.Security.Cryptography;

namespace administracion
{
    internal class Program
    {
        public delegate T Ingreso<T>(string obj);
        static void Main(string[] args)
        {
            // delegados para verificar tipos de datos
            Ingreso<byte> verificadorByte = new Ingreso<byte>(IsByte);
            Ingreso<int> verificadorInt = new Ingreso<int>(IsInt);
            Ingreso<long> verificadorLong = new Ingreso<long>(IsLong);
            Ingreso<string> verificadorString = new Ingreso<string>(NonNullable);

            // handlers
            LibroHandler libroHandler = new LibroHandler();
            PrestamoHandler prestamoHandler = new PrestamoHandler();
            StaffHandler staffHandler = new StaffHandler();
            UsuarioHandler usuarioHandler = new UsuarioHandler();

            // Pruebas
            List<Usuario> usuarios = new List<Usuario>()
            {
                new Usuario(30001, "Emiliano", "Jimenez", 17, "emilianokaleb@gmail.com",
                5644383186),
                new Usuario(30002, "Andrea", "Gutierrez", 18, "andgu@gmail.com",
                5644383186),
                new Usuario(30003, "Ignacio", "Espinoza", 23, "igni@gmail.com",
                5644383186),
                new Usuario(30004, "Laura", "Flores", 22, "lau.flor@gmail.com",
                5644383186),
                new Usuario(30005, "Roberto", "Sanchez", 21, "sanrober@gmail.com",
                5644383186)

            };
            List<Staff> staffs = new List<Staff>()
            {
                new Staff(40001, "Ingrid", "Nunez", "MATUTINO"),
                new Staff(40002, "Ingrid", "Nunez", "VESPERTINO"),
                new Staff(40003, "Ingrid", "Nunez", "NOCTURNO")

            };
            List<Libro> libros = new List<Libro>()
            {
                new Libro(70001, "Crimen y Castigo", "Fiodor", "Dostoyevski", "Porrua", "Novela",
                10),
                new Libro(70002, "Frankenstein", "Mary", "Shelley", "Austral", "Novela",
                10),
                new Libro(70003, "Calculo diferencial", "Samuel", "Fuenlabrada de la Vega",
                "McGraw-Hill", "Ciencias Fisico Matematicas",
                30),
                new Libro(70004, "Dibujo constructivo", "Jany Edna", "Castellanos Lopez",
                "McGraw-Hill", "Humanidades y Artes",
                25),
                new Libro(70005, "Conceptos de administracion estrategica", "Fred", "David",
                "Pearson", "Ciencias Sociales",
                15),
                new Libro(70006, "Biologia", "Gama", "", "Pearson", "Ciencias Biologicas Quimicas" +
                " y de la Salud",
                40)
            };

            byte opcion;
            do
            {
                // titulo
                Console.WriteLine("========= BIBLIOTECA ===========\n\n");
                // opciones
                Console.Write("%%%%%%%% ELIGE UNA OPCION %%%%%%%\n" +
                    "\t(0) SALIR\n\n" +
                    "\t(1) Control de Usuarios\n" +
                    "\t(2) Control de Libros\n" +
                    "\t(3) Control de Personal\n" +
                    "\t(4) Control de Prestamos\n\nOpcion: ");

                opcion = verificadorByte(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Console.Write("\n\n%%%%%%%% OPCIONES USUARIO %%%%%%%\n" +
                            "\t(1) Crear nuevo Usuario\n" +
                            "\t(2) Mostrar Usuario\n" +
                            "\t(3) Actualizar Usuario\n" +
                            "\t(4) Eliminar Usuario\n" +
                            "\n\t (11) anterior pagina \n\nOpcion: ");
                        Usuario usuario = new Usuario();
                        List<Usuario> usuarioss = new List<Usuario>();
                        switch (verificadorByte(Console.ReadLine()))
                        {
                            case 1:
                                
                                Console.WriteLine("\n\n---- Crear Usuario ----\n" +
                                    "Escribe los siguientes datos:");
                                Console.Write("Id: ");
                                usuario.Id = verificadorLong(Console.ReadLine());
                                Console.Write("\nNombre: ");
                                usuario.Nombre = verificadorString(Console.ReadLine());
                                Console.Write("\nApellido: ");
                                usuario.Apellido = verificadorString(Console.ReadLine());
                                Console.Write("\nEdad: ");
                                usuario.Edad = verificadorInt(Console.ReadLine());
                                Console.Write("\nMail: ");
                                verificadorString -= NonNullable;
                                verificadorString += IsMail;
                                usuario.Mail = verificadorString(Console.ReadLine());
                                Console.Write("\nTelefono: ");
                                usuario.Telefono = verificadorLong(Console.ReadLine());

                                usuarioHandler.Create(usuario);
                                usuario = null;
                                break;
                            case 2:
                                Console.Write("\n\n---- Mostrar Usuario ----\n" +
                                    "\n1) Ver un usuario por su id" +
                                    "\n2) Ver todos los usuarios \n\nOpcion: ");
                                if(verificadorByte(Console.ReadLine()) == 1)
                                {
                                    Console.Write("\n@@ opcion 1 @@\nUsuario: ");
                                    usuarioss.Add(usuarioHandler.Get(verificadorLong(Console.ReadLine())));
                                    usuarioHandler.ImprimirUsuarios(usuarioss);
                                    usuarioss.Clear();
                                }
                                else if(verificadorByte(Console.ReadLine()) == 2)
                                {
                                    Console.Write("\n@@ opcion 2 @@\n");
                                    usuarioss = usuarioHandler.GetAll();
                                    usuarioHandler.ImprimirUsuarios(usuarioss);
                                    usuarioss.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("\n&&&&&&&&& NO INGRESASTE OPCION " +
                                        "CORRECTA &&&&&&&&&\n");
                                    continue;
                                }
                                break;
                            case 3:
                                Console.Write("\n\n---- Actualizar Usuario ----\n" +
                                    "Escribe el id del usuario que deseas actualizar:");
                                usuario = usuarioHandler.Get(verificadorLong(Console.ReadLine()));
                                do
                                {
                                    Console.Write("\n\nAhora escribe el atributo que quieres cambiar:" +
                                    "\nNOMBRE" +
                                    "\nAPELLIDO" +
                                    "\nEDAD" +
                                    "\nMAIL" +
                                    "\nTELEFONO");

                                    
                                    switch (verificadorString(Console.ReadLine()))
                                    {
                                        case "NOMBRE":
                                            Console.Write("\ningresa nuevo valor de Nombre: ");
                                            verificadorString = new Ingreso<string>(NonNullable);
                                            usuario.Nombre = verificadorString(Console.ReadLine());
                                            break;
                                        case "APELLIDO":
                                            Console.Write("\ningresa nuevo valor de Apellido: ");
                                            verificadorString = new Ingreso<string>(NonNullable);
                                            usuario.Apellido = verificadorString(Console.ReadLine());
                                            break;
                                        case "EDAD":
                                            Console.Write("\ningresa nuevo valor de Edad: ");
                                            usuario.Edad = verificadorInt(Console.ReadLine());
                                            break;
                                        case "MAIL":
                                            Console.Write("\ningresa nuevo valor de Mail: ");
                                            verificadorString -= NonNullable;
                                            verificadorString += IsMail;
                                            usuario.Mail = verificadorString(Console.ReadLine());
                                            break;
                                        case "TELFONO":
                                            Console.Write("\ningresa nuevo valor de Telefono: ");
                                            usuario.Telefono = verificadorLong(Console.ReadLine());
                                            break;
                                        default:
                                            continue;
                                    }
                                    Console.WriteLine("\nQuieres cambiar otro atributo? si(1) no(0)");
                                } while (verificadorByte(Console.ReadLine()) != 0);
                                usuarioHandler.Update(usuario);
                                usuario = null;
                                break;
                            case 4:
                                Console.Write("\n\n---- Eliminar Usuario ----\n" +
                                    "Escribe el id del usuario:");
                                usuarioHandler.Delete(verificadorLong(Console.ReadLine()));
                                break;
                            case 11:
                                continue;
                            default:
                                Console.WriteLine("\n&&&&&&&&& NO INGRESASTE OPCION CORRECTA " +
                                    "&&&&&&&&&\n");
                                continue;

                        }
                        break;



                    case 2:
                        Console.Write("%%%%%%%% OPCIONES LIBRO %%%%%%%\n" +
                            "\t(1) Crear nuevo Libro\n" +
                            "\t(2) Mostrar Libro\n" +
                            "\t(3) Acutalizar Libro\n" +
                            "\t(4) Eliminar Libro\n" +
                            "\n\t (11) anterior pagina \n\nOpcion: ");

                        Libro libro = new Libro();
                        List<Libro> libross = new List<Libro>();

                        switch (verificadorByte(Console.ReadLine()))
                        {
                            case 1:

                                Console.WriteLine("\n\n---- Crear Libro ----\n" +
                                    "Escribe los siguientes datos:");
                                Console.Write("Id: ");
                                libro.Id = verificadorLong(Console.ReadLine());
                                Console.Write("\nTitulo: ");
                                verificadorString = new Ingreso<string>(NonNullable);
                                libro.Titulo = verificadorString(Console.ReadLine());
                                Console.Write("\nNombre del autor: ");
                                libro.NombreAutor= verificadorString(Console.ReadLine());
                                Console.Write("\nApellido del autor: ");
                                libro.ApellidoAutor = verificadorString(Console.ReadLine());
                                Console.Write("\nEditorial: ");
                                libro.Editorial = verificadorString(Console.ReadLine());
                                Console.Write("\nSeccion: ");
                                libro.Seccion = verificadorString(Console.ReadLine());
                                Console.Write("\nCantidad: ");
                                libro.Cantidad = verificadorInt(Console.ReadLine());

                                libroHandler.Create(libro);
                                libro = null;
                                break;
                            case 2:
                                Console.Write("\n\n---- Mostrar Libro ----\n" +
                                    "\n1) Ver un libro por su id" +
                                    "\n2) Ver todos los libros \n\nOpcion: ");
                                if (verificadorByte(Console.ReadLine()) == 1)
                                {
                                    Console.Write("\n@@ opcion 1 @@\nLibro: ");
                                    libross.Add(libroHandler.Get(verificadorLong(Console.ReadLine())));
                                    libroHandler.ImprimirLibros(libross);
                                    libross.Clear();
                                }
                                else if (verificadorByte(Console.ReadLine()) == 2)
                                {
                                    Console.Write("\n@@ opcion 2 @@\n");
                                    libross = libroHandler.GetAll();
                                    libroHandler.ImprimirLibros(libross);
                                    libross.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("\n&&&&&&&&& NO INGRESASTE OPCION " +
                                        "CORRECTA &&&&&&&&&\n");
                                    continue;
                                }
                                break;
                            case 3:
                                Console.Write("\n\n---- Actualizar Libro ----\n" +
                                    "Escribe el id del libro que deseas actualizar:");
                                libro = libroHandler.Get(verificadorLong(Console.ReadLine()));
                                do
                                {
                                    Console.Write("\n\nAhora escribe el atributo que quieres cambiar:" +
                                    "\nTITULO" +
                                    "\nNOMBRE AUTOR" +
                                    "\nAPELLIDO AUTOR" +
                                    "\nEDITORIAL" +
                                    "\nSECCION" +
                                    "\nCANTIDAD");

                                    switch (verificadorString(Console.ReadLine()))
                                    {
                                        case "TITULO":
                                            Console.Write("\ningresa nuevo valor de Titulo: ");
                                            verificadorString = new Ingreso<string>(NonNullable);
                                            libro.Titulo = verificadorString(Console.ReadLine());
                                            break;
                                        case "NOMBRE AUTOR":
                                            Console.Write("\ningresa nuevo valor de Nombre del autor: ");
                                            libro.ApellidoAutor = verificadorString(Console.ReadLine());
                                            break;
                                        case "APELLIDO AUTOR":
                                            Console.Write("\ningresa nuevo valor de Apellido del autor: ");
                                            libro.ApellidoAutor = verificadorString(Console.ReadLine());
                                            break;
                                        case "EDITORIAL":
                                            Console.Write("\ningresa nuevo valor de Editorial: ");
                                            libro.Editorial = verificadorString(Console.ReadLine());
                                            break;
                                        case "SECCION":
                                            Console.Write("\ningresa nuevo valor de Seccion: ");
                                            libro.Seccion = verificadorString(Console.ReadLine());
                                            break;
                                        case "CANTIDAD":
                                            Console.Write("\ningresa nuevo valor de Telefono: ");
                                            libro.Cantidad = verificadorInt(Console.ReadLine());
                                            break;
                                        default:
                                            continue;
                                    }
                                    Console.WriteLine("\nQuieres cambiar otro atributo? si(1) no(0)");
                                } while (verificadorByte(Console.ReadLine()) != 0);
                                libroHandler.Update(libro);
                                libro = null;
                                break;
                            case 4:
                                Console.Write("\n\n---- Eliminar Libro ----\n" +
                                    "Escribe el id del libro:");
                                libroHandler.Delete(verificadorLong(Console.ReadLine()));
                                break;
                            case 11:
                                continue;
                            default:
                                Console.WriteLine("\n&&&&&&&&& NO INGRESASTE OPCION CORRECTA " +
                                    "&&&&&&&&&\n");
                                continue;

                        }
                        break;
                    case 3:
                        Console.Write("%%%%%%%% OPCIONES PERSONAL %%%%%%%\n" +
                            "\t(1) Crear nuevo Bibliotecario(a)\n" +
                            "\t(2) Mostrar Bibliotecario(a)\n" +
                            "\t(3) Acutalizar Bibliotecario(a)\n" +
                            "\t(4) Eliminar Bibliotecario(a)\n" +
                            "\n\t (11) anterior pagina \n\nOpcion: ");

                        Staff staff = new Staff();
                        List<Staff> staffss = new List<Staff>();

                        switch (verificadorByte(Console.ReadLine()))
                        {
                            case 1:

                                Console.WriteLine("\n\n---- Crear Staff ----\n" +
                                    "Escribe los siguientes datos:");
                                Console.Write("Id: ");
                                staff.Id = verificadorLong(Console.ReadLine());
                                Console.Write("\nNombre: ");
                                verificadorString = new Ingreso<string>(NonNullable);
                                staff.Nombre = verificadorString(Console.ReadLine());
                                Console.Write("\nApellido: ");
                                staff.Apellido = verificadorString(Console.ReadLine());
                                Console.Write("\nTurno: ");
                                verificadorString = new Ingreso<string>(IsTurno);
                                staff.Turno = verificadorString(Console.ReadLine());

                                staffHandler.Create(staff);
                                staff = null;
                                break;
                            case 2:
                                Console.Write("\n\n---- Mostrar Staff ----\n" +
                                    "\n1) Ver un staff por su id" +
                                    "\n2) Ver todos los staffs \n\nOpcion: ");
                                if (verificadorByte(Console.ReadLine()) == 1)
                                {
                                    Console.Write("\n@@ opcion 1 @@\nStaff: ");
                                    staffss.Add(staffHandler.Get(verificadorLong(Console.ReadLine())));
                                    staffHandler.ImprimirStaffs(staffss);
                                    staffss.Clear();
                                }
                                else if (verificadorByte(Console.ReadLine()) == 2)
                                {
                                    Console.Write("\n@@ opcion 2 @@\n");
                                    staffss = staffHandler.GetAll();
                                    staffHandler.ImprimirStaffs(staffss);
                                    staffss.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("\n&&&&&&&&& NO INGRESASTE OPCION " +
                                        "CORRECTA &&&&&&&&&\n");
                                    continue;
                                }
                                break;
                            case 3:
                                Console.Write("\n\n---- Actualizar Staff ----\n" +
                                    "Escribe el id del staff que deseas actualizar:");
                                staff = staffHandler.Get(verificadorLong(Console.ReadLine()));
                                do
                                {
                                    Console.Write("\n\nAhora escribe el atributo que quieres cambiar:" +
                                    "\nNOMBRE" +
                                    "\nAPELLIDO" +
                                    "\nTURNO");

                                    switch (verificadorString(Console.ReadLine()))
                                    {
                                        case "NOMBRE":
                                            Console.Write("\ningresa nuevo valor de Nombre: ");
                                            verificadorString = new Ingreso<string>(NonNullable);
                                            staff.Nombre = verificadorString(Console.ReadLine());
                                            break;
                                        case "APELLIDO":
                                            Console.Write("\ningresa nuevo valor de Apellido: ");
                                            staff.Apellido = verificadorString(Console.ReadLine());
                                            break;
                                        case "TURNO":
                                            Console.Write("\ningresa nuevo valor de Turno: ");
                                            verificadorString = new Ingreso<string>(IsTurno);
                                            staff.Turno = verificadorString(Console.ReadLine());
                                            break;
                                        default:
                                            continue;
                                    }
                                    Console.WriteLine("\nQuieres cambiar otro atributo? si(1) no(0)");
                                } while (verificadorByte(Console.ReadLine()) != 0);
                                staffHandler.Update(staff);
                                staff = null;
                                break;
                            case 4:
                                Console.Write("\n\n---- Eliminar Staff ----\n" +
                                    "Escribe el id del Staff:");
                                staffHandler.Delete(verificadorLong(Console.ReadLine()));
                                break;
                            case 11:
                                continue;
                            default:
                                Console.WriteLine("\n&&&&&&&&& NO INGRESASTE OPCION CORRECTA " +
                                    "&&&&&&&&&\n");
                                continue;

                        }
                        break;



                    case 4:
                        Console.Write("%%%%%%%% OPCIONES PRESTAMO %%%%%%%\n" +
                            "\t(1) Ingresar o crear prestamo\n" +
                            "\t(2) Consultar prestamo\n" +
                            "\t(3) Acutalizar Prestamo\n" +
                            "\n\t (11) anterior pagina \n\nOpcion: ");

                        Prestamo prestamo;
                        List<Prestamo> prestamos = new List<Prestamo>();

                        int dia1, dia2, mes1, mes2, anio1, anio2;

                        switch (verificadorByte(Console.ReadLine()))
                        {
                            case 1:

                                Console.WriteLine("\n\n---- Crear Prestamo ----\n" +
                                    "Escribe los siguientes datos:");
                                do
                                {
                                    Console.Write("Id de Usuario: ");
                                    usuario = usuarioHandler.Get(verificadorLong(Console.ReadLine()));
                                    Console.Write("Id de Libro: ");
                                    libro = libroHandler.Get(verificadorLong(Console.ReadLine()));
                                    Console.Write("Id de Staff encargado: ");
                                    staff = staffHandler.Get(verificadorLong(Console.ReadLine()));
                                    
                                } while (usuario == null || staff == null || libro == null);

                                do
                                {

                                    Console.Write("\nFecha Prestamo: \nDia: ");
                                    dia1 = verificadorInt(Console.ReadLine());
                                    Console.Write("\nMes: ");
                                    mes1 = verificadorInt(Console.ReadLine());
                                    Console.Write("\nAnio: ");
                                    anio1 = verificadorInt(Console.ReadLine());


                                    Console.Write("\nFecha Devolucion: \nDia");
                                    dia2 = verificadorInt(Console.ReadLine());
                                    Console.Write("\nMes: ");
                                    mes2 = verificadorInt(Console.ReadLine());
                                    Console.Write("\nAnio: ");
                                    anio2 = verificadorInt(Console.ReadLine());

                                    Console.WriteLine(!(dia2 > dia1 || mes2 > mes1 || anio2 > anio1) ?
                                        "%%%%%%%%%%%% La fecha de devolucion debe ser posterior " +
                                        "al prestamo %%%%%%%%%%%%" :
                                        "");
                                } while (!(dia2 > dia1 || mes2 > mes1 || anio2 > anio1));

                                string fechaPrestamo = "" + mes1 + "/" + dia1 + "/" + anio1;
                                string fechaDevolucion = "" + mes2 + "/" + dia2 + "/" + anio2;

                                prestamo = new Prestamo(usuario, libro, staff, fechaPrestamo,
                                    fechaDevolucion, "PRESTAMO");

                                prestamoHandler.Create(prestamo);
                                prestamo = null;
                                usuario = null;
                                libro = null;
                                staff = null;
                                break;
                            case 2:
                                Console.Write("\n\n---- Mostrar Prestamo ----\n" +
                                    "\n1) Ver un prestamo por su id" +
                                    "\n2) Ver todos los prestamos \n\nOpcion: ");
                                if (verificadorByte(Console.ReadLine()) == 1)
                                {
                                    Console.Write("\n@@ opcion 1 @@\nPrestamo: ");
                                    prestamos.Add(prestamoHandler.Get(verificadorLong(Console.ReadLine())));
                                    prestamoHandler.ImprimirPrestamos(prestamos);
                                    prestamos.Clear();
                                }
                                else if (verificadorByte(Console.ReadLine()) == 2)
                                {
                                    Console.Write("\n@@ opcion 2 @@\n");
                                    prestamos = prestamoHandler.GetAll();
                                    prestamoHandler.ImprimirPrestamos(prestamos);
                                    prestamos.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("\n&&&&&&&&& NO INGRESASTE OPCION " +
                                        "CORRECTA &&&&&&&&&\n");
                                    continue;
                                }
                                break;
                            case 3:
                                Console.Write("\n\n---- Actualizar Prestamo (solo el estatus) ----\n" +
                                    "Escribe el id del usuario asociado que deseas actualizar:");
                                prestamos = prestamoHandler.GetPrestamos(verificadorLong(Console.ReadLine()));
                                prestamoHandler.ImprimirPrestamos(prestamos);

                                Console.Write("\nAhora ingresa el id del libro asociado" +
                                    "al prestamo \nId del libro: ");
                                long idLibro = verificadorLong(Console.ReadLine());
                                prestamo = null;
                                foreach(Prestamo prestamo1 in prestamos)
                                {
                                    if (prestamo1.IdLibro == idLibro)
                                        prestamo = prestamo1;
                                    
                                }
                                if (prestamo != null)
                                    prestamoHandler.Update(prestamo);
                                else
                                    Console.WriteLine("\n%% no se pudo actualizar porque no se " +
                                        "encontro con ese id %%\n");
                                staff = null;
                                break;
                            case 11:
                                continue;
                            default:
                                Console.WriteLine("\n&&&&&&&&& NO INGRESASTE OPCION CORRECTA " +
                                    "&&&&&&&&&\n");
                                continue;

                        }
                        break;
                    default:
                        Console.WriteLine("\n&&&&&&&&& NO INGRESASTE OPCION CORRECTA &&&&&&&&&\n");
                        continue;
                }
            } while (opcion != 0);

        }
        public static byte IsByte(string obj)
        {
            byte num;
            bool boolean;
            boolean = byte.TryParse(obj, out num);
            while (!boolean)
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                boolean = byte.TryParse(Console.ReadLine(), out num);
                if (num <= 0)
                    boolean = false;
            }
            return num;
        }
        public static int IsInt(string obj)
        {
            int num;
            bool boolean;
            boolean = int.TryParse(obj, out num);
            while (!boolean)
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                boolean = int.TryParse(Console.ReadLine(), out num);
                if (num <= 0)
                    boolean = false;
            }
            return num;
        }
        public static long IsLong(string obj)
        {
            long num;
            bool boolean;
            boolean = long.TryParse(obj, out num);
            while (!boolean)
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                boolean = long.TryParse(Console.ReadLine(), out num);
                if (num <= 0)
                    boolean = false;
            }
            return num;
        }
        public static string NonNullable(string obj)
        {
            while (String.IsNullOrWhiteSpace(obj))
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                obj = Console.ReadLine();
            }

            return obj;
        }
        public static string IsMail(string obj)
        {
            while (!obj.Contains("@"))
            {
                Console.Write("\n---------- No ingresaste correo, debe " +
                    "tener @ ----------\n" +
                    "\nPon un correo: ");
                obj = Console.ReadLine();
            }
            return obj;
        }
        public static string IsTurno(string obj)
        {
            while (obj == "MATUTINO" || obj == "VESPERTINO" | obj == "NOCTURNO")
            {
                Console.Write("\n---------- No ingresaste bien el turno, debe " +
                    "estar en mayus----------\n" +
                    "\nPon un turno correcto (MATUTINO, VESPERTINO O NOCTURNO): ");
                obj = Console.ReadLine();
            }
            return obj;
        }
        public static bool IsFecha(string obj, out string salida)
        {
            salida = null;
            int dia = 0;
            int mes = 0;
            int anio = 0;
            int progreso = 0;
            int flag = 0;

            // verificar que sean enteros
            for (int i = 0; i < obj.Length; i++)
            {
                if (i < obj.Length - 1)
                {
                    if (obj[i + 1] == '-' || obj[i + 1] == '/')
                    {
                        if (progreso == 0)
                        {
                            if (!(int.TryParse(StringDerivado(obj, flag, i), out dia)))
                            {
                                salida = null;
                                return false;
                            }
                            flag = i + 2;
                            progreso++;
                        }
                        else if (progreso == 1)
                        {
                            if (!(int.TryParse(StringDerivado(obj, flag, i), out mes)))
                            {
                                salida = null;
                                return false;
                            }
                            flag = i + 2;
                            progreso++;

                        }

                    }
                }
            }

            if (progreso == 2)
            {
                if (!(int.TryParse(StringDerivado(obj, flag, obj.Length - 1), out anio)))
                {
                    salida = null;
                    return false;
                }
            }

            // verificar que tengan algun valor del mes, que sean formatos correctos
            if (dia < 0 || (mes < 1 || mes > 12) || anio < 2021)
            {
                salida = null;
                return false;
            }

            if (mes == 2 && dia == (int)Meses.FebreroB && !(anio % 400 == 0 ? true :
                (anio % 4 == 0 ? (anio % 100 == 0 ? false : true) : false)))
            {
                salida = null;
                return false;
            }

            switch (mes)
            {
                case 1:
                    if (dia > (int)Meses.Enero)
                        return false;
                    else
                        break;
                case 2:
                    if (dia > (int)Meses.FebreroB)
                        return false;
                    else
                        break;
                case 3:
                    if (dia > (int)Meses.Marzo)
                        return false;
                    else
                        break;
                case 4:
                    if (dia > (int)Meses.Abril)
                        return false;
                    else
                        break;
                case 5:
                    if (dia > (int)Meses.Mayo)
                        return false;
                    else
                        break;
                case 6:
                    if (dia > (int)Meses.Junio)
                        return false;
                    else
                        break;
                case 7:
                    if (dia > (int)Meses.Julio)
                        return false;
                    else
                        break;
                case 8:
                    if (dia > (int)Meses.Agosto)
                        return false;
                    else
                        break;
                case 9:
                    if (dia > (int)Meses.Septiembre)
                        return false;
                    else
                        break;
                case 10:
                    if (dia > (int)Meses.Octubre)
                        return false;
                    else
                        break;
                case 11:
                    if (dia > (int)Meses.Noviembre)
                        return false;
                    else
                        break;
                case 12:
                    if (dia > (int)Meses.Diciembre)
                        return false;
                    else
                        break;
            }

            salida = "" + dia + "/" + mes + "/" + anio;

            return true;
        }

        public static string StringDerivado(string texto, int startIndex, int endIndex)
        {
            string derivado = "";
            for (int i = startIndex; i <= endIndex; i++)
            {
                derivado += texto[i];
            }
            return derivado;
        }
        public enum Meses : int
        {
            Enero = 31,
            Febrero = 28,
            FebreroB = 29,
            Marzo = 31,
            Abril = 30,
            Mayo = 31,
            Junio = 30,
            Julio = 31,
            Agosto = 31,
            Septiembre = 30,
            Octubre = 31,
            Noviembre = 30,
            Diciembre = 31
        }
    }
}