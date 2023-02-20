using administracion.Handler;
using administracion.Models;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;

namespace administracion
{
    public class Program
    {
        public delegate T Ingreso<T>(string obj);
        public delegate void ConsultaFecha(Prestamo prestamo);
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

            ConsultaFecha consulta = new ConsultaFecha(prestamoHandler.ConsultarFecha);

            DateTime date = new DateTime();
            byte opcion, subopcion;
            string atributo;
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

                                subopcion = verificadorByte(Console.ReadLine());
                                if (subopcion == 1)
                                {
                                    Console.Write("\n@@ opcion 1 @@\nUsuario: ");
                                    usuarioss.Add(usuarioHandler.Get(verificadorLong(Console.ReadLine())));
                                    Console.WriteLine(usuarioHandler.ImprimirUsuarios(usuarioss));
                                    usuarioss.Clear();
                                }
                                else if (subopcion == 2)
                                {
                                    Console.Write("\n@@ opcion 2 @@\n");
                                    usuarioss = usuarioHandler.GetAll();
                                    Console.WriteLine(usuarioHandler.ImprimirUsuarios(usuarioss));
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
                                Console.Write("\n\n---- Actualizar Usuario ----\n");
                                do
                                {
                                    Console.Write("Escribe el id del usuario que deseas actualizar:");
                                    usuario = usuarioHandler.Get(verificadorLong(Console.ReadLine()));
                                    Console.Write(usuario == null ? "\n%% usuario no encontrado" +
                                        "con el id %%\n" : "");
                                } while (usuario == null);
                                
                                do
                                {
                                    Console.Write("\n\nAhora escribe el atributo que quieres cambiar:" +
                                    "\nNOMBRE" +
                                    "\nAPELLIDO" +
                                    "\nEDAD" +
                                    "\nMAIL" +
                                    "\nTELEFONO\n\nAtributo:");

                                    atributo = verificadorString(Console.ReadLine());
                                    switch (atributo)
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
                                            Console.WriteLine("\n%% atributo incorrecto %%");
                                            break;
                                    }
                                    Console.WriteLine("\nQuieres repetir la operacion? si(1) no(0)");
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
                                subopcion = verificadorByte(Console.ReadLine());

                                if (subopcion == 1)
                                {
                                    Console.Write("\n@@ opcion 1 @@\nLibro: ");
                                    libross.Add(libroHandler.Get(verificadorLong(Console.ReadLine())));
                                    Console.WriteLine(libroHandler.ImprimirLibros(libross));
                                    libross.Clear();
                                }
                                else if (subopcion == 2)
                                {
                                    Console.Write("\n@@ opcion 2 @@\n");
                                    libross = libroHandler.GetAll();
                                    Console.WriteLine(libroHandler.ImprimirLibros(libross));
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
                                Console.Write("\n\n---- Actualizar Libro ----\n");
                                do
                                {
                                    Console.Write("Escribe el id del libro que deseas actualizar:");
                                    libro = libroHandler.Get(verificadorLong(Console.ReadLine()));
                                    Console.WriteLine(libro == null ? "\n%% libro no encontrado" +
                                        "con el id %%\n" : "");
                                } while (libro == null);
                                do
                                {
                                    Console.Write("\n\nAhora escribe el atributo que quieres cambiar:" +
                                    "\nTITULO" +
                                    "\nNOMBRE AUTOR" +
                                    "\nAPELLIDO AUTOR" +
                                    "\nEDITORIAL" +
                                    "\nSECCION" +
                                    "\nCANTIDAD \n\nAtributo:");

                                    atributo = verificadorString(Console.ReadLine());

                                    switch (atributo)
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
                                subopcion = verificadorByte(Console.ReadLine());

                                if (subopcion == 1)
                                {
                                    Console.Write("\n@@ opcion 1 @@\nStaff: ");
                                    staffss.Add(staffHandler.Get(verificadorLong(Console.ReadLine())));
                                    Console.WriteLine(staffHandler.ImprimirStaffs(staffss));
                                    staffss.Clear();
                                }
                                else if (subopcion == 2)
                                {
                                    Console.Write("\n@@ opcion 2 @@\n");
                                    staffss = staffHandler.GetAll();
                                    Console.WriteLine(staffHandler.ImprimirStaffs(staffss));
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
                                Console.Write("\n\n---- Actualizar Staff ----\n");
                                do
                                {
                                    Console.Write("Escribe el id del staff que deseas actualizar:");
                                    staff = staffHandler.Get(verificadorLong(Console.ReadLine()));
                                    Console.WriteLine(staff == null ? "\n%% staff no encontrado" +
                                        "con el id %%\n" : "");
                                } while (staff == null);
                                do
                                {
                                    Console.Write("\n\nAhora escribe el atributo que quieres cambiar:" +
                                    "\nNOMBRE" +
                                    "\nAPELLIDO" +
                                    "\nTURNO \n\nAtributo:");

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
                                    Console.WriteLine(usuario == null ? "%% usuario no encontrado" +
                                            ", intentalo de nuevo %%" : "");
                                } while (usuario == null);
                                
                                do
                                {
                                    Console.Write("Id de Libro: ");
                                    libro = libroHandler.Get(verificadorLong(Console.ReadLine()));
                                    Console.WriteLine(libro == null ? "%% libro no encontrado" +
                                        ", intentalo de nuevo %%" : "");
                                } while(libro == null);
                                    
                                
                                do
                                {
                                    Console.Write("Id de Staff encargado: ");
                                    staff = staffHandler.Get(verificadorLong(Console.ReadLine()));
                                    Console.WriteLine(staff == null ? "%% staff no encontrado" +
                                        ", intentalo de nuevo %%" : "");
                                } while (staff == null);

                                string fechaPrestamo, fechaPrestamoCorrecta, fechaDevolucion,
                                    fechaDevolucionCorrecta;

                                do
                                {
                                    date = DateTime.Today
                                    dia1 = date.Day;
                                    mes1 = date.Month;
                                    anio1 = date.Year;
                                    fechaPrestamo = "" + mes1 + "/" + dia1 + "/" + anio1;
                                } while (!IsFecha(fechaPrestamo, out fechaPrestamo));
                                
                                dia2 = dia1 + 3;
                                
                                if (dia2 > GetDiaMes(mes1, anio1))
                                {
                                    dia2 -= GetDiaMes(mes1, anio1);
                                    mes1++;
                                }
                                
                                mes2 = mes1;
                                
                                if (mes2 > 12)
                                {
                                    mes2 = 1;
                                    anio1++;
                                }
                                anio2 = anio1;
                                
                                fechaDevolucion = "" + mes2 + "/" + dia2 + "/" + anio2;


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
                                
                                foreach(Prestamo prestamo in prestamoHandler.GetAll())
                                {
                                    consulta(prestamo);
                                }

                                subopcion = verificadorByte(Console.ReadLine());

                                if (subopcion == 1)
                                {
                                    Console.Write("\n@@ opcion 1 @@\nPrestamo: ");
                                    prestamos.Add(prestamoHandler.Get(verificadorLong(Console.ReadLine())));
                                    Console.WriteLine(prestamoHandler.ImprimirPrestamos(prestamos));
                                    prestamos.Clear();
                                }
                                else if (subopcion == 2)
                                {
                                    Console.Write("\n@@ opcion 2 @@\n");
                                    prestamos = prestamoHandler.GetAll();
                                    Console.WriteLine(prestamoHandler.ImprimirPrestamos(prestamos));
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
                                Console.Write("\n\n---- Actualizar Prestamo (solo el estatus) ----\n");
                                do
                                {
                                    Console.Write("Escribe el id del usuario asociado a un prestamo\nId del" +
                                        " usuario: ");
                                    prestamos = prestamoHandler.GetPrestamos(verificadorLong(Console.ReadLine()));
                                    prestamoHandler.ImprimirPrestamos(prestamos);
                                    Console.WriteLine(prestamos.Count == 0 ? "\n%% No se encontraron prestamos" +
                                        " asociados a ese usuario, vuelve a intentarlo %%\n" : "");
                                } while (prestamos.Count == 0);
                                do
                                {
                                    Console.Write("\nAhora ingresa el id del libro asociado" +
                                    "al prestamo \nId del libro: ");
                                    long idLibro = verificadorLong(Console.ReadLine());

                                    prestamo = null;
                                    foreach (Prestamo prestamo1 in prestamos)
                                    {
                                        if (prestamo1.IdLibro == idLibro)
                                            prestamo = prestamo1;

                                    }
                                    Console.WriteLine(prestamo == null ? "\n%% no se pudo " +
                                        "encontrar un prestamo con ese id de libro, vuelve a " +
                                        "intentarlo%%\n" : "");
                                } while (prestamo == null);
                                
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
                            if (!(int.TryParse(StringDerivado(obj, flag, i), out mes)))
                            {
                                salida = null;
                                return false;
                            }
                            flag = i + 2;
                            progreso++;
                        }
                        else if (progreso == 1)
                        {
                            if (!(int.TryParse(StringDerivado(obj, flag, i), out dia)))
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

            if (mes == 2 && dia == (int)Meses.FebreroB && !EsBisiesto(anio))
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

            salida = "" + mes + "/" + dia + "/" + anio;

            return true;
        }
        
        public static int GetDiaMes(int mes, int anio)
        {
            switch (mes)
            {
                case 1:
                    return (int)Meses.Enero;
                case 2:
                    return EsBisiesto(anio) ? (int)Meses.FebreroB : (int)Meses.Febrero;
                case 3:
                    return (int)Meses.Marzo;
                case 4:
                    return (int)Meses.Abril;
                case 5:
                    return (int)Meses.Mayo;
                case 6:
                    return (int)Meses.Junio;
                case 7:
                    return (int)Meses.Julio;
                case 8:
                    return (int)Meses.Agosto;
                case 9:
                    return (int)Meses.Septiembre;
                case 10:
                    return (int)Meses.Octubre;
                case 11:
                    return (int)Meses.Noviembre;
                case 12:
                    return (int)Meses.Diciembre;
                default:
                    return 0;

            }
        }

        public static bool EsBisiesto(int anio)
        {
            if(anio % 400 == 0 ? true :
                (anio % 4 == 0 ? (anio % 100 == 0 ? false : true) : false))
            {
                return true;
            }
            return false;

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