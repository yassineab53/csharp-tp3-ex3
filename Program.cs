using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Tp3Me
{
    [Serializable]
    public class Etudiant
    {
        public int Numero { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public float[] Notes { get; set; }

        public Etudiant() { } // Constructeur par défaut requis pour la sérialisation XML

        public Etudiant(int numero, string nom, string prenom)
        {
            Numero = numero;
            Nom = nom;
            Prenom = prenom;
        }

        public override string ToString()
        {
            return $"Numéro: {Numero}, Nom: {Nom}, Prénom: {Prenom}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Etudiant> etudiants = new List<Etudiant>();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Étudiant {i + 1}:");
                Console.Write("Numéro: ");
                int numero = int.Parse(Console.ReadLine());
                Console.Write("Nom: ");
                string nom = Console.ReadLine();
                Console.Write("Prénom: ");
                string prenom = Console.ReadLine();
                etudiants.Add(new Etudiant(numero, nom, prenom));
            }

            // Étape 2 : Sérialisation binaire
            string binaryFilePath = "etudiants.bin";
            SerializeBinary(etudiants, binaryFilePath);

            // Étape 3 : Désérialisation binaire
            List<Etudiant> binaryDeserializedList = DeserializeBinary(binaryFilePath);
            Console.WriteLine("\nListe d'étudiants chargée depuis le fichier binaire:");
            DisplayList(binaryDeserializedList);

            // Étape 4 : Sérialisation XML
            string xmlFilePath = "etudiants.xml";
            SerializeXml(etudiants, xmlFilePath);

            // Étape 5 : Désérialisation XML
            List<Etudiant> xmlDeserializedList = DeserializeXml(xmlFilePath);
            Console.WriteLine("\nListe d'étudiants chargée depuis le fichier XML:");
            DisplayList(xmlDeserializedList);

            Console.ReadLine();
        }
        static void SerializeBinary(List<Etudiant> etudiants, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, etudiants);
            }
            Console.WriteLine($"\nListe d'étudiants sérialisée en binaire dans le fichier : {filePath}");
        }
        // Désérialisation binaire
        static List<Etudiant> DeserializeBinary(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<Etudiant>)formatter.Deserialize(fs);
            }
        }
        // Sérialisation XML
        static void SerializeXml(List<Etudiant> etudiants, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Etudiant>));
                serializer.Serialize(fs, etudiants);
            }
            Console.WriteLine($"\nListe d'étudiants sérialisée en XML dans le fichier : {filePath}");
        }
        // Désérialisation XML
        static List<Etudiant> DeserializeXml(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Etudiant>));
                return (List<Etudiant>)serializer.Deserialize(fs);
            }
        }

        // Afficher la liste des étudiants
        static void DisplayList(List<Etudiant> etudiants)
        {
            foreach (var etudiant in etudiants)
            {
                Console.WriteLine(etudiant);
            }
        }
    }
}
