using administracion.Handler;
using administracion.Models;

namespace administracion
{
    internal class Program
    {
        public delegate T Ingreso<T>(string obj);
        static void Main(string[] args)
        {
            // delegados para verificar tipos de datos
            Ingreso<byte> verificadorByte = new Ingreso<byte>(IsByte);

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
                Console.WriteLine("%%%%%%%% ELIGE UNA OPCION %%%%%%%\n" +
                    "\t(0) SALIR\n\n" +
                    "\t(1) Control de Usuarios\n" +
                    "\t(2) Control de Libros\n" +
                    "\t(3) Control de Personal\n" +
                    "\t(4) Control de Prestamos\n");

                opcion = verificadorByte(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("%%%%%%%% OPCIONES USUARIO %%%%%%%\n" +
                            "\t(1) Crear nuevo Usuario\n" +
                            "\t(2) Mostrar Usuario\n" +
                            "\t(3) Actualizar Usuario\n" +
                            "\t(4) Eliminar Usuario\n" +
                            "\n\t (11) anterior pagina");
                        switch (verificadorByte(Console.ReadLine()))
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
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
                        Console.WriteLine("%%%%%%%% OPCIONES LIBRO %%%%%%%\n" +
                            "\t(1) Crear nuevo Libro\n" +
                            "\t(2) Mostrar Libro\n" +
                            "\t(3) Acutalizar Libro\n" +
                            "\t(4) Eliminar Libro\n" +
                            "\n\t (11) anterior pagina");
                        switch (verificadorByte(Console.ReadLine()))
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
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
                        Console.WriteLine("%%%%%%%% OPCIONES PERSONAL %%%%%%%\n" +
                            "\t(1) Crear nuevo Bibliotecario(a)\n" +
                            "\t(2) Mostrar Bibliotecario(a)\n" +
                            "\t(3) Acutalizar Bibliotecario(a)\n" +
                            "\t(4) Eliminar Bibliotecario(a)\n" +
                            "\n\t (11) anterior pagina");
                        switch (verificadorByte(Console.ReadLine()))
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
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
                        Console.WriteLine("%%%%%%%% OPCIONES PRESTAMO %%%%%%%\n" +
                            "\t(1) Ingresar o crear prestamo\n" +
                            "\t(2) Consultar prestamo\n" +
                            "\t(3) Acutalizar Prestamo\n" +
                            "\n\t (11) anterior pagina");
                        switch (verificadorByte(Console.ReadLine()))
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
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
    }
}