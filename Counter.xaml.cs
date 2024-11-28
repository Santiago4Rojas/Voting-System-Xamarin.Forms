using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace JRojas
{
    public partial class Counter : ContentPage
    {
        // Diccionarios para contar los votos
        private Dictionary<string, int> studentVotes;
        private Dictionary<string, Dictionary<string, int>> teacherVotes;
        private CandidateService _candidateService;

        public Counter()
        {
            InitializeComponent();
            // Inicializar los diccionarios de conteo
            studentVotes = new Dictionary<string, int>();
            teacherVotes = new Dictionary<string, Dictionary<string, int>>();
            _candidateService = new CandidateService();  // Crear instancia del servicio de candidatos
            UpdateVoteCounts();  // Actualiza los conteos de votos al iniciar
        }

        // Método para agregar un voto de Student
        public void AddStudentVote(string documentNumber, string candidateName)
        {
            // Convertir el número de documento a mayúsculas para la comparación
            string normalizedDocumentNumber = documentNumber.ToUpper();

            // Verificar que el número de documento cumpla con las condiciones de Student
            if (normalizedDocumentNumber.StartsWith("SOF") || normalizedDocumentNumber.StartsWith("AMB") ||
                normalizedDocumentNumber.StartsWith("ALI") || normalizedDocumentNumber.StartsWith("ASST") ||
                normalizedDocumentNumber.StartsWith("ELE"))
            {
                // Verificar si el candidato está en la lista de candidatos
                var candidate = _candidateService.GetCandidates().FirstOrDefault(c => c.Name == candidateName);
                if (candidate != null)
                {
                    // Si el candidato ya tiene votos, incrementar el contador
                    if (studentVotes.ContainsKey(candidateName))
                    {
                        studentVotes[candidateName]++;
                    }
                    else
                    {
                        // Si es la primera vez que recibe un voto, inicializar el contador en 1
                        studentVotes[candidateName] = 1;
                    }
                }
                else
                {
                    throw new ArgumentException("Candidato no encontrado.");
                }
            }
            else
            {
                throw new ArgumentException("Número de documento no válido para Student.");
            }
        }

        // Método para agregar un voto de Teacher
        public void AddTeacherVote(string program, string candidateName)
        {
            // Inicializar el diccionario del programa si no existe
            if (!teacherVotes.ContainsKey(program))
            {
                teacherVotes[program] = new Dictionary<string, int>();
            }

            // Verificar si el candidato está en la lista de candidatos
            var candidate = _candidateService.GetCandidates().FirstOrDefault(c => c.Name == candidateName);
            if (candidate != null)
            {
                // Si el candidato ya tiene votos en el programa, incrementar el contador
                if (teacherVotes[program].ContainsKey(candidateName))
                {
                    teacherVotes[program][candidateName]++;
                }
                else
                {
                    // Si es la primera vez que recibe un voto, inicializar el contador en 1
                    teacherVotes[program][candidateName] = 1;
                }
            }
            else
            {
                throw new ArgumentException("Candidato no encontrado.");
            }
        }

        // Método para obtener el conteo de votos de Student
        public Dictionary<string, int> GetStudentVotes()
        {
            return studentVotes;
        }

        // Método para obtener el conteo de votos de Teacher por programa
        public Dictionary<string, Dictionary<string, int>> GetTeacherVotes()
        {
            return teacherVotes;
        }

        // Método para actualizar los conteos de votos en la interfaz
        private void UpdateVoteCounts()
        {
            // Obtener los votos de los estudiantes
            var studentVoteCounts = GetStudentVotes();
            StudentVoteCountLabel.Text = "Votos Estudiantes:\n" + string.Join("\n", studentVoteCounts.Select(v => $"{v.Key}: {v.Value}"));

            // Obtener los votos de los profesores
            var teacherVoteCounts = GetTeacherVotes();
            TeacherVoteCountLabel.Text = "Votos Profesores:\n";
            foreach (var program in teacherVoteCounts)
            {
                TeacherVoteCountLabel.Text += $"\n{program.Key}:\n";
                foreach (var candidate in program.Value)
                {
                    TeacherVoteCountLabel.Text += $"{candidate.Key}: {candidate.Value}\n";
                }
            }
        }

        // Evento para actualizar los votos manualmente
        private void OnUpdateVoteCountClicked(object sender, EventArgs e)
        {
            UpdateVoteCounts();  // Llamar a la función para actualizar los votos
        }
    }
}