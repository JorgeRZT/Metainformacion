using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Metainformacion
{
    /// <summary>
    /// Java : Annotation
    /// C# : Attribute
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person>()
            {
                new Person(){Nombre = "Jorge", Id=1, Surname="Lopez"},
                //Compila, pero el nombre dentro de la base de datos no puede ser nulo.
                new Person(){Id=2, Surname="Hurtado"},
            };

            var generics = ToGeneric(people);

            //Tenemos que validar los elementos antes de guardarlos en la base de datos
            foreach(var generic in generics)
            {
                var type = generic.GetType(); //Obtenemos el tipo
                var properties = type.GetProperties(); //Obtenemos las propiedades de la clase
                foreach(var property in properties)
                {
                    var value = property.GetValue(generic); //Obtenemos el valor de la propiedad de una instancia
                }
            }

            List<Generic> ToGeneric(List<Object> list)
            {
                return list.Select(c => new Generic { obj = c, type = c.GetType() }); //Obtenemos lista de objetos genericos
            }
        }


        //Clase generica para pasar todos los objetos de cualquier tipo a un objeto de tipo generico
        public class Generic
        {
            public Object obj { get; set; }
            public Type type { get; set; }
        }
    }

    /// <summary>
    /// Limitamos la longitud del nombre para la base de datos
    /// en una longitud no superior a 50 cartacteres
    /// </summary>
    public class Person
    {
        public int Id { get; set; }
        [StringDataBase(Length = 50)]
        public string Nombre { get; set; }
        [StringDataBase(Length = 15, Required = false)]
        public string Surname { get; set; }
    }

    /// <summary>
    /// Definición para el atributo (Anotacion)
    /// * Heredan de Attribute
    /// * Terminan su nombre por "Atribute"
    /// </summary>
    public class StringDataBaseAttribute : Attribute, IValid
    {
        public StringDataBaseAttribute() { this.Required = true; }

        public int Length { get; set; }
        public bool Required { get; set; }

        public bool IsValid(String value)
        {
            //TODO: (Implementar)
            return true;
        }
    }

    /// <summary>
    /// Interfaz para comprobar si un valor es valido o no
    /// </summary>
    public interface IValid
    {
        bool IsValid(String value);
    }
}